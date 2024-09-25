using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicChanger : MonoBehaviour
{
    public AudioClip[] music;
    public  AudioSource audioSource;
    private int currentTrackIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        music = new AudioClip[3];
        music[0] = Resources.Load<AudioClip>("Sail The Seven Seas (1)");
        music[1] = Resources.Load<AudioClip>("Music2");
        music[2] = Resources.Load<AudioClip>("Music3");

        PlayNextTrack();
    }

    // Update is called once per frame
    void Update()
    {
        if (!audioSource.isPlaying)
        {
            PlayNextTrack();
        }
    }

    private void PlayNextTrack()
    {
        if (music.Length == 0) return;

        audioSource.clip = music[currentTrackIndex];
        audioSource.Play();

        currentTrackIndex = (currentTrackIndex + 1) % music.Length;
    }
}
