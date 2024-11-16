using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SharkCreator : MonoBehaviour
{
    public GameObject shark;
    public bool canSummonShark = false;
    private bool isSummon = false;
    void Update()
    {
        if(canSummonShark && !isSummon){
            StartCoroutine(SummonShark());
            isSummon = true;
        }
    }
    IEnumerator SummonShark(){
        Instantiate(shark, new Vector3(100,0,0), Quaternion.identity);
        yield return new WaitForSeconds(20);
        isSummon = false;
    }
}
