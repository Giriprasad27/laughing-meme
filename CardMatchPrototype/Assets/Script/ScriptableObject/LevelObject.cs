using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLevelData", menuName = "ScriptableObjects/LevelData")]
public class LevelObject : ScriptableObject {
    public string levelName;
    public Difficulty level;
    public int rowCount = 1;
    public int totalCardCount = 1;
    public float spacing = 20;
    public int Padding = 20;
}
