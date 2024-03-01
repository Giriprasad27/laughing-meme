using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class CardGridOptions {
    public LevelObject LevelObject;
    public Action<string> callback;
    public JSONObject savedLevelData;
    public int ColumnCount() {
        int numColumns = LevelObject.totalCardCount / LevelObject.rowCount;
        if (LevelObject.totalCardCount % LevelObject.rowCount != 0) {
            numColumns++;
        }
        return numColumns;
    }
    public Action<CardObject> scoreCalback;
}

public class CardGridCtrl : MonoBehaviour
{ 
    private GridLayoutGroup _gridLayout;
    private RectTransform _rectTransform;

    private CardCtrl _selectedcard = null;
    private int _activeCardCount = 0;
    private int _turnCount = 0;

    private CardGridOptions _options;

    private JSONObject _leveldata;
    private bool[] _cardClaimStatus;
    private List<CardOption> _cardOptionsList;

    public void Init(CardGridOptions option) {
        this. _options = option;
        this.SetGrid();
        this.RenderCards();
    }

    public void ClearGrid() {
        ObjectPooler.SharedInstance.ClearAllObject(0);
        this._selectedcard = null;
        this.gameObject.SetActive(false);
    }

    public void SaveLevelProgression() {
        this.SetLevelJsonData(this._cardOptionsList);
    }

    private void SetGrid() {
        this._turnCount = 0;
        this.gameObject.SetActive(true);
        this._gridLayout = this.GetComponent<GridLayoutGroup>();
        this._rectTransform = this.GetComponent<RectTransform>();
        float cardSize = 1;
        if (this._rectTransform != null) {
            float gridHeight = this._rectTransform.rect.height;
            gridHeight = gridHeight - ((this._options.LevelObject.Padding * 2) + ((this._options.LevelObject.rowCount - 1) * this._options.LevelObject.spacing));
            cardSize = gridHeight / this._options.LevelObject.rowCount;
            if (cardSize * this._options.ColumnCount() > gridHeight) {
                float gridwidth = this._rectTransform.rect.width;
                gridwidth = gridwidth - ((this._options.LevelObject.Padding * 2) + ((this._options.ColumnCount() - 1) * this._options.LevelObject.spacing));
                cardSize = gridwidth / this._options.ColumnCount();
            }
        }

        if (this._gridLayout != null) {
            this._gridLayout.cellSize = new Vector2(cardSize, cardSize);
            this._gridLayout.constraintCount = this._options.LevelObject.rowCount;
            this._gridLayout.spacing = Vector2.one * this._options.LevelObject.spacing;
            this._gridLayout.padding.top = this._gridLayout.padding.bottom = this._gridLayout.padding.left = this._gridLayout.padding.right = this._options.LevelObject.Padding;
        }
    }

    private void RenderCards() {
        this._cardOptionsList = new List<CardOption>();
        this._activeCardCount = this._options.LevelObject.totalCardCount;
        this._cardClaimStatus = new bool[this._activeCardCount];

        if (this._options.savedLevelData != null) {
            this.LoadSavedLevel();
        } else {
            this.GenerateNewLevel();
        }
        for (int i = 0; i < this._options.LevelObject.totalCardCount; i++) {
            GameObject card = ObjectPooler.SharedInstance.GetPooledObject(0);
            card.transform.SetParent(this.transform);
            card.transform.localScale = Vector3.one;
            CardCtrl cardCtrl = card.GetComponent<CardCtrl>();
            cardCtrl.Init(this._cardOptionsList[i]);
        }
    }

    private void LoadSavedLevel() {
        JSONObject savedLevelData = this._options.savedLevelData;
        ResourceCtrl.instance.ResourceData.BuildCardNameMap();
        for (int i = 0; i < savedLevelData.Count; i++) {
            JSONObject card = savedLevelData[i.ToString()];
            string cardname = card["cardname"].str;
            this._cardClaimStatus[i] = card["claimed"].b;
            CardObject carddata = ResourceCtrl.instance.ResourceData.GetCardByName(cardname);
            CardOption cardOption = new CardOption {
                cardData = carddata,
                callback = CheckForMatchCard,
                hideCard = this._cardClaimStatus[i]
            };
            this._cardOptionsList.Add(cardOption);
            if (this._cardClaimStatus[i]) {
                this._activeCardCount--;
            }
        }
    }

