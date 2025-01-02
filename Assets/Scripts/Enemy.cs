using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Vector3 playerPos;
    Vector3 enemyPos;
    public GameObject underRedArea;
    public GameObject redArea;
    private bool isRush = false;
    protected private AudioManager audioManager;
        
    // private ObjectPool<Enemy> underRedPool;
    // private ObjectPool<Enemy> redPool;
    // void Start() {
    //     redPool = new ObjectPool<Enemy>();
    //     underRedPool = new ObjectPool<Enemy>();
    //     redPool.InitPool(redArea, 10);
    //     underRedPool.InitPool(underRedArea, 10);
    // }

    protected private void Rush(){
        if(!isRush){
            StartCoroutine(WaitRush());
            isRush = true;
        }
    }
    IEnumerator WaitRush(){
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;
        int randomAngle = UnityEngine.Random.Range(0,360);
        // int randomAngle = (int)gameObject.transform.eulerAngles.z + 180;
        float angleInRadians = randomAngle * Mathf.Deg2Rad;
        float offsetX = (float)(30 * Math.Cos(angleInRadians));
        float offsetY = (float)(30 * Math.Sin(angleInRadians));
        Quaternion areaRotation = Quaternion.Euler(0, 0, randomAngle);
        if(randomAngle <= 90 || randomAngle >= 270){
            Quaternion enemyRotation = Quaternion.Euler(0, 0, randomAngle);
            transform.rotation = enemyRotation;
            transform.localScale = new Vector3(1,1,1);
        }
        else if(randomAngle <= 180){
            Quaternion enemyRotation = Quaternion.Euler(0, 0, randomAngle + 180);
            transform.rotation = enemyRotation;
            transform.localScale = new Vector3(-1,1,1);
        }
        else{
            Quaternion enemyRotation = Quaternion.Euler(0, 0, randomAngle - 180);
            transform.rotation = enemyRotation;
            transform.localScale = new Vector3(-1,1,1);
        }
        

        enemyPos = new Vector3(playerPos.x + offsetX, playerPos.y + offsetY, playerPos.z);
        transform.position = enemyPos;
        GameObject URA = Instantiate(underRedArea, playerPos, areaRotation);
        GameObject RA = Instantiate(redArea, enemyPos, areaRotation);
        // GameObject URA = underRedPool.Spawn(playerPos, areaRotation).gameObject;
        // GameObject RA = redPool.Spawn(enemyPos, areaRotation).gameObject;
        if(gameObject.tag == "Shark"){
            URA.transform.localScale = new Vector3(100, 2, 1);
            RA.transform.localScale = new Vector3(30, 2, 1);
            StartCoroutine(RAMoving(URA, RA));
        }
        else if(gameObject.tag == "Submarine"){
            URA.transform.localScale = new Vector3(100, 7, 1);
            RA.transform.localScale = new Vector3(30, 7, 1);
            StartCoroutine(RAMoving(URA, RA));
        }
        yield return new WaitForSeconds(10);
        
        isRush = false;
    }
    IEnumerator RAMoving(GameObject URA, GameObject RA){
        float distance = Vector3.Distance(URA.transform.position, RA.transform.position);
        float offsetX = (URA.transform.position.x - enemyPos.x)/300;
        float offsetY = (URA.transform.position.y - enemyPos.y)/300;
        while(distance > 1){
            distance = Vector3.Distance(URA.transform.position, RA.transform.position);
            RA.transform.position += new Vector3(offsetX, offsetY, 0);
            yield return new WaitForSeconds(0.01f);
        }
        if(gameObject.tag == "Shark"){
            audioManager.Play(19, audioManager.sdShark);
            StartCoroutine(EnemyRushing(URA, RA, 1f));
        }
        else if(gameObject.tag == "Submarine"){
            audioManager.Play(20, audioManager.sdSubmarine);
            StartCoroutine(EnemyRushing(URA, RA, 0.3f));
        }
        
    }

    IEnumerator EnemyRushing(GameObject URA, GameObject RA, float speed){
        Vector3 uraPos = URA.transform.position;
        float distance = Vector3.Distance(transform.position, uraPos);
        float offsetX = (uraPos.x - enemyPos.x)/100;
        float offsetY = (uraPos.y - enemyPos.y)/100;
        // underRedPool.Recycle(URA.GetComponent<Enemy>());
        // redPool.Recycle(RA.GetComponent<Enemy>());
        // come
        while(distance > 1){
            distance = Vector3.Distance(transform.position, uraPos);
            transform.position += new Vector3(offsetX, offsetY, 0) * speed;
            yield return new WaitForSeconds(0.01f);
        }
        Destroy(URA);
        Destroy(RA);
        // go
        while(distance < 20){
            distance = Vector3.Distance(transform.position, uraPos);
            transform.position += new Vector3(offsetX, offsetY, 0) * speed;
            yield return new WaitForSeconds(0.01f);
        }
        Destroy(gameObject);
        // ObjectPool<Enemy>.instance.Recycle(this);
    }
}
