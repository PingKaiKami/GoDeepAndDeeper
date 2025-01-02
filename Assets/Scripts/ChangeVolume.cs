using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ChangeVolume : MonoBehaviour
{
    public int level;
    public Volume volume;
    private bool change;
    private Vector2 playerEnterPosition;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            playerEnterPosition = other.transform.position;
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "Player"){
            Vector2 playerExitPosition = other.transform.position;
            //go down
            if(playerExitPosition.y < playerEnterPosition.y){
                if(level == 3){
                    change = true;
                    StartCoroutine(ChangeCF());
                }
                if(level == 4){
                    change = false;
                    StartCoroutine(ChangeCF());
                }
            }
            //go up
            else{
                if(level == 3){
                    change = false;
                    StartCoroutine(ChangeCF());
                }
            }
        }
    }
    IEnumerator ChangeCF(){
        while(change){
            if (volume.profile.TryGet(out ColorAdjustments ca))
            {
                Color color = ca.colorFilter.value;
                if(color.g <= 0){
                    break;
                }
                color.g -= 0.01f;
                color.b -= 0.01f;
                ca.colorFilter.value = color;
            }
            yield return new WaitForSeconds(0.1f);
        }
        while(!change){
            if (volume.profile.TryGet(out ColorAdjustments ca))
            {
                Color color = ca.colorFilter.value;
                if(color.g >= 1){
                    break;
                }
                color.g += 0.01f;
                color.b += 0.01f;
                ca.colorFilter.value = color;
                
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

}