    private void GenerateNewLevel() {
        List<CardObject> randCardData = this.GetRandomCardData(this._options.LevelObject.totalCardCount / 2);
        int j = 0;
        int counter = 0;
        for (int i = 0; i < this._options.LevelObject.totalCardCount; i++) {
            CardOption cardOption = new CardOption {
                cardData = randCardData[j],
                callback = CheckForMatchCard
            };
            counter++;
            if (counter == 2) {
                j++;
                counter = 0;
            }
            this._cardOptionsList.Add(cardOption);
            this._cardClaimStatus[i] = (false);
        }
        this._cardOptionsList = this.ShuffleCards(this._cardOptionsList);
    }

    private List<CardOption> ShuffleCards(List<CardOption> list) {
        System.Random rand = new System.Random();
        for (int i = list.Count - 1; i > 0; i--) {
            var k = rand.Next(i + 1);
            var value = list[k];
            list[k] = list[i];
            list[i] = value;
        }
        return list;
    }

    private List<CardObject> GetRandomCardData(int count) {
        List<CardObject> icons = new List<CardObject>();
        int i = 0;
        while (i < count) {
            flag: int rand = UnityEngine.Random.Range(0, count);
            if (icons.Contains(ResourceCtrl.instance.ResourceData.CardObjects[rand])) {
                goto flag;
            }
            icons.Add(ResourceCtrl.instance.ResourceData.CardObjects[rand]);
            i++;
        }
        return icons;
    }

    private void CheckForMatchCard(CardCtrl card) {
        if (this._selectedcard == null) {
            this._selectedcard = card;
        } else {
            if (this._selectedcard.GetCardId() == card.GetCardId()) {
                this.MardCardAsClaimed(card);
                this.MardCardAsClaimed(this._selectedcard);
                SoundController.instance.PlayOneShot("ClamSucess");
                this.CheckForGameComplete();
            } else {
                this._selectedcard.ReFlipCard();
                card.ReFlipCard();
                SoundController.instance.PlayOneShot("ClamFail");
            }
            this._selectedcard = null;
            this._turnCount++;
        }
    }
    private void CheckForGameComplete() {
        if (this._activeCardCount <= 0) {
            this._options.callback?.Invoke("GameFinished");
        }
    }

    private void SetLevelJsonData(List<CardOption> cardOptionsList) {
        if (_activeCardCount > 0) {
            this._leveldata = new JSONObject();
            this._leveldata.AddField("score", GameController.instance.GetScore());
            this._leveldata.AddField("turn", this._turnCount);
            this._leveldata.AddField("difficulty", (int)this._options.LevelObject.level);

            JSONObject carddatas = new JSONObject();
            for (int i = 0; i < cardOptionsList.Count; i++) {
                JSONObject carddata = new JSONObject();
                carddata.AddField("cardname", cardOptionsList[i].cardData.name);
                carddata.AddField("claimed", this._cardClaimStatus[i]);
                carddatas.SetField(i.ToString(), carddata);
            }
            this._leveldata.SetField("carddatas", carddatas);
            Debug.Log(this._leveldata.ToString());
            LocalDataController.instance.SetValue("savedGame", this._leveldata);
        }
    }

    private void MardCardAsClaimed(CardCtrl card) {
        card.OnCardMatched();
        CardOption cardOption = card.GetCardOption();
        int index = this._cardOptionsList.IndexOf(cardOption);
        this._cardClaimStatus[index] = true;
        this._activeCardCount --;
        this._options.scoreCalback?.Invoke(cardOption.cardData);
    }

    private void OnApplicationQuit() {
        Debug.Log("OnApplicationQuit");
        this.SetLevelJsonData(this._cardOptionsList);
    }
}
