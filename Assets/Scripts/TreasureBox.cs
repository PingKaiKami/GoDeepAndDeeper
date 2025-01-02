using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TreasureBox : MonoBehaviour
{
    public float shakeMagnitude = 0.1f;
    public Sprite open;
    public GameObject map;
    public TreasureBoxSensor sensor;
    private Sprite close;
    private GameObject mainCamera;
    private GameObject player;
    private Camera_Move camera_script;
    private AudioManager audioManager;
    private bool isStarted = false;
    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        camera_script = mainCamera.GetComponent<Camera_Move>();
        player = GameObject.FindGameObjectWithTag("Player");
        close = GetComponent<SpriteRenderer>().sprite;
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }
    void Update(){
        if(Input.GetKeyDown(KeyCode.E) && sensor.isSensored && !isStarted){
            sensor.ui_word.SetActive(false);
            isStarted = true;
            StartCoroutine(ChangeCameraSize());
            //make camera focus on treasure box
            camera_script.player = gameObject.transform;
            GetComponent<SpriteRenderer>().sprite = open;
            GetComponent<PolygonCollider2D>().enabled = false;
        }
    }
    IEnumerator ChangeCameraSize(){
        player.GetComponent<PlayerController>().isSuck = true;
        while(mainCamera.GetComponent<Camera>().orthographicSize > 3){
            mainCamera.GetComponent<Camera>().orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, 2, Time.deltaTime * 0.7f);
            camera_script.TriggerShake(Time.deltaTime, 0.1f + Time.deltaTime * shakeMagnitude);
            yield return new WaitForSeconds(0.1f);
        }
        StartCoroutine(Eat());
    }   
    IEnumerator Eat(){
        audioManager.Play(24, audioManager.sdBite);
        GetComponent<SpriteRenderer>().sprite = close;
        player.GetComponent<PlayerController>().Disappear();
        yield return new WaitForSeconds(3);
        GetComponent<SpriteRenderer>().sprite = open;
        yield return new WaitForSeconds(3);
        map.GetComponent<TreasureMap>().Appear();
        yield return null;
    }
}
