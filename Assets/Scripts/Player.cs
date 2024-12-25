using UnityEngine;
//for push
public class Player : PlayerController
{
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
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
