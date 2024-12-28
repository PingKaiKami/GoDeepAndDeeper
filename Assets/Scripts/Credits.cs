using TMPro;
using UnityEngine;
using System.Collections;

public class Credits : MonoBehaviour
{
    [TextArea]
    public string[] words;
    private TextMeshProUGUI text;
    public bool canPlay = false;
    private bool isPlaying = false;
    private bool isChanging = false;
    private bool goNext = false;
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if(canPlay && !isPlaying){
            StartCoroutine(Playing());
            isPlaying = true;
            canPlay = false;
        }
    }
    private IEnumerator Playing(){
        int index = 0;
        while(index < words.Length){
            text.text = words[index];
            if(!isChanging){
                StartCoroutine(ChangeAlpha());
                isChanging = true;
            }
            if(goNext){
                index++;
                goNext = false;
                isChanging = false;
            }
            yield return null;
        }
    }
    private IEnumerator ChangeAlpha(){
        Color color = text.color;
        bool isUp = true;
        while(isUp){
            color.a = Mathf.Clamp01(color.a + 0.5f * Time.deltaTime);
            text.color = color;
            yield return null;
            if(color.a == 1){
                yield return new WaitForSeconds(5);
                isUp = false;
            }
        }
        while(!isUp && !goNext){
            color.a = Mathf.Clamp01(color.a - 0.5f * Time.deltaTime);
            text.color = color;
            yield return null;
            if(color.a == 0){
                yield return new WaitForSeconds(5);
                goNext = true;
            }
        }
    }
}
