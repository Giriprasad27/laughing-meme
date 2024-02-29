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
    public CardCtrl Card;

    private GridLayoutGroup _gridLayout;
    private RectTransform _rectTransform;

    public void Init(CardGridOptions options) {
        this._gridLayout = this.GetComponent<GridLayoutGroup>();
        this._rectTransform = this.GetComponent<RectTransform>();
        float cardSize = 1;
        if (this._rectTransform != null) {
            float gridHeight = this._rectTransform.rect.height;
            gridHeight = gridHeight - ((options.Padding * 2) + ((options.rowCount - 1) * options.spacing));
            cardSize = gridHeight / options.rowCount;
            if (cardSize * options.ColumnCount() > gridHeight) {
                float gridwidth = this._rectTransform.rect.width;
                gridwidth = gridwidth - ((options.Padding * 2) + ((options.ColumnCount() - 1) * options.spacing));
                cardSize = gridwidth / options.ColumnCount();
            }
        }

        if (this._gridLayout != null) {
            this._gridLayout.cellSize = new Vector2(cardSize, cardSize);
            this._gridLayout.constraintCount = options.rowCount;
            this._gridLayout.spacing = Vector2.one * options.spacing;
            this._gridLayout.padding.top = this._gridLayout.padding.bottom = this._gridLayout.padding.left = this._gridLayout.padding.right = options.Padding;
        }

        for (int i = 0; i < options.totalCardCount; i++) {
            GameObject card = Instantiate(Card.gameObject);
            card.transform.SetParent(this.transform);
            card.transform.localScale = Vector3.one;
        }
    }
}
