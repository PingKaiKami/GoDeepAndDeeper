using UnityEngine;
//for push
public class Player : PlayerController
{
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }
    void Update()
    {
        if(isAlive() && !isdied){
            Move();
            if(isSuck){
                Suck();
            }
        }
        else{
            Died();
        }
    }
}
