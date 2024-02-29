using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public CardGridCtrl CardGridCtrl;

    private void Start() {
        CardGridCtrl.Init(new CardGridOptions {
            rowCount = 5,
            totalCardCount = 40,
            spacing = 20,
            Padding = 20
        });
    }
}
