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
}
