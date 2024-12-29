using UnityEngine;

// hello
public class BackgroundControllerX : MonoBehaviour
{
    private GameObject player;
    private Vector2 startPos;
    private float length;
    public GameObject cam;
    public float parallaxEffect;
    public float minX;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        startPos = new Vector2 (transform.position.x,transform.position.y);
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(player.transform.position.x > minX){
            float distance = cam.transform.position.x * parallaxEffect;
            float movement = cam.transform.position.x * (1 - parallaxEffect);

            transform.position = new Vector3(startPos.x + distance, transform.position.y, transform.position.z);
            if(movement > startPos.x + length) 
                startPos.x += length;
            else if(movement < startPos.x - length) 
                startPos.x -= length;
        }
    }
}
