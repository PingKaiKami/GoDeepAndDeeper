using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// hello
public class BackgroundController : MonoBehaviour
{
    private Vector2 startPos;
    private float length;
    public GameObject cam;
    public float parallaxEffect;

    // 新增變數來限制背景生成
    public float upperLimitY; // 背景生成的上限 y 值

    void Start()
    {
        startPos = new Vector2(transform.position.x, transform.position.y);
        length = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float distance = cam.transform.position.y * parallaxEffect;
        float movement = cam.transform.position.y * (1 - parallaxEffect);

        transform.position = new Vector3(transform.position.x, startPos.y + distance, transform.position.z);

        // 限制背景在上限範圍內生成
        if (movement > startPos.y + length && startPos.y + length < upperLimitY)
        {
            startPos.y += length;
        }
        else if (movement < startPos.y - length)
        {
            startPos.y -= length;
        }
    }
}
