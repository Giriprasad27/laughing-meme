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

    public List<AudioClip> AudioClips;

    private Dictionary<string, AudioClip> _audioClipMap = new Dictionary<string, AudioClip>();

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

    public void BuildAudioClipMap() {
        foreach (AudioClip audioClip in this.AudioClips) {
            if (audioClip != null) {
                if (!_audioClipMap.ContainsKey(audioClip.name)) {
                    _audioClipMap.Add(audioClip.name, audioClip);
                }
            }
        }
    }
    public AudioClip GetAudioByName(string name) {
        AudioClip audioClip;
        _audioClipMap.TryGetValue(name, out audioClip);
        return audioClip;
    }
}
