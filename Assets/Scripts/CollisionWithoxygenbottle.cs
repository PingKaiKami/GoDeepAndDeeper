using UnityEngine;

public class CollisionWithoxygenbottle : MonoBehaviour
{
    public AudioManager audioManager;
    private void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            ValueController.Instance.IncreaseOxygen(1.0f);
            audioManager.Play(9, "sdOxygenbottle", false);
        }
    }
}
