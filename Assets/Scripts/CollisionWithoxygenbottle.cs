using UnityEngine;

public class CollisionWithoxygenbottle : MonoBehaviour
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
            ValueController.Instance.IncreaseOxygen(1.0f);
            audioManager.Play(18, audioManager.sdOxygenbottle);
        }
    }
}
