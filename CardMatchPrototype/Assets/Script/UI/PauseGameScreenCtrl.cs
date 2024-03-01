using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseGameScreenCtrl : UIScreenBaseCtrl {
    public Button QuitButton;
    public Button SaveAndQuitButton;
    public Button CloseButton;

    private Action<string> _callback;

    private void Awake() {
        QuitButton.onClick.AddListener(OnQuitButtonClick);
        SaveAndQuitButton.onClick.AddListener(OnSaveButtonClick);
        CloseButton.onClick.AddListener(OnCloseButtonClick);
    }

    private void OnQuitButtonClick() {
        this._callback?.Invoke("QuitGame");
        SoundController.instance.PlayOneShot("ButtonClick");
        this.Hide();
    }

    private void OnSaveButtonClick() {
        this._callback?.Invoke("SaveAndQuit");
        SoundController.instance.PlayOneShot("ButtonClick");
        this.Hide();
    }

    private void OnCloseButtonClick() {
        this.Hide();
        SoundController.instance.PlayOneShot("ButtonClick");
    }

    internal void Init(Action<string> callback) {
        this._callback = callback;
        this.SetScreen();
        this.Show();
    }
}
