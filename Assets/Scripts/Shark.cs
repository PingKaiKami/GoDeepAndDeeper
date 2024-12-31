using UnityEngine;

public class Shark : Enemy
{
    AudioManager audioManager;
    void OnEnable()
    {
        Rush();
        audioManager.Play(10, "sdShark", false);
    }
}
