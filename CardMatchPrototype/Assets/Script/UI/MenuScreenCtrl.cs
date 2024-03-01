using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class MenuScreenCtrl : UIScreenBaseCtrl {
    public Button PlayButton;
    public Button ResumeButton;

    private Action<string> _callback;

    private void Awake() {
        this.PlayButton.onClick.AddListener(OnPlayButtonClick);
        this.ResumeButton.onClick.AddListener(OnResumeButtonClick);
    }

    public void Init(Action<string> callback) {
        this._callback = callback;
        this.SetScreen();
        this.CheckForAnySavedGames();
        this.Show();
    }

    private void OnPlayButtonClick() {
        this._callback?.Invoke("playbutton");
        SoundController.instance.PlayOneShot("ButtonClick");
    }

    private void OnResumeButtonClick() {
        this._callback?.Invoke("resumebutton");
        SoundController.instance.PlayOneShot("ButtonClick");
    }

    private void CheckForAnySavedGames() {
        JSONObject savedGame = LocalDataController.instance.GetValue("savedGame");
        ResumeButton.gameObject.SetActive(savedGame != null && !savedGame.IsNull);
    }
}
