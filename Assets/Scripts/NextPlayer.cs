using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextPlayer : MonoBehaviour
{
    public float speed = 1;
    private Animator animator;
    private AudioManager audioManager;
    private bool isPlayingFootstepSound;
    private float time = 0f;
    void Start() {
        animator = GetComponent<Animator>();
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }
    void Update()
    {
        animator.SetBool("Run", true);

        if (!isPlayingFootstepSound)
        {
            isPlayingFootstepSound = true;
            audioManager.Play(10, audioManager.sdFootBeach, true); // 讓腳步聲循環播放
        }
        time += Time.deltaTime;
        if(time >= 7){
            audioManager.Stop(10);
        }
        
        transform.position += new Vector3(-speed,0,0) * Time.deltaTime;
    }
}
