using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BGMPlayer : MonoBehaviour
{
    public AudioClip bgm;
    void Start()
    {
        var audio = gameObject.AddComponent<AudioSource>();
        audio.volume = 0.5f;
        audio.clip = bgm;
        audio.loop = true;
        audio.Play();
    }

}
