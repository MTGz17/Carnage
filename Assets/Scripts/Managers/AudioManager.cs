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
        if (SaveManager.instance != null)
        {
            masterVol.value = SaveManager.instance.masterVolume;
            musicVol.value = SaveManager.instance.musicVolume;
            sFXVol.value = SaveManager.instance.sfxVolume;
        }

        ChangeMasterVolume();
        ChangeMusicVolume();
        ChangeSFXVolume();
    }

    public void ChangeMasterVolume()
    {
        mainAudioMixer.SetFloat("MasterVol", masterVol.value);

        if (SaveManager.instance != null)
        {
            SaveManager.instance.masterVolume = masterVol.value;
            SaveManager.instance.Save();
        }
    }

    public void ChangeMusicVolume()
    {
        mainAudioMixer.SetFloat("MusicVol", musicVol.value);

        if (SaveManager.instance != null)
        {
            SaveManager.instance.musicVolume = musicVol.value;
            SaveManager.instance.Save();
        }
    }

    public void ChangeSFXVolume()
    {
        mainAudioMixer.SetFloat("SFXVol", sFXVol.value);

        if (SaveManager.instance != null)
        {
            SaveManager.instance.sfxVolume = sFXVol.value;
            SaveManager.instance.Save();
        }
    }

    public void ButtonSound()
    {
        if (uIAudioSource != null)
        {
            uIAudioSource.Play();
        }
    }
}