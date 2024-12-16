using UnityEngine;

[CreateAssetMenu(fileName = "BubbleSetData", menuName = "Custom/Bubble Set Data")]
public class BubbleSetData : ScriptableObject
{
    public int[] rows; // -3 ~ 3
    public int[] columns; // -5 ~ 6
    public float appearInterval; //second
}