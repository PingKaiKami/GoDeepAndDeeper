using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup bgmSource;
    [SerializeField] private AudioMixerGroup sfxSource;
    //0
    public AudioClip bgmMain;
    public AudioClip bgmBeach;
    public AudioClip bgmWater;
    public AudioClip bgmEnd;

    //10
    public AudioClip sdFootBeach;
    public AudioClip sdMap;
    public AudioClip smoke;
    public AudioClip sdIntoWater;
    public AudioClip sdSwim;
    //15
    public AudioClip sdBoom;
    public AudioClip sdMapClose;
    public AudioClip sdPopo;
    public AudioClip sdOxygenbottle;
    public AudioClip sdShark;
    //20
    public AudioClip sdSubmarine;
    public AudioClip sdharm;
    public AudioClip sdIsred;
    public AudioClip sdGarbage;
    public AudioClip sdBite;
    
    private int numSd = 30;
    private GameObject player;
    List<AudioSource> audios = new List<AudioSource>();

    //change volume
    public int debugIndex = 2;
    public bool isPlay = false; // for debug
    private bool isPlayed = false;
    private bool isPlayed2 = false;
    private List<AudioClip> audioClips = new List<AudioClip>();
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        for (int i = 0; i < numSd; ++i)
        {
            var audio = gameObject.AddComponent<AudioSource>();
            audio.outputAudioMixerGroup = (i <= 4) ? bgmSource : sfxSource;
            audios.Add(audio);
        }

    }
    void Start()
    {
        // 添加所有音效到列表
        audioClips.Add(bgmMain);
        audioClips.Add(bgmBeach);
        audioClips.Add(bgmWater);
        audioClips.Add(bgmEnd);
        //padding
        audioClips.Add(bgmEnd);
        audioClips.Add(bgmEnd);
        audioClips.Add(bgmEnd);
        audioClips.Add(bgmEnd);
        audioClips.Add(bgmEnd);
        audioClips.Add(bgmEnd);
        //padding
        audioClips.Add(sdFootBeach);
        audioClips.Add(sdMap);
        audioClips.Add(smoke);
        audioClips.Add(sdIntoWater);
        audioClips.Add(sdSwim);
        audioClips.Add(sdBoom);
        audioClips.Add(sdMapClose);
        audioClips.Add(sdPopo);
        audioClips.Add(sdOxygenbottle);
        audioClips.Add(sdShark);
        audioClips.Add(sdSubmarine);
        audioClips.Add(sdharm);
        audioClips.Add(sdIsred);
        audioClips.Add(sdGarbage);
        audioClips.Add(sdBite);

        SetVolume(1, 0.8f);
        SetVolume(2, 0.5f);
        SetVolume(12, 0.5f);
        SetVolume(15, 0.1f);
        SetVolume(19, 0.8f);
        SetVolume(20, 0.5f);
        SetVolume(21, 0.2f);
        SetVolume(22, 0.5f);
        SetVolume(24, 0.2f);
    }
    //playing bgm
    void Update(){
        //for debug
        if(isPlay){
            Play(debugIndex, audioClips[debugIndex]);
            isPlay = false;
        }
        if(player.GetComponent<PlayerController>().isAlive()){
            //on beach
            if(!player.activeSelf){
                if(!audios[1].isPlaying){
                    Play(1, bgmBeach, true);
                }
            }
            //in water
            else{
                Stop(1);
                Stop(3);
                if(!audios[2].isPlaying){
                    Play(2, bgmWater, true);
                }
            }
        }
        //player died
        else{
            //game resume
            if(player.activeSelf){
                Stop(2);
                if(!audios[3].isPlaying){
                    Play(3, bgmEnd, true);
                }
            }
            //game over
            else{
                Stop(2);
                if(!audios[3].isPlaying){
                    if(!isPlayed){
                        Play(3, bgmEnd);
                        isPlayed = true;
                    }
                    //play main_theme after play bgmEnd once
                    else{
                        if(!audios[0].isPlaying && !isPlayed2){
                            Play(0, bgmMain, true);
                            isPlayed2 = true;
                        }
                    }
                }
            }
        }
    }

    public void Play(int index, AudioClip clip, bool isLoop = false)
    {
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
        if (index >= 0 && index < audios.Count && audios[index].isPlaying)
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
}
