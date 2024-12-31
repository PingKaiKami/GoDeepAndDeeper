using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class AudioManager : MonoBehaviour
{
    public AudioClip bgmMain;
    public AudioClip sdFootBeach;
    public AudioClip sdMap;
    public AudioClip smoke;
    public AudioClip sdIntoWater;
    public AudioClip sdSwim;
    public AudioClip sdBoom;
    public AudioClip sdMapClose;
    public AudioClip sdPopo;
    public AudioClip sdOxygenbottle;
    public AudioClip sdShark;
    public AudioClip sdSubmarine;
    public AudioClip sdharm;
    public AudioClip sdIsred;
    public AudioClip sdGarbage;
    private int numSd = 15;
    List<AudioSource> audios = new List<AudioSource>();
    private void Awake()
    {
        for (int i = 0; i < numSd; ++i)
        {
            var audio = this.gameObject.AddComponent<AudioSource>();
            audios.Add(audio);
        }

    }
    void Start()
    {
        SetVolume(0, 0.35f);
    }

    public void Play(int index, string name, bool isLoop)
    {
        var clip = GetAudioClip(name);
        if (clip != null)
        {
            var audio = audios[index];
            audio.clip = clip;
            audio.loop = isLoop;
            audio.Play();
        }
    }
    public void Stop(int index)
    {
        if (index >= 0 && index < audios.Count)
        {
            audios[index].Stop();
        }
    }
    
    public void SetVolume(int index, float volume)
{
    if (index >= 0 && index < audios.Count)
    {
        audios[index].volume = volume;
    }
}

    AudioClip GetAudioClip(string name)
    {
        switch (name)
        {
            case "bgmMain":
                return bgmMain;
            case "sdFootBeach":
                return sdFootBeach;
            case "sdMap":
                return sdMap;
            case "sdMapClose":
                return sdMapClose;
            case "smoke":
                return smoke;
            case "sdIntoWater":
                return sdIntoWater;
            case "sdSwim":
                return sdSwim;
            case "sdBoom":
                return sdBoom;
            case "sdPopo":
                return sdPopo;
            case "sdOxygenbottle":
                return sdOxygenbottle;
            case "sdShark":
                return sdShark;
            case "sdSubmarine":
                return sdSubmarine;
            case "sdharm":
                return sdharm;
            case "sdIsred":
                return sdIsred;
            case "sdGarbage":
                return sdGarbage;
        }
        return null;
    }
}