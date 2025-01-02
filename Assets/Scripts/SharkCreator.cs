using System.Collections;
using UnityEngine;

public class SharkCreator : MonoBehaviour
{
    public GameObject shark;
    public bool canSummonShark = false;
    public int summonTime = 20;//生成間隔
    private bool isSummon = false;
    private PlayerController player;
    // private ObjectPool<Enemy> sharkPool;
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        // sharkPool = ObjectPool<Enemy>.instance;
        // sharkPool.InitPool(shark);
    }
    void Update()
    {
        if(canSummonShark && !isSummon){
            StartCoroutine(SummonShark());
            isSummon = true;
        }
        if(!player.isAlive()){
            canSummonShark = false;
        }
    }
    IEnumerator SummonShark(){
        // Vector3 playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;
        // int randomAngle = Random.Range(0,360);
        // float angleInRadians = randomAngle * Mathf.Deg2Rad;
        // float offsetX = (float)(30 * Mathf.Cos(angleInRadians));
        // float offsetY = (float)(30 * Mathf.Sin(angleInRadians));
        // Quaternion enemyRotation;
        // Vector3 localScale;
        // if(randomAngle <= 90 || randomAngle >= 270){
        //     enemyRotation = Quaternion.Euler(0, 0, randomAngle);
        //     localScale = new Vector3(1,1,1);
        // }
        // else if(randomAngle <= 180){
        //     enemyRotation = Quaternion.Euler(0, 0, randomAngle + 180);
        //     localScale = new Vector3(-1,1,1);
        // }
        // else{
        //     enemyRotation = Quaternion.Euler(0, 0, randomAngle - 180);
        //     localScale = new Vector3(-1,1,1);
        // }
        
        // Vector3 enemyPos = new Vector3(playerPos.x + offsetX, playerPos.y + offsetY, playerPos.z);
        
        // sharkPool.Spawn(enemyPos, enemyRotation, localScale);
        Instantiate(shark, new Vector3(100,0,0), Quaternion.identity);
        yield return new WaitForSeconds(summonTime);
        isSummon = false;
    }
}
