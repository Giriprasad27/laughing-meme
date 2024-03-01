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
    public Animator ScoreAnimator;

    private Action<string> _callback;
    private Coroutine comboEffectRoutine;

    private void Awake() {
        this.PauseButton.onClick.AddListener(OnPauseButtonClick);
    }

    public void Init(Action<string> callback) {
        this._callback = callback;
        this.SetScreen();
        this._Init();
    }

    public void EnableComboEffectUI(float timer, float val) {
        this.HeatModeValText.text = val.ToString() + "x";
        if (comboEffectRoutine != null) {
            StopCoroutine(comboEffectRoutine);
        }
        comboEffectRoutine = StartCoroutine(ActivateComboRewards(timer));
    }

    private IEnumerator ActivateComboRewards(float timer) {
        HeatModeObject.SetActive(true);
        yield return null;
        float elapsedTime = 0;
        while (elapsedTime < timer) {
            float sliderValue = Mathf.Lerp(1, 0, elapsedTime / timer);
            this.HeatModeSlider.value = sliderValue;
            yield return null;
            elapsedTime += Time.deltaTime;
        }
        this.HeatModeSlider.value = 0;
        this.HeatModeObject.SetActive(false);
    }

    private void _Init() {
        if (GameController.instance != null) {
            GameController.instance.OnScoreUpdate += this.OnScoreUpdate;
        }
        this.OnScoreUpdate(0);
        this.HeatModeObject.SetActive(false);
        this.Show();
    }

    private void OnPauseButtonClick() {
        this._callback?.Invoke("PauseGame");
        SoundController.instance.PlayOneShot("ButtonClick");
    }

    private void OnScoreUpdate(int score) {
        ScoreText.text = "Score: "+score.ToString("D4");
        ScoreAnimator.SetTrigger("ping");
    }

    private void OnDisable() {
        if (GameController.instance != null) {
            GameController.instance.OnScoreUpdate -= this.OnScoreUpdate;
        }
    }
}
