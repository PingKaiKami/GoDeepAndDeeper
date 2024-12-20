using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TreasureBox : MonoBehaviour
{
    public Sprite open;
    private GameObject mainCamera;
    private GameObject player;
    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionStay2D(Collision2D other) {
        if(other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.E)){
            Camera_Move camera_script = mainCamera.GetComponent<Camera_Move>();
            StartCoroutine(ChangeCameraSize());
            camera_script.player = gameObject.transform;
            GetComponent<SpriteRenderer>().sprite = open;
            GetComponent<PolygonCollider2D>().enabled = false;
        }
    }
    IEnumerator ChangeCameraSize(){
        player.GetComponent<PlayerController>().isSuck = true;
        while(mainCamera.GetComponent<Camera>().orthographicSize > 3){
            mainCamera.GetComponent<Camera>().orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, 2, Time.deltaTime * 0.5f);
            yield return new WaitForSeconds(0.1f);
        }
        StartCoroutine(Suck());
    }   
    IEnumerator Suck(){
        
        yield return 0;
    }
}
