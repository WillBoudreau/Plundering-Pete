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
        effects[0] = Resources.Load<AudioClip>("CannonShot_Rework");
        effects[1] = Resources.Load<AudioClip>("GoldCoin");
        effects[2] = Resources.Load<AudioClip>("Plunder'inPeteDamage 1");
        PlaySceneTrack("MainMenuScene");

    }

    public void PlaySceneTrack(string sceneName)
    {
        Debug.Log("Playing music for scene: " + sceneName);
        switch(sceneName)
        {
            case "GameTestScene":
                Debug.Log("Playing music for scene 1 : " + sceneName);
                musicSource.clip = music[1];
                break;
            case "MainMenuScene":
                Debug.Log("Playing music for scene 3 : " + sceneName);
                musicSource.clip = music[0];
                break;
            default:
                Debug.Log("Playing music for scene 2 : " + sceneName);
                musicSource.clip = music[0];
                currentTrackIndex = (currentTrackIndex + 1) % music.Length;
                break;
        }
        musicSource.Play();
    }

    public void PlaySound(int index)
    {
        effectsSource.PlayOneShot(effects[index]);
    }
}
