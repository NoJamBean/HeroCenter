using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionScript : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider MasteraudioSlider;
    public Slider BGMaudioSlider;
    public Slider SFXaudioSlider;


    public void MasterAudioControl()
    {
        float sound = MasteraudioSlider.value;

        if (sound == -40f) audioMixer.SetFloat("Master", -80);
        else audioMixer.SetFloat("Master", sound);
    }

    public void BGMControl()
    {
        float sound = BGMaudioSlider.value;

        if (sound == -40f) audioMixer.SetFloat("BGM", -80);
        else audioMixer.SetFloat("BGM", sound);
    }

    public void SFXControl()
    {
        float sound = SFXaudioSlider.value;

        if (sound == -40f) audioMixer.SetFloat("SFX", -80);
        else audioMixer.SetFloat("SFX", sound);
    }

    public void ToggleAudioVolume()
    {
        AudioListener.volume = AudioListener.volume == 0?1: 0;  
    }
}
