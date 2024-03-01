using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
public class CardOption {
    public CardObject cardData;
    public bool hideCard = false;
    public Action<CardCtrl> callback;
}

public class CardCtrl : MonoBehaviour {
    public GameObject ActiveObject;
    public Image Icon;
    private Button _button;
    private Image _image;
    private CardOption _option;

    private float _resetTime = 0.75f;

    private void Awake() {
        this._button = this.GetComponent<Button>();
        this._image = this.GetComponent<Image>();
        if (this._button != null) {
            this._button.onClick.AddListener(OnCardButtonClick);
        }
    }

    public void Init(CardOption option) {
        this._option = option;
        this.gameObject.SetActive(true);
        this._image.enabled = true;
        this.Icon.sprite = this._option.cardData.icon;
        if (this._option.hideCard) {
            this.HideCard();
        } else {
            this.ShowCard();
        }
    }

    public string GetCardId() {
        if (this._option!= null) {
            return this._option.cardData.id;
        } else {
            return null;
        }
    }

    public int GetCardScore() {
        if (this._option != null) {
            return this._option.cardData.score;
        } else {
            return 0;
        }
    }

    public CardOption GetCardOption() {
        if (this._option != null) {
            return this._option;
        } else {
            return null;
        }
    }

    public void ResetCard() {
        Invoke("ShowCard", this._resetTime);
    }
    private void ShowCard() {
        this._button.enabled = true;
        this.ActiveObject.SetActive(false);
    }
    private void HideCard() {
        this._image.enabled = false;
        this.ActiveObject.SetActive(false);
    }

    public void OnCardMatched() {
        Invoke("_OnCardMatched", this._resetTime);
    }
    private void _OnCardMatched() {
        this.HideCard();
        if (GameController.instance != null) {
            GameController.instance.CardClaimed(this._option.cardData);
        }
    }

    private void OnCardButtonClick() {
        this._button.enabled = false;
        this.ActiveObject.SetActive(true);
        this._option.callback?.Invoke(this);
    }
    
}
