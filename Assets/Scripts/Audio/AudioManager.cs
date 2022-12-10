using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioManager
{
    static bool initialized = false;
    static Dictionary<AudioName, AudioClip> audioClips = new Dictionary<AudioName, AudioClip>();
    static AudioSource audioSource;

    public static bool Instance
    {
        get { return initialized; }
    }

    public static void Initialze(AudioSource source)
    {
        initialized = true;
        audioSource = source;
        audioClips.Add(AudioName.AkaReload, Resources.Load<AudioClip>("AkaReload"));
        audioClips.Add(AudioName.AkaShoot, Resources.Load<AudioClip>("AkShoot"));
        audioClips.Add(AudioName.PistolShoot, Resources.Load<AudioClip>("PistolShoot"));
        audioClips.Add(AudioName.PistolReload, Resources.Load<AudioClip>("PistolReload"));
        audioClips.Add(AudioName.PlayerHited, Resources.Load<AudioClip>("PlayerHit"));
        audioClips.Add(AudioName.ZombieHitted, Resources.Load<AudioClip>("HitZombie"));
        audioClips.Add(AudioName.ZombieDead, Resources.Load<AudioClip>("ZombieDie"));
        audioClips.Add(AudioName.ButtonClick, Resources.Load<AudioClip>("Button"));
    }

    public static void Play(AudioName name)
    {
        audioSource.PlayOneShot(audioClips[name]);

    }

    public static AudioClip GetClip(AudioName name)
    {
        return (audioClips[name]);
    }

}
