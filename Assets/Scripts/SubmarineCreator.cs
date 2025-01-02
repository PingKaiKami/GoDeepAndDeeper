using System.Collections;
using UnityEngine;

public class SubmarineCreator : MonoBehaviour
{
    public GameObject submarine;
    public bool canSummonSubmarine = false;
    private bool isSummon = false;
    private PlayerController player;
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
    void Update()
    {
        if(canSummonSubmarine && !isSummon){
            StartCoroutine(SummonSubmarine());
            isSummon = true;
        }
        if(!player.isAlive()){
            canSummonSubmarine = false;
        }
    }
    IEnumerator SummonSubmarine(){
        Instantiate(submarine, new Vector3(100,0,0), Quaternion.identity);
        yield return new WaitForSeconds(60);
        isSummon = false;
    }
}
