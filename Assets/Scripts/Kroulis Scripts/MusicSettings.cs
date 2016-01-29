using UnityEngine;
using System.Collections;

public class MusicSettings : MonoBehaviour {

    [Range(0f, 1f)]
    public float music = 1;
    [Range(0f, 1f)]
    public float sound=1;
    [Range(0f, 1f)]
    public float dialogue = 1;

    void Updata()
    {
        Globe.music_volume = music;
        Globe.sound_volume = sound;
        Globe.dialogue_volume = dialogue;
    }

    public void SetMusicVolume(float value)
    {
        if(value>1)
        {
            Debug.Log("Music Volume cannot more than 100%.");
        }
        else
        {
            music = value;
        }
    }

    public void SetSoundVolume(float value)
    {
        if (value > 1)
        {
            Debug.Log("Sound Volume cannot more than 100%.");
        }
        else
        {
            sound = value;
        }
    }

    public void SetDialogVolume(float value)
    {
        if (value > 1)
        {
            Debug.Log("Dialogue Volume cannot more than 100%.");
        }
        else
        {
            dialogue = value;
        }
    }
}
