using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class InGameUIScreenCtrl : UIScreenBaseCtrl {

    public Button PauseButton;
    public TextMeshProUGUI ScoreText;
    public GameObject HeatModeObject;
    public Slider HeatModeSlider;
    public TextMeshProUGUI HeatModeValText;

    private Action<string> _callback;

    private void Awake() {
        this.PauseButton.onClick.AddListener(OnPauseButtonClick);
    }

    public void Init(Action<string> callback) {
        this._callback = callback;
        this.SetScreen();
        this._Init();
    }

    private void _Init() {
        if (GameController.instance != null) {
            GameController.instance.OnScoreUpdate += this.OnScoreUpdate;
        }
        this.OnScoreUpdate(0);
        this.Show();
    }

    private void OnPauseButtonClick() {
        this._callback?.Invoke("QuitGame");
    }

    private void OnScoreUpdate(int score) {
        ScoreText.text = "Score: "+score.ToString("D4");
    }

    private void OnDisable() {
        if (GameController.instance != null) {
            GameController.instance.OnScoreUpdate -= this.OnScoreUpdate;
        }
    }
}
