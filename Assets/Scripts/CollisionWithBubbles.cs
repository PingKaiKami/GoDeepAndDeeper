using UnityEngine;

public class CollisionWithBubbles : MonoBehaviour
{
    private void Start()
    {

    }
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ValueController.Instance.IncreaseOxygen(0.20f * Time.deltaTime);
            ValueController.Instance.IncreaseHeartRate(0.2f * Time.deltaTime);
        }
    }
}
