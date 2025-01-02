using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject ui_blackBG;
    public GameObject ui_heart;
    public GameObject ui_depthplay;
    public GameObject credit_names;
    public GameObject credit_jobs;
    public Volume volume;
    public TreasureMap map;
    public Light2D gl;
    private GameObject player;
    private PlayerController playerScript;
    private bool isGameOver = false;
    private bool isEnd = false;
    private int level = 0;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<PlayerController>();
    }

    void Update()
    {
        level = CheckLevel();

        ChangeLevelPref(level);
        //for debug

        if(!playerScript.isAlive()){
            if(Input.GetKeyDown(KeyCode.LeftControl)){
                playerScript.curRisingSpeed += 5;
            }
        }

        if(playerScript.isGameOver && !isGameOver){
            StartCoroutine(GameOver());
            isGameOver = true;
        }
        if(map.isEnd && !isEnd){
            StartCoroutine(End());
            isEnd = true;
        }
    }
    private int CheckLevel(){
        if(player.activeSelf){
            if(player.transform.position.y < -1150){
                return 4;
            }
            else if(player.transform.position.y < -807){
                return 3;
            }
            else if(player.transform.position.y < -518){
                return 2;
            }
            else if(player.transform.position.y < 16){
                return 1;
            }
        }
        return 0;
    }
    private bool change1 = false;
    private bool change2 = false;
    private bool isTrigger1 = false;
    private bool isTrigger2 = false;
    private bool isTrigger3 = false;
    private bool isTrigger4 = false;
    private void ChangeLevelPref(int level){
        if(level == 0){
            gl.intensity = 1;
            if (volume.profile.TryGet(out Bloom bloom))
            {
                bloom.threshold.value = 0.5f;
                bloom.intensity.value = 3;
            }
            isTrigger1 = false;
        }
        else if(level == 1){
            gl.intensity = 3;
            if (volume.profile.TryGet(out Bloom bloom))
            {
                bloom.threshold.value = 0.5f;
                bloom.intensity.value = 1;
            }
            
            change1 = true;
            if(!isTrigger1){
                StartCoroutine(ChangeCF());
                isTrigger1 = true;
            }
            isTrigger2 = false;
            
        }
        else if(level == 2){
            change1 = false;
            isTrigger1 = false;
            if(!isTrigger2){
                StartCoroutine(ChangeCF());
                isTrigger2 = true;
            }
        }
        else if(level == 3){
            isTrigger2 = false;
            isTrigger4 = false;
            change2 = false;
            if(!isTrigger3){
                StartCoroutine(ChangeContrast());
                isTrigger3 = true;
            }
        }
        else if(level == 4){
            isTrigger3 = false;
            change2 = true;
            if(!isTrigger4){
                StartCoroutine(ChangeContrast());
                isTrigger4 = true;
            }
            return;
        }
    }
    IEnumerator ChangeCF(){
        while(change1){
            if (volume.profile.TryGet(out ColorAdjustments ca))
            {
                Color color = ca.colorFilter.value;
                if(color.r <= 0){
                    break;
                }
                color.r -= 0.01f;
                ca.colorFilter.value = color;
            }
            yield return new WaitForSeconds(0.1f);
        }
        while(!change1){
            if (volume.profile.TryGet(out ColorAdjustments ca))
            {
                Color color = ca.colorFilter.value;
                if(color.r >= 1){
                    break;
                }
                color.r += 0.01f;
                ca.colorFilter.value = color;
                
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
    IEnumerator ChangeContrast(){
        while(change2){
            if (volume.profile.TryGet(out ColorAdjustments ca))
            {
                if(ca.contrast.value >= 90){
                    break;
                }
                ca.contrast.value += 1f;
            }
            yield return new WaitForSeconds(0.1f);
        }
        while(!change2){
            if (volume.profile.TryGet(out ColorAdjustments ca))
            {
                if(ca.contrast.value <= 0){
                    break;
                }
                ca.contrast.value -= 1f;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
    IEnumerator GameOver(){
        ui_heart.SetActive(false);
        ui_depthplay.SetActive(false);
        change2 = false;
        StartCoroutine(ChangeContrast());
        yield return new WaitForSeconds(20f);
        credit_jobs.GetComponent<Credits>().canPlay = true;
        credit_names.GetComponent<Credits>().canPlay = true;
    }

    IEnumerator End(){
        ui_blackBG.SetActive(true);
        yield return new WaitForSeconds(3f);
        //switch scene
        SceneManager.LoadSceneAsync("Menu");
    }
}
