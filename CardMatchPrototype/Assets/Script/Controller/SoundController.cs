using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public static SoundController instance;
    AudioSource _sfxSource;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        this._sfxSource = this.GetComponent<AudioSource>();
    }

    private void Start() {
        ResourceCtrl.instance.ResourceData.BuildAudioClipMap();
    }

    public void PlayOneShot(string audioname) {
        AudioClip audioClip = ResourceCtrl.instance.ResourceData.GetAudioByName(audioname);
        if (audioClip != null) {
            _sfxSource.PlayOneShot(audioClip);
        }
    }
}
