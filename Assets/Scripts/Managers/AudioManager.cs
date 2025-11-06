using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Mixer & Sliders")]
    [SerializeField] private Slider masterVol;
    [SerializeField] private Slider musicVol;
    [SerializeField] private Slider sFXVol;
    [SerializeField] private AudioMixer mainAudioMixer;

    [Header("UI Sound")]
    [SerializeField] private AudioSource uIAudioSource;

    private void Start()
    {
        masterVol.value = PlayerPrefs.GetFloat("MasterVol", 0f);
        musicVol.value = PlayerPrefs.GetFloat("MusicVol", 0f);
        sFXVol.value = PlayerPrefs.GetFloat("SFXVol", 0f);

        ChangeMasterVolume();
        ChangeMusicVolume();
        ChangeSFXVolume();
    }

    public void ChangeMasterVolume()
    {
        mainAudioMixer.SetFloat("MasterVol", masterVol.value);
        PlayerPrefs.SetFloat("MasterVol", masterVol.value);
    }

    public void ChangeMusicVolume()
    {
        mainAudioMixer.SetFloat("MusicVol", musicVol.value);
        PlayerPrefs.SetFloat("MusicVol", musicVol.value);
    }

    public void ChangeSFXVolume()
    {
        mainAudioMixer.SetFloat("SFXVol", sFXVol.value);
        PlayerPrefs.SetFloat("SFXVol", sFXVol.value);
    }

    public void ButtonSound()
    {
        if (uIAudioSource != null)
        {
            uIAudioSource.Play();
        }
    }
}