using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ChangeVolume : MonoBehaviour
{
    public int level;
    public Volume volume;
    private bool goDown;
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
                    goDown = true;
                    StartCoroutine(ChangeCF());
                }
            }
            //go up
            else{
                if(level == 3){
                    goDown = false;
                    StartCoroutine(ChangeCF());
                }
            }
        }
    }
    IEnumerator ChangeCF(){
        while(goDown){
            if (volume.profile.TryGet(out ColorAdjustments ca))
            {
                Color color = ca.colorFilter.value;
                color.g -= 0.01f;
                color.b -= 0.01f;
                ca.colorFilter.value = color;
                if(color.g == 0){
                    break;
                }
            }
            yield return new WaitForSeconds(0.1f);
        }
        while(!goDown){
            if (volume.profile.TryGet(out ColorAdjustments ca))
            {
                Color color = ca.colorFilter.value;
                color.g += 0.01f;
                color.b += 0.01f;
                ca.colorFilter.value = color;
                if(color.g == 1){
                    break;
                }
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

}
