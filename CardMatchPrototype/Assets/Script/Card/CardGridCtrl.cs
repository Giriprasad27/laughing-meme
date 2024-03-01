using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class CardGridOptions {
    public LevelObject LevelObject;
    public Action<string> callback;
    public int ColumnCount() {
        int numColumns = LevelObject.totalCardCount / LevelObject.rowCount;
        if (LevelObject.totalCardCount % LevelObject.rowCount != 0) {
            numColumns++;
        }
        return numColumns;
    }
}

public class CardGridCtrl : MonoBehaviour
{ 
    private GridLayoutGroup _gridLayout;
    private RectTransform _rectTransform;

    private CardCtrl _selectedcard = null;
    private int _activeCardCount = 0;

    private CardGridOptions _options;

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

    private void SetGrid() {
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
        this._activeCardCount = this._options.LevelObject.totalCardCount;
        List<CardObject> randCardData = this.GetRandomCardData(this._options.LevelObject.totalCardCount / 2);
        int j = 0;
        int counter = 0;
        List<CardOption> cardOptionsList = new List<CardOption>();
        for (int i = 0; i < this. _options.LevelObject.totalCardCount; i++) {
            CardOption cardOption = new CardOption {
                cardData = randCardData[j],
                callback = CheckForMatchCard
            };
            counter++;
            if (counter == 2) {
                j++;
                counter = 0;
            }
            cardOptionsList.Add(cardOption);
        }
        cardOptionsList = this.ShuffleCards(cardOptionsList);
        for (int i = 0; i < this._options.LevelObject.totalCardCount; i++) {
            GameObject card = ObjectPooler.SharedInstance.GetPooledObject(0);
            card.transform.SetParent(this.transform);
            card.transform.localScale = Vector3.one;
            CardCtrl cardCtrl = card.GetComponent<CardCtrl>();
            cardCtrl.Init(cardOptionsList[i]);
        }
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
            flag: int rand = UnityEngine.Random.RandomRange(0, count);
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
                this._selectedcard.OnCardMatched();
                card.OnCardMatched();
                this._activeCardCount -= 2;
                this.CheckForGameComplete();
            } else {
                this._selectedcard.ResetCard();
                card.ResetCard();
            }
            this._selectedcard = null;
        }
    }
    private void CheckForGameComplete() {
        if (this._activeCardCount <= 0) {
            this._options.callback?.Invoke("GameFinished");
        }
    }
}
