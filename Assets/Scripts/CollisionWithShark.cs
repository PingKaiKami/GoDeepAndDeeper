using UnityEngine;

public class CollisionWithShark : MonoBehaviour
{
    private bool isAttack = false;
    public AudioManager audioManager;
    private void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isAttack)
        {
            ValueController.Instance.DecreaseOxygen(0.4f);
            isAttack = true;
            audioManager.Play(12,"sdharm", false);
        }
    }
}
