using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
public class CardOption {
    public int index;
    public string id;
    public Action<CardCtrl> callback;
}

public class CardCtrl : MonoBehaviour
{
    private Button _button;
    private Image _image;
    private CardOption _option;
    private Action _callback;

    public void Init(CardOption option) {
        this._option = option;
        this._button = this.GetComponent<Button>();
        this._image = this.GetComponent<Image>();
        if (this._button != null) {
            this._button.onClick.AddListener(OnCardButtonClick);
        }
    }

    public string GetCardId() {
        if (this._option!= null) {
            return this._option.id;
        } else {
            return null;
        }
    }

    public void ResetCard() {

    }

    public void OnCardMatched() {
        this._image.enabled = false;
    }

    private void OnCardButtonClick() {
        this._option.callback?.Invoke(this);
    }
}
