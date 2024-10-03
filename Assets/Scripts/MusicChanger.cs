using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicChanger : MonoBehaviour
{
    public AudioClip[] music;
    public AudioClip[] effects;
    public AudioSource musicSource;
    public AudioSource effectsSource;
    private int currentTrackIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        music = new AudioClip[3];
        music[0] = Resources.Load<AudioClip>("Sail The Seven Seas (1)");
        music[1] = Resources.Load<AudioClip>("Sailing The Seven Seas");
        music[2] = Resources.Load<AudioClip>("Music3");
        effects = new AudioClip[3];
        effects[0] = Resources.Load<AudioClip>("CanonShot_");
        effects[1] = Resources.Load<AudioClip>("Effect2");
        effects[2] = Resources.Load<AudioClip>("Effect3");

        PlayNextTrack();
    }

    // Update is called once per frame
    void Update()
    {
        if (!musicSource.isPlaying)
        {
            PlayNextTrack();
        }
    }

    public void PlayNextTrack()
    {
        if (music.Length == 0) return;

        musicSource.clip = music[currentTrackIndex];
        musicSource.Play();

        currentTrackIndex = (currentTrackIndex + 1) % music.Length;
    }

    public void PlaySound(int index)
    {
        effectsSource.PlayOneShot(effects[index]);
    }
}
