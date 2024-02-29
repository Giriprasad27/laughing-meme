using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public GameObject GameCanvas;
    public UiController UiController;

    private CardGridCtrl _cardGridCtrl;

    private Difficulty _levelDifficulty = Difficulty.Easy;

    private void Start() {
        this.UiController.Init(OnUIControllerCallBack);
    }

    private void RenderLevel() {
        this._cardGridCtrl = Instantiate(ResourceCtrl.instance.ResourceData.CardGridCtrl) as CardGridCtrl;
        this._cardGridCtrl.gameObject.transform.SetParent(GameCanvas.transform);
        this._cardGridCtrl.gameObject.transform.localScale = Vector3.one;
        this._cardGridCtrl.Init(new CardGridOptions {
            LevelObject = ResourceCtrl.instance.ResourceData.LevelObjects[(int)this._levelDifficulty]
        });
    }

    private void OnUIControllerCallBack(string key) {
        switch (key) {
            case "GoToGame":
            this.RenderLevel();
            break;
        }
    }
}

public enum Difficulty {
    Easy = 0,
    Intermidate =1,
    Expert = 2,
    Professional = 3
}
