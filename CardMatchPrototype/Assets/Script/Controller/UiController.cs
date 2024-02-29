using System;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class UiController : MonoBehaviour
{
    private MenuScreenCtrl _menuScreen;
    private Action<string> _callback;
    public void Init(Action<string> callback) {
        this._callback = callback;
        this.RenderMenuScreen();
    }

    private void RenderMenuScreen() {
        if (this._menuScreen != null) {
            this._menuScreen.Show();
        } else {
            this._menuScreen = Instantiate(ResourceCtrl.instance.ResourceData.MenuScreen) as MenuScreenCtrl;
            this._menuScreen.gameObject.transform.SetParent(this.transform);
            this._menuScreen.gameObject.transform.localScale = Vector3.one;
            this._menuScreen.gameObject.transform.localPosition = Vector3.zero;
            this._menuScreen.Init(callback: OnMenuScreenCallBack);
        }
    }

    private void OnMenuScreenCallBack() {
        this._menuScreen.Hide();
        this._callback?.Invoke("GoToGame");
        //this.RenderLevelSelectionScreen();
    }

    private void RenderLevelSelectionScreen() {
        //if (this._menuScreen != null) {
        //    this._menuScreen.Show();
        //} else {
        //    this._menuScreen = Instantiate(ResourceCtrl.instance.ResourceData.MenuScreenCtrl) as MenuScreenCtrl;
        //    this._menuScreen.gameObject.transform.SetParent(this.transform);
        //    this._menuScreen.gameObject.transform.localScale = Vector3.one;
        //    this._menuScreen.Init(callback: OnMenuScreenCallBack);
        //}
    }
}
