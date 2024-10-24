using UnityEngine;

public class Player : PlayerController
{
    new void Start()
    {
        base.Start();
    }
    void Update()
    {
        Move();
    }
}
