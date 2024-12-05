using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicChanger : MonoBehaviour
{
    public AudioClip[] music;
    public AudioClip[] effects;
    public AudioClip[] damageEffects;
    public AudioSource musicSource;
    public AudioSource effectsSource;
    private int currentTrackIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        //Get the music sources
        AddMusicToArray();
        //Get the effects sources
        AddEffectsToArray();
        //Damage sound effects
        AddDamageEffectsToArray();
        PlaySceneTrack("MainMenuScene");

    }
    void AddMusicToArray()
    {
        music = new AudioClip[2];
        music[0] = Resources.Load<AudioClip>("Sail The Seven Seas (1)");
        music[1] = Resources.Load<AudioClip>("Sailing The Seven Seas");
    }
    void AddEffectsToArray()
    {
        effects = new AudioClip[4];
        effects[0] = Resources.Load<AudioClip>("CannonShot_Rework");
        effects[1] = Resources.Load<AudioClip>("coin-dropped-81172");
        effects[2] = Resources.Load<AudioClip>("Plunder'inPeteDamage 1");
        effects[3] = Resources.Load<AudioClip>("DeathSoundEffect");
    }
    void AddDamageEffectsToArray()
    {
        damageEffects = new AudioClip[5];
        damageEffects[0] = Resources.Load<AudioClip>("DamageSoundEffect1");
        damageEffects[1] = Resources.Load<AudioClip>("DamageSoundEffect2");
        damageEffects[2] = Resources.Load<AudioClip>("DamageSoundEffect3");
        damageEffects[3] = Resources.Load<AudioClip>("DamageSoundEffect4");
        damageEffects[4] = Resources.Load<AudioClip>("DamageSoundEffect5");
    }
    //Play the music for the scene
    public void PlaySceneTrack(string sceneName)
    {
        Debug.Log("Playing music for scene: " + sceneName);
        StopMusic();
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
                break;
        }
        musicSource.Play();
    }
    //Play the sound effect
    public void PlaySound(int index)
    {
        effectsSource.PlayOneShot(effects[index]);
    }
    public void PlayDamageSound(int index)
    {
        effectsSource.PlayOneShot(damageEffects[index]);
    }
    public void DevTools()
    {
        Debug.Log("Dev Tools");
        if(Input.GetKeyDown(KeyCode.M))
        {
            PlaySound(0);
        }
    }
    public void StopSound()
    {
       effectsSource.Stop();
    }
    public void StopMusic()
    {
        musicSource.Stop();
    }
}