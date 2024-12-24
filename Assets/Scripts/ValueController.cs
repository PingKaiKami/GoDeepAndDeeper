using UnityEngine;
using UnityEngine.UI;

public class ValueController : MonoBehaviour
{
    public static ValueController Instance;
    public Slider oxygenSlider;
    public Slider SP;
    private float maxOxygen = 1.0f;
    private float oxygen = 1.0f;
    private float heartRate = 0f;
    private float energy = 1f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        oxygenSlider.maxValue = maxOxygen;
        oxygenSlider.value = oxygen;
    }
    void Update()
    {
        //sprint
        if (Input.GetMouseButton(0) && Input.GetKey(KeyCode.LeftShift))
        {
            energy -= 0.15f * Time.deltaTime;
            heartRate = Mathf.Clamp(heartRate + 0.08f * Time.deltaTime, 0, 1);
            if (oxygen > 0)
            {
                oxygen -= heartRate / 4000f;
            }

        }
        else
        {
            energy += 0.05f * Time.deltaTime;
            heartRate = Mathf.Clamp(heartRate - 0.05f * Time.deltaTime, 0, 1);
            if (oxygen > 0)
            {
                if (heartRate == 0)
                {
                    oxygen -= 0.05f/4000f;
                }
                oxygen -= heartRate / 4000f;
            }
        }
        //update value
        SP.value = energy;
        oxygenSlider.value = oxygen;
    }
    //Oxygen
    public void DecreaseMaxOxygen(float amount)
    {
        maxOxygen = Mathf.Clamp(maxOxygen - amount, 0, 1);
        oxygen = Mathf.Clamp(oxygen - amount, 0, maxOxygen);
    }
    public void IncreaseOxygen(float amount)
    {
        oxygen = Mathf.Clamp(oxygen + amount, 0, maxOxygen);
    }
    public void DecreaseOxygen(float amount)
    {
        oxygen = Mathf.Max(oxygen - amount, 0);
    }
    //Energy
    public void DecreaseEnergy(float amount)
    {
        SP.value = Mathf.Clamp(SP.value -= amount, 0, 1);
    }
    //HeartRate
    public void IncreaseHeartRate(float amount){
        heartRate = Mathf.Clamp(heartRate + amount, 0, 1);
    }
    public float GetHeartRate(){
        return heartRate;
    }

}
