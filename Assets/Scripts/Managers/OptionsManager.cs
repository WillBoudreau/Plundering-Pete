using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{
    [Header("Options Values")]
    public Slider volumeSlider;
    public AudioSource musicSource;
    public GameObject MusicChanger;
    public Slider SFXSlider;
    public AudioSource SFXSource;


    // Start is called before the first frame update
    void Start()
    {
        volumeSlider.value = musicSource.volume;
        volumeSlider.onValueChanged.AddListener(SetVolume);
        SFXSlider.value = SFXSource.volume;
        SFXSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    // Update is called once per frame
    void Update()
    {
        musicSource = MusicChanger.GetComponent<AudioSource>();
        SFXSource = MusicChanger.GetComponent<AudioSource>();
    }
    //Set the volume of the music
    public void SetVolume(float volume)
    {
        musicSource.volume = volume;
        Debug.Log("Volume: " + volume);
    }
    //Set the volume of the sound effects
    public void SetSFXVolume(float volume)
    {
        SFXSource.volume = volume;
        Debug.Log("SFX Volume: " + volume);
    }
}
