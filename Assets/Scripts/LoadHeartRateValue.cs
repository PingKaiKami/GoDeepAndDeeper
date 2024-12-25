using TMPro;
using UnityEngine;

public class LoadHeartRateValue : MonoBehaviour
{
    public ValueController valueController;
    private TextMeshProUGUI text;
    private int value;
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }
    
    void Update()
    {
        value = (int)(valueController.GetHeartRate() * 100) + 60;
        text.text = value.ToString();
    }
}
