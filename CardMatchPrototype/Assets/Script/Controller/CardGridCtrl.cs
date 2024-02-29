using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardGridOptions {
    public int rowCount = 1;
    public int totalCardCount = 1;
    public float spacing = 20;
    public int Padding = 20;

    public int ColumnCount() {
        int numColumns = totalCardCount / rowCount;
        if (totalCardCount % rowCount != 0) {
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
    private CardGridOptions _options;

    public void Init(CardGridOptions option) {
        this. _options = option;
        this.SetGrid();
        this.RenderCards();
    }

    private void SetGrid() {
        this._gridLayout = this.GetComponent<GridLayoutGroup>();
        this._rectTransform = this.GetComponent<RectTransform>();
        float cardSize = 1;
        if (this._rectTransform != null) {
            float gridHeight = this._rectTransform.rect.height;
            gridHeight = gridHeight - ((this._options.Padding * 2) + ((this._options.rowCount - 1) * this._options.spacing));
            cardSize = gridHeight / this._options.rowCount;
            if (cardSize * this._options.ColumnCount() > gridHeight) {
                float gridwidth = this._rectTransform.rect.width;
                gridwidth = gridwidth - ((this._options.Padding * 2) + ((this._options.ColumnCount() - 1) * this._options.spacing));
                cardSize = gridwidth / this._options.ColumnCount();
            }
        }

        if (this._gridLayout != null) {
            this._gridLayout.cellSize = new Vector2(cardSize, cardSize);
            this._gridLayout.constraintCount = this._options.rowCount;
            this._gridLayout.spacing = Vector2.one * this._options.spacing;
            this._gridLayout.padding.top = this._gridLayout.padding.bottom = this._gridLayout.padding.left = this._gridLayout.padding.right = this._options.Padding;
        }
    }

    private void RenderCards() {

        List<Sprite> randIcons = this.GetRandomIcons(this._options.totalCardCount / 2);
        int j = 0;
        int counter = 0;
        List<CardOption> cardOptionsList = new List<CardOption>();
        for (int i = 0; i < this. _options.totalCardCount; i++) {
            CardOption cardOption = new CardOption {
                id = j,
                icon = randIcons[j],
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
        for (int i = 0; i < this._options.totalCardCount; i++) {
            GameObject card = Instantiate(GameController.instance.ResourceData.CardCtrl.gameObject) as GameObject;
            card.transform.SetParent(this.transform);
            card.transform.localScale = Vector3.one;
            CardCtrl cardCtrl = card.GetComponent<CardCtrl>();
            Debug.Log(cardOptionsList[i].icon.name);
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

    private List<Sprite> GetRandomIcons(int count) {
        List<Sprite> icons = new List<Sprite>();
        int i = 0;
        while (i < count) {
            flag: int rand = UnityEngine.Random.RandomRange(0, count);
            if (icons.Contains(GameController.instance.ResourceData.IconSprites[rand])) {
                goto flag;
            }
            icons.Add(GameController.instance.ResourceData.IconSprites[rand]);
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
            } else {
                this._selectedcard.ResetCard();
                card.ResetCard();
            }
            this._selectedcard = null;
        }
    }
}
