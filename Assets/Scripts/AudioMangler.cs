using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioMangler : MonoBehaviour
{
    [SerializeField] AudioMixer Mixer;
    [SerializeField] Slider SFX_Slider;
    [SerializeField] Slider Soundtrack_Slider;

    const string Mixer_SoundtrackVolume = "Soundtrack_Volume";
    const string Mixer_SoundtrackPitch = "Soundtrack_Pitch";
    const string Mixer_SFXVolume = "SFX_Volume";
    const string Mixer_SFXPitch = "SFX_Pitch";

    void Awake()
    {
        Soundtrack_Slider.onValueChanged.AddListener(SetSoundtrackVolume);
        SFX_Slider.onValueChanged.AddListener(SetSFXVolume);
    }

    void SetSoundtrackVolume(float sliderVal)
    {
        Mixer.SetFloat(Mixer_SoundtrackVolume, Mathf.Log10(sliderVal) * 20);
        Mixer.SetFloat(Mixer_SoundtrackPitch, sliderVal);
    }

    void SetSFXVolume(float sliderVal)
    {
        Mixer.SetFloat(Mixer_SFXVolume, Mathf.Log10(sliderVal) * 20);
        Mixer.SetFloat(Mixer_SFXPitch, sliderVal);
    }
}
