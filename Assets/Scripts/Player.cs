using UnityEngine;

public class Player : PlayerController
{
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        Move();
    }
}
