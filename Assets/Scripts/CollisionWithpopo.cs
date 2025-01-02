using UnityEngine;

public class CollisionWithpopo : MonoBehaviour
{
    private AudioManager audioManager;
    private void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            ValueController.Instance.IncreaseOxygen(0.20f);
            audioManager.Play(17, audioManager.sdPopo);
        }
    }
}
