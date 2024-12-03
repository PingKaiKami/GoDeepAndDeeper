using System.Collections;
using UnityEngine;

public class SubmarineCreator : MonoBehaviour
{
    public GameObject submarine;
    public bool canSummonSubmarine = false;
    private bool isSummon = false;
    void Update()
    {
        if(canSummonSubmarine && !isSummon){
            StartCoroutine(SummonSubmarine());
            isSummon = true;
        }
    }
    IEnumerator SummonSubmarine(){
        Instantiate(submarine, new Vector3(100,0,0), Quaternion.identity);
        yield return new WaitForSeconds(60);
        isSummon = false;
    }
}
