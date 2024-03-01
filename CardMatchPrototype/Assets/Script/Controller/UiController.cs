using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiController : MonoBehaviour
{
    private MenuScreenCtrl _menuScreen;
    private InGameUIScreenCtrl _inGameUIScreen;
    private GameCompleteScreenCtrl _gameCompleteScreen;
    private LevelSelectionScreenCtrl _levelSelectionScreen;
    private Action<UiCallback> _callback;
    public void Init(Action<UiCallback> callback) {
        this._callback = callback;
        this.RenderMenuScreen();
    }

    public void RenderMenuScreen() {
        if (this._menuScreen == null) { 
            this._menuScreen = Instantiate(ResourceCtrl.instance.ResourceData.MenuScreen) as MenuScreenCtrl;
            this._menuScreen.gameObject.transform.SetParent(this.transform);
        }
        this._menuScreen.Init(callback: OnMenuScreenCallBack);
    }

    public void RenderInGameUI() {
        if (this._inGameUIScreen == null) { 
            this._inGameUIScreen = Instantiate(ResourceCtrl.instance.ResourceData.InGameUIScreen) as InGameUIScreenCtrl;
            this._inGameUIScreen.gameObject.transform.SetParent(this.transform);
        }
        this._inGameUIScreen.Init(callback: OnInGameScreenCallBack);
    }
    public void OnGameFinished() {
        this._inGameUIScreen.Hide();
        this.RenderGameFinishScreen();
    }

    private void RenderGameFinishScreen() {

        if (this._gameCompleteScreen == null) {
            this._gameCompleteScreen = Instantiate(ResourceCtrl.instance.ResourceData.GameCompleteScreen) as GameCompleteScreenCtrl;
            this._gameCompleteScreen.gameObject.transform.SetParent(this.transform);
        }
        this._gameCompleteScreen.Init(callback: OnGameCompleteScreenCallBack);
    }


    private void RenderLevelSelectionScreen() {
        if (this._levelSelectionScreen == null) {
            this._levelSelectionScreen = Instantiate(ResourceCtrl.instance.ResourceData.LevelSelectionScreen) as LevelSelectionScreenCtrl;
            this._levelSelectionScreen.gameObject.transform.SetParent(this.transform);
        }
        this._levelSelectionScreen.Init(callback: OnLevelSelectionScreenCallBack);
    }

    private void OnMenuScreenCallBack() {
        this._menuScreen.Hide();
        this.RenderLevelSelectionScreen();
    }

    private void OnInGameScreenCallBack(string type) {
        switch (type) {
            case "QuitGame":
            this._inGameUIScreen.Hide();
            UiCallback callbackObj = new UiCallback() {
                type = "GoToMenu"
            };
            this._callback?.Invoke(callbackObj);
            break;
        }
    }

    private void OnGameCompleteScreenCallBack() {
        this._gameCompleteScreen.Hide();
        UiCallback callbackObj = new UiCallback() {
            type = "GoToMenu"
        };
        this._callback?.Invoke(callbackObj);
    }

    private void OnLevelSelectionScreenCallBack(Difficulty difficulty) {
        this._levelSelectionScreen.Hide();
        UiCallback callbackObj = new UiCallback() {
            type = "GoToGame",
            value = (int)difficulty
        };
        this._callback?.Invoke(callbackObj);
    }
}

public class UiCallback{
    public string type;
    public int value = 0;
}
