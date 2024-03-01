using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GameController : MonoBehaviour {

    public GameObject GameCanvas;
    public UiController UiController;

    private CardGridCtrl _cardGridCtrl;

    private int _score = 0;

    private Difficulty _levelDifficulty = Difficulty.Easy;

    public static GameController instance;
    public event Action<int> OnScoreUpdate;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    private void Start() {
        this.UiController.Init(OnUIControllerCallBack);
    }

    private void RenderCardGrid(JSONObject gamedata = null) {
        if (this._cardGridCtrl == null) {
            this._cardGridCtrl = Instantiate(ResourceCtrl.instance.ResourceData.CardGridCtrl) as CardGridCtrl;
            this._cardGridCtrl.gameObject.transform.SetParent(GameCanvas.transform);
            this._cardGridCtrl.gameObject.transform.localScale = Vector3.one;
        }

        this._cardGridCtrl.Init(new CardGridOptions {
            LevelObject = ResourceCtrl.instance.ResourceData.LevelObjects[(int)this._levelDifficulty],
            callback = OnCardGridCallBack,
            savedLevelData = gamedata
        }); ;
        this.UiController.RenderInGameUI();
    }

    private void OnCardGridCallBack(string key) {
        switch (key) {
            case "GameFinished":
            Invoke("OnGameFinished",1);
            break;
        }
    }

    private void OnGameFinished() {
        this.UiController.OnGameFinished();
        LocalDataController.instance.SetValue("savedGame", null);
    }

    private void OnUIControllerCallBack(UiCallback callbackObj) {
        string key = callbackObj.type;
        switch (key) {
            case "GoToGame":  // from level seletion
            this._levelDifficulty = (Difficulty)callbackObj.value;
            this._score = 0;
            this.RenderCardGrid();
            LocalDataController.instance.SetValue("savedGame", null); // cleared old save game if any
            break;
            case "ResumeSavedGame": // from menu screen itself from resume button
            JSONObject savedGame = LocalDataController.instance.GetValue("savedGame");
            if (savedGame != null && !savedGame.IsNull) {
                this._score = (int)savedGame["score"].i;
                this._levelDifficulty = (Difficulty)(int)savedGame["difficulty"].i;
                this.RenderCardGrid(savedGame["carddatas"]);
                OnScoreUpdate(this._score);
            }
            break;
            case "GoToMenu":
            this.OnGoToMainMenu();
            break;
            case "SaveAndToMenu":
            this._cardGridCtrl.SaveLevelProgression();
            this.OnGoToMainMenu();
            break;
        }
    }

    private void OnGoToMainMenu() {
        this._cardGridCtrl.ClearGrid();
        this.UiController.RenderMenuScreen();
    }

    public void CardClaimed(int score) {
        this._score += score;
        if (this.OnScoreUpdate != null) {
            this.OnScoreUpdate(this._score);
        }
    }
    public int GetScore() {
        return this._score;
    }
}

public enum Difficulty {
    Easy = 0,
    Intermidate =1,
    Expert = 2,
    Professional = 3
}
