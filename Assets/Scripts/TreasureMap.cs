using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TreasureMap : MonoBehaviour
{
    public float speed = 1f;
    public Light2D gl;
    private GameObject mainCamera;
    private bool canRise = false;
    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    void Update()
    {
        if(canRise){
            transform.position += new Vector3(0,speed * Time.deltaTime,0);
        }
    }
    public void Appear(){
        Color color = GetComponent<SpriteRenderer>().color;
        color.a = 255;
        GetComponent<SpriteRenderer>().color = color;
        mainCamera.GetComponent<Camera_Move>().player = gameObject.transform;
        StartCoroutine(MoveLeft());
    }
    IEnumerator MoveLeft(){
        while(transform.position.x > 0){
            transform.position += new Vector3(-speed * 0.5f * Time.deltaTime,0,0);
            mainCamera.GetComponent<Camera>().orthographicSize += 0.05f * Time.deltaTime;
            gl.intensity += 0.005f * Time.deltaTime;
            yield return 0.1f;
        }
        canRise = true;
        while(mainCamera.GetComponent<Camera>().orthographicSize < 9){
            mainCamera.GetComponent<Camera>().orthographicSize += 0.05f * Time.deltaTime;
            gl.intensity += 0.0005f * Time.deltaTime;
            yield return 0.1f;
        }
        yield return 0;
    }
}

