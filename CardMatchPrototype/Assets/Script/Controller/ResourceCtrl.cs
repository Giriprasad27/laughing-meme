using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceCtrl : MonoBehaviour
{
    public static ResourceCtrl instance;
    public ResourceData ResourceData;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }
}
