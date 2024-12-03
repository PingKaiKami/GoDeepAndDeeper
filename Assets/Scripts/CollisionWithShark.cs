using UnityEngine;

public class CollisionWithShark : MonoBehaviour
{
    private bool isAttack = false;
    private void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (OxygenController.Instance != null && !isAttack)
        {
            OxygenController.Instance.DecreaseOxygen(0.4f);
            isAttack = true;
        }
    }
}
