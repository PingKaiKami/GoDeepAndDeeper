using UnityEngine;

public class TreasureBoxSensor : MonoBehaviour
{
    public bool isSensored = false;
    public GameObject ui_word;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            isSensored = true;
            ui_word.SetActive(true);
        }
    }   
    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "Player"){
            isSensored = false;
            ui_word.SetActive(false);
        }
    }
}
