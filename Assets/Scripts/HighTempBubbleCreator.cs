using System.Collections;
using UnityEngine;

public class HighTempBubbleCreator : MonoBehaviour
{
    public BubbleSetData[] bubbleSets;
    public GameObject warning;
    public float appearSetInterval; //second
    public bool canAppear = true;
    private int index = 0;
    public Canvas canvas;

    // Update is called once per frame
    void Update()
    {
        if(canAppear){
            if(bubbleSets.Length > index){
                StartCoroutine(StartAppearing(bubbleSets[index++]));
                canAppear = false;
            }
        }
    }

    IEnumerator StartAppearing(BubbleSetData bubbleSet){
        foreach(int row in bubbleSet.rows){
            GameObject newUI = Instantiate(warning);
            RectTransform rectTransform = newUI.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(-360, 65 * row);
            newUI.transform.SetParent(canvas.transform, false);
            yield return new WaitForSeconds(bubbleSet.appearInterval);
        }
        foreach(int column in bubbleSet.columns){
            GameObject newUI;
            newUI = Instantiate(warning);
            RectTransform rectTransform = newUI.GetComponent<RectTransform>();

            rectTransform.anchoredPosition = new Vector2(column * 60, -195);

            newUI.transform.SetParent(canvas.transform, false);
            yield return new WaitForSeconds(bubbleSet.appearInterval);
        }
        yield return new WaitForSeconds(appearSetInterval);
        canAppear = true;
    }
}
