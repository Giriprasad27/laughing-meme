using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewData", menuName = "ScriptableObjects/ResourceData")]
public class ResourceData : ScriptableObject {

    public CardGridCtrl CardGridCtrl;
    public CardCtrl CardCtrl;

    public List<CardObject> CardObjects;
    public List<LevelObject> LevelObjects;

    public MenuScreenCtrl MenuScreen;
    public LevelSelectionScreenCtrl LevelSelectionScreen;
    public InGameUIScreenCtrl InGameUIScreen; 
    public GameCompleteScreenCtrl GameCompleteScreen;
    public PauseGameScreenCtrl PauseGameScreen;

    private Dictionary<string, CardObject> _cardObjectsMap = new Dictionary<string, CardObject>();


    public void BuildCardNameMap() {
        foreach (CardObject card in this.CardObjects) {
            if (card != null) {
                if (!_cardObjectsMap.ContainsKey(card.name)) {
                    _cardObjectsMap.Add(card.name, card);
                }
            }
        }
    }
    public CardObject GetCardByName(string name) {
        CardObject card;
        _cardObjectsMap.TryGetValue(name, out card);
        return card;
    }
}
