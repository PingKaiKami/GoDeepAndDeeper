using UnityEngine;
using UnityEngine.UI;

public class HeartColor : MonoBehaviour
{
    public ValueController valueController;
    private Image image;
    private Color heartColor;
    private float heartRate;
    void Start()
    {
        image = GetComponent<Image>();
        heartColor = image.color;
    }
    
    void Update()
    {
        heartRate = valueController.GetHeartRate();
        float newColorValue = Mathf.Clamp01(1.0f - heartRate * 0.8f);
        heartColor.r = newColorValue;
        heartColor.g = newColorValue;
        heartColor.b = 1f;
        image.color = heartColor;
    }
}
