using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadValue : MonoBehaviour
{
    public Slider slider;
    private TextMeshProUGUI text;
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    private int value;
    void Update()
    {
        value = (int)(slider.value * 100);
        text.text = value.ToString();
    }
}
