using UnityEngine;

public class CollisionWithGarbage : MonoBehaviour
{
    private AudioManager audioManager;
    private void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            ValueController.Instance.DecreaseMaxOxygen(0.1f);
            ValueController.Instance.DecreaseEnergy(0.2f);
            audioManager.Play(23, audioManager.sdGarbage);
        }
    }
}
