using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class OxygneMaxValueController : MonoBehaviour{

    public static OxygneMaxValueController maxVlaue_Instance;
    public Slider lightBlueSlider;

    void Awake()
    {
        if(maxVlaue_Instance == null)
        {
            maxVlaue_Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        if(lightBlueSlider == null)
        {
            lightBlueSlider = GetComponent<Slider>();
        }
    }

    void Start()
    {
        lightBlueSlider = GetComponent<Slider>();
    }

    public void DecreaseMaxOxygen(float amount)
    {
        lightBlueSlider.value -= amount;
        lightBlueSlider.value = Mathf.Clamp(lightBlueSlider.value, 0, 1);
    }
}