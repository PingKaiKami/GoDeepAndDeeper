using UnityEngine;

public class Submarine : Enemy
{
    public AudioManager audioManager;
    void OnEnable()
    {
        Rush();
        audioManager.Play(11, "sdSubmarine", false);
    }
}
