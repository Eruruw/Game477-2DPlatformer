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

    const string Mixer_Soundtrack = "Soundtrack_Volume";
    const string Mixer_SFX = "SFX_Volume";

    void Awake()
    {
        Soundtrack_Slider.onValueChanged.AddListener(SetSoundtrackVolume);
        SFX_Slider.onValueChanged.AddListener(SetSFXVolume);
    }

    void SetSoundtrackVolume(float sliderVal)
    {
        Mixer.SetFloat(Mixer_Soundtrack, Mathf.Log10(sliderVal) * 20);
    }

    void SetSFXVolume(float sliderVal)
    {
        Mixer.SetFloat(Mixer_SFX, Mathf.Log10(sliderVal) * 20);
    }
}
