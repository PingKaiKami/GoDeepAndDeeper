using UnityEngine;

public class Submarine : Enemy
{
    void OnEnable()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        Rush();
    }
}
