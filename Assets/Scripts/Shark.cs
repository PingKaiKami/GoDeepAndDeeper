using UnityEngine;

public class Shark : Enemy
{
    
    void OnEnable()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        Rush();
    }
}
