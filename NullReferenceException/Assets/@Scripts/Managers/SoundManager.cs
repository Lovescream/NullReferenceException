using System;
using System.Collections.Generic;
using UnityEngine;

public enum SoundType
{
    HIT,
    DEAD,
    //TODO..
}

public class SoundManager : MonoBehaviour
{
    private Dictionary<string, AudioSource> musicSources = new Dictionary<string, AudioSource>();
    private Dictionary<string, AudioSource> effectSources = new Dictionary<string, AudioSource>();

    private bool isMusicMuted = false;
    private bool isEffectsMuted = false;

    private static SoundManager _instance;
    public static SoundManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<SoundManager>();
            }
            return _instance;
        }
    }

    // 배경음악 재생
    public void PlayMusic(SoundType key, bool loop = true)
    {
        Debug.Log(gameObject);
        if (!musicSources.TryGetValue(Enum.GetName(typeof(SoundType), key), out AudioSource musicSource))
        {
            Main.Resource.LoadAsync<AudioClip>(Enum.GetName(typeof(SoundType), key), clip =>
            {
                if (clip != null)
                {
                    musicSource = GetOrCreateAudioSource(musicSources, Enum.GetName(typeof(SoundType), key), loop);
                    musicSource.clip = clip;
                    musicSource.Play();
                }
                else
                {
                    Debug.LogError("로드하지 못함 : " + Enum.GetName(typeof(SoundType), key));
                }
            });
        }
        else
        {
            if (!musicSource.isPlaying)
            {
                musicSource.Play();
            }
        }
    }

    // 효과음 재생
    public void PlayEffect(SoundType key)
    {
        if (!effectSources.TryGetValue(Enum.GetName(typeof(SoundType), key), out AudioSource effectSource))
        {
            Main.Resource.LoadAsync<AudioClip>(Enum.GetName(typeof(SoundType), key), clip =>
            {
                if (clip != null)
                {
                    effectSource = GetOrCreateAudioSource(effectSources, Enum.GetName(typeof(SoundType), key), false);
                    effectSource.clip = clip;
                    effectSource.PlayOneShot(clip);
                }
                else
                {
                    Debug.LogError("로드하지 못함 : " + Enum.GetName(typeof(SoundType), key));
                }
            });
        }
        else
        {
            effectSource.PlayOneShot(effectSource.clip);
        }
    }

    private AudioSource GetOrCreateAudioSource(Dictionary<string, AudioSource> sources, string key, bool loop)
    {
        if (!sources.TryGetValue(key, out AudioSource source))
        {
            source = gameObject.AddComponent<AudioSource>();
            source.loop = loop;
            sources[key] = source;
        }
        return sources[key];
    }

}
