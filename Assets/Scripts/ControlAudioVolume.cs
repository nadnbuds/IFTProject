using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ControlAudioVolume: MonoBehaviour
{
    public AudioMixer mixer;
    public string volumeName;

    public void SetSound(float soundLevel)
    {
        mixer.SetFloat(volumeName, soundLevel);
    }
}
