using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewData", menuName = "ScriptableObjects/ResourceData")]
public class ResourceData : ScriptableObject {

    public CardCtrl CardCtrl;
    public List<Sprite> IconSprites;
}
