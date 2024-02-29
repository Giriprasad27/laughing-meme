using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class MenuScreenCtrl : UIScreenBaseCtrl {
    public Button PlayButton;

    private Action _callback;

    public void Init(Action callback) {
        this._callback = callback;
        this.SetScreen();
        this._Init();
    }

    private void _Init() {
        this.PlayButton.onClick.AddListener(OnPlayButtonClick);
    }

    private void OnPlayButtonClick() {
        this._callback?.Invoke();
    }
}
