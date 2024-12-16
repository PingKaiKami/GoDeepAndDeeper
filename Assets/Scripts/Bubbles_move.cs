using UnityEngine;

public class Bubbles_move : MonoBehaviour
{
    public float speed = 1;
    private int type; //(1 for vertical, 0 for horizontal)
    void Start()
    {
        type = (transform.eulerAngles.z == 90) ? 0:1;
    }
    private float passTime = 0;
    public float existTime = 10f;
    void Update()
    {
        if(passTime >= existTime){
            Destroy(gameObject);
        }
        if(type == 0){
            transform.position += new Vector3(1, 0, 0) * speed * Time.deltaTime;
        }
        else{
            transform.position += new Vector3(0, 1, 0) * speed * Time.deltaTime;
        }
        passTime += Time.deltaTime;
    }
}
