using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewCardData", menuName = "ScriptableObjects/CardData")]
public class CardObject : ScriptableObject {
    public Sprite icon;
    public string id;
    public int score;
    public CardType cardType;
    public int specialValue;
    public int specialtimer;
    public Sprite specialIcon;
}

public enum CardType {
    NormalCard = 0,
    TwoXCard = 1,
    ThreeXCard = 2
}
