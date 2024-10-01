using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{
    public Slider volumeSlider;
    public AudioSource musicSource;
    public GameObject MusicChanger;

    // Start is called before the first frame update
    void Start()
    {
        volumeSlider.value = musicSource.volume;
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    // Update is called once per frame
    void Update()
    {
        musicSource = MusicChanger.GetComponent<AudioSource>();
    }

    public void SetVolume(float volume)
    {
        musicSource.volume = volume;
        Debug.Log("Volume: " + volume);
    }
}
