using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TreasureMap : MonoBehaviour
{
    public float seaSurface = 15.5f;
    public float speed = 1f;
    public bool isEnd = false;
    public Light2D gl;
    public GameObject nextPlayer;
    private GameObject mainCamera;
    private AudioManager audioManager;
    private bool canRise = false;
    private bool isOnSea = false;
    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    void Update()
    {
        if(canRise){
            transform.position += new Vector3(0,speed * Time.deltaTime,0);
            if(speed <= 6f){
                speed += 0.02f * Time.deltaTime;
            }
            if(transform.position.y >= seaSurface){
                canRise = false;
                isOnSea = true;
            }
        }
        if(isOnSea){
            audioManager.Stop(0);
            audioManager.Play(1, audioManager.bgmBeach, true);
            StartCoroutine(MoveRight());
            isOnSea = false;
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
            transform.position += new Vector3(-speed * Time.deltaTime,0,0);
            mainCamera.GetComponent<Camera>().orthographicSize += 0.05f * Time.deltaTime;
            gl.intensity += 0.005f * Time.deltaTime;
            yield return new WaitForSeconds(0.01f);
        }
        canRise = true;
        while(mainCamera.GetComponent<Camera>().orthographicSize < 9){
            mainCamera.GetComponent<Camera>().orthographicSize += 0.05f * Time.deltaTime;
            gl.intensity += 0.0005f * Time.deltaTime;
            yield return new WaitForSeconds(0.01f);
        }
        yield return null;
    }
    IEnumerator MoveRight(){
        mainCamera.GetComponent<Camera_Move>().maxX = 100;
        speed = 3f;
        while(transform.position.x < 12.8f){
            mainCamera.GetComponent<Camera>().orthographicSize -= 0.05f * Time.deltaTime;
            transform.position += new Vector3(speed * Time.deltaTime,0,0);
            yield return new WaitForSeconds(0.01f);
        }
        float time = 0f;
        while(time <= 1.5f){
            mainCamera.GetComponent<Camera>().orthographicSize -= 0.05f * Time.deltaTime;
            transform.position += new Vector3(0.02f, 0.03f, 0);
            time += 0.01f;
            yield return new WaitForSeconds(0.01f);
        }
        while(time <= 2.7f){
            mainCamera.GetComponent<Camera>().orthographicSize -= 0.05f * Time.deltaTime;
            transform.position += new Vector3(0.02f, -0.02f, 0);
            time += 0.01f;
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(3f);
        nextPlayer.SetActive(true);
        yield return new WaitForSeconds(7f);
        //End
        isEnd = true;
    }
}

