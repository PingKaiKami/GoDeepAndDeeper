using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
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
    private PlayerController player;
    private bool isGameOver = false;
    private bool isEnd = false;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        if(player.isGameOver && !isGameOver){
            StartCoroutine(GameOver());
            isGameOver = true;
        }
        if(map.isEnd && !isEnd){
            StartCoroutine(End());
            isEnd = true;
        }
    }
    IEnumerator GameOver(){
        ui_heart.SetActive(false);
        ui_depthplay.SetActive(false);
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
