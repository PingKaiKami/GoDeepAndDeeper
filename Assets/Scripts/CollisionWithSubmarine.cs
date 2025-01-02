using UnityEngine;

public class CollisionWithSubmarine : MonoBehaviour
{
    private bool isAttack = false;
    private AudioManager audioManager;
    private void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isAttack)
        {
            ValueController.Instance.DecreaseOxygen(0.5f);
            isAttack = true;
            audioManager.Play(21, audioManager.sdharm);
        }
    }
}
