using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GameCompleteScreenCtrl : UIScreenBaseCtrl {

    public Button HomeButton;
    public TextMeshProUGUI ScoreText;

    private Action _callback;

    private void Awake() {
        this.HomeButton.onClick.AddListener(OnHomeButtonClick);
    }

    public void Init(Action callback) {
        this._callback = callback;
        this.SetScreen();
        this._Init();
    }

    private void _Init() {
        this.ScoreText.text ="Score: "+ GameController.instance.GetScore().ToString("D4");
        this.Show();
    }

    private void OnHomeButtonClick() {
        this._callback?.Invoke();
    }
}
