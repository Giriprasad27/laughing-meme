using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewData", menuName = "ScriptableObjects/ResourceData")]
public class ResourceData : ScriptableObject {

    public CardGridCtrl CardGridCtrl;
    public CardCtrl CardCtrl;

    public List<Sprite> IconSprites;
    public List<LevelObject> LevelObjects;

    public MenuScreenCtrl MenuScreen;
    public InGameUIScreenCtrl inGameUIScreen;
}
