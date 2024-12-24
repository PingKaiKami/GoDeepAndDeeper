using TMPro;
using UnityEngine;

public class LoadHeartRateValue : MonoBehaviour
{
    public ValueController valueController;
    private TextMeshProUGUI text;
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    private int value;
    void Update()
    {
        value = (int)(valueController.GetHeartRate() * 100) + 60;
        text.text = value.ToString();
    }
}
