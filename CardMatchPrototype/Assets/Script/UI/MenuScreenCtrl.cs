using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class MenuScreenCtrl : UIScreenBaseCtrl {
    public Button PlayButton;

    private Action _callback;

    private void Awake() {
        this.PlayButton.onClick.AddListener(OnPlayButtonClick);
    }

    public void Init(Action callback) {
        this._callback = callback;
        this.SetScreen();
        this.Show();
    }

    private void OnPlayButtonClick() {
        this._callback?.Invoke();
    }
}
