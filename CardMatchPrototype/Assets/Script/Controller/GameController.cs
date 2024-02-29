using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public static GameController instance;
    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    public ResourceData ResourceData;
    public CardGridCtrl CardGridCtrl;

    private void Start() {
        CardGridCtrl.Init(new CardGridOptions {
            rowCount = 6,
            totalCardCount = 36,
            spacing = 20,
            Padding = 20
        });
    }
}
