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
    public Animator Animator;
    public Image Icon;
    public Image SpecialIcon;
    private Button _button;
    private Image _image;
    private CardOption _option;

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
        if (this._option.cardData.specialIcon != null) {
            this.SpecialIcon.sprite = this._option.cardData.specialIcon;
        } else {
            this.SpecialIcon.sprite = ResourceCtrl.instance.ResourceData.EmptySprite;
        }
        if (this._option.hideCard) {
            this.DisableCard();
        } else {
            this.FlipCard();
            this.ReFlipCard(1.5f);
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

    public void ReFlipCard(float resettime = 0.75f) {
        Invoke("EnableCard", resettime);
    }
    private void FlipCard() {
        this._button.enabled = false;
        //this.ActiveObject.SetActive(true);
        this.Animator.SetTrigger("flip");
    }
    private void EnableCard() {
        this._button.enabled = true;
        //this.ActiveObject.SetActive(false);
        this.Animator.SetTrigger("reflip");
    }
    private void DisableCard() {
        this._image.enabled = false;
        //this.ActiveObject.SetActive(false);
        this.Animator.SetTrigger("claim");
    }

    public void OnCardMatched() {
        Invoke("_OnCardMatched", 0.75f);
    }
    private void _OnCardMatched() {
        this.DisableCard();
    }

    private void OnCardButtonClick() {
        this.FlipCard();
        SoundController.instance.PlayOneShot("CardFlip");
        this._option.callback?.Invoke(this);
    }
    
}
