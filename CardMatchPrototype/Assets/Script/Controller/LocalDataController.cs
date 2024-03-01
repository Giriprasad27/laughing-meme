using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalDataController : MonoBehaviour
{
    public static LocalDataController instance;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }
    public JSONObject GetValue(string key, JSONObject defaultVal = null) {
        string val = PlayerPrefs.GetString(key);
        if (val != null) {
            Debug.Log(val);
            return new JSONObject(val);
        }
        return defaultVal;
    }

    public void SetValue(string key, JSONObject val) {
        if (val != null && !val.IsNull) {
            PlayerPrefs.SetString(key, val.ToString());
        } else {
            PlayerPrefs.SetString(key, null);
        }
    }
}
