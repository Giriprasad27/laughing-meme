using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class LevelSelectionScreenCtrl : UIScreenBaseCtrl {

    public Button GoButton;
    public Toggle EasyLevel;
    public Toggle IntermidateLevel;
    public Toggle ExpertLevel;
    public Toggle ProfessionalLevel;
    private Action<Difficulty> _callback;

    private Difficulty _selectedDifficulty;

    public void Init(Action<Difficulty> callback) {
        this._callback = callback;
        this.SetScreen();
        this._Init();
    }

    private void _Init() {
        //Set level toggle here
        this.Show();
    }


    private void Awake() {
        this.GoButton.onClick.AddListener(OnGoButtonClick);
        this.EasyLevel.onValueChanged.AddListener(OnEasySelected);
        this.IntermidateLevel.onValueChanged.AddListener(OnIntermidateSelected);
        this.ExpertLevel.onValueChanged.AddListener(OnExpertSelected);
        this.ProfessionalLevel.onValueChanged.AddListener(OnProfessionalSelected);
    }

    private void OnGoButtonClick() {
        this._callback?.Invoke(_selectedDifficulty);
    }

    private void OnEasySelected(bool val) {
        if (val) {
            _selectedDifficulty = Difficulty.Easy;
        }
    }

    private void OnIntermidateSelected(bool val) {
        if (val) {
            _selectedDifficulty = Difficulty.Intermidate;
        }
    }

    private void OnExpertSelected(bool val) {
        if (val) {
            _selectedDifficulty = Difficulty.Expert;
        }
    }

    private void OnProfessionalSelected(bool val) {
        if (val) {
            _selectedDifficulty = Difficulty.Professional;
        }
    }


}
