using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScreenBaseCtrl : MonoBehaviour
{
    public void SetScreen() {
        RectTransform panelRect = this.GetComponent<RectTransform>();
        panelRect.anchorMin = new Vector2(0, 0);
        panelRect.anchorMax = new Vector2(1, 1);
        panelRect.pivot = new Vector2(0.5f, 0.5f);
        panelRect.anchoredPosition = Vector2.zero;
        panelRect.sizeDelta = Vector2.zero;
    }
    public virtual void Show() {
        this.gameObject.SetActive(true);
    }

    public virtual void Hide() {
        this.gameObject.SetActive(false);
    }
}
