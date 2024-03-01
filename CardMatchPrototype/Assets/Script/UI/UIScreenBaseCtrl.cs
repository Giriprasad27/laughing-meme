using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScreenBaseCtrl : MonoBehaviour
{
    public Animator animator;
    public void SetScreen() {
        RectTransform panelRect = this.GetComponent<RectTransform>();
        panelRect.anchorMin = new Vector2(0, 0);
        panelRect.anchorMax = new Vector2(1, 1);
        panelRect.pivot = new Vector2(0.5f, 0.5f);
        panelRect.anchoredPosition = Vector2.zero;
        panelRect.sizeDelta = Vector2.zero;
        this.gameObject.transform.localScale = Vector3.one;
        this.gameObject.transform.localPosition = Vector3.zero;
    }
    public virtual void Show() {
        this.gameObject.SetActive(true);
        SoundController.instance.PlayOneShot("Window");
        if (this.animator != null) {
            this.animator.SetTrigger("show");
        }
    }

    public virtual void Hide() {
        if (this.animator != null) {
            this.animator.SetTrigger("hide");
        } else {
            this.gameObject.SetActive(false);
        }
    }

    public void OnFinishHideAnimation() {
        this.gameObject.SetActive(false);
    }
}
