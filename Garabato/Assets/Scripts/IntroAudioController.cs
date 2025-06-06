using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroAudioController : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioClip introMusic;

    [System.Serializable]
    public class NamedAudioClip
    {
        public string name;
        public AudioClip clip;
    }

    public List<NamedAudioClip> soundEffects = new List<NamedAudioClip>();

    private Dictionary<string, AudioClip> soundEffectDict;

    void Awake()
    {
        soundEffectDict = new Dictionary<string, AudioClip>();
        foreach (var item in soundEffects)
        {
            if (!soundEffectDict.ContainsKey(item.name))
                soundEffectDict.Add(item.name, item.clip);
        }
    }

    public void PlayMusic()
    {
        musicSource.clip = introMusic;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlaySoundEffectByName(string name)
    {
        if (soundEffectDict.ContainsKey(name))
        {
            musicSource.PlayOneShot(soundEffectDict[name]);
        }
        else
        {
            Debug.LogWarning("Sound effect not found: " + name);
        }
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }
}