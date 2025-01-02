using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class ValueController : MonoBehaviour
{
    public static ValueController Instance;
    public bool isDebug = true;
    public Slider oxygenSlider;
    public Slider SP;
    private float maxOxygen = 1f;
    private float oxygen = 1f;
    private float heartRate = 0f;
    private float energy = 1f;
    private GameObject player;
    private PlayerController playerScript;

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
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<PlayerController>();
        oxygenSlider.maxValue = maxOxygen;
        oxygenSlider.value = oxygen;
    }
    void Update()
    {
        //sprint
        if(playerScript.isAlive() && isDebug && player.activeSelf){
            if (Input.GetMouseButton(0) && Input.GetKey(KeyCode.LeftShift) && energy > 0)
            {
                energy = Mathf.Clamp01(energy - 0.15f * Time.deltaTime);
                heartRate = Mathf.Clamp01(heartRate + 0.1f * Time.deltaTime);
                oxygen = Mathf.Clamp01(oxygen - (0.01f + heartRate * 0.05f) * Time.deltaTime);
            }
            else
            {
                energy = Mathf.Clamp01(energy + 0.05f * Time.deltaTime);
                heartRate = Mathf.Clamp01(heartRate - 0.04f * Time.deltaTime);
                oxygen = Mathf.Clamp01(oxygen - (0.01f + heartRate * 0.05f) * Time.deltaTime);
            }
        }
        //update value
        SP.value = energy;
        oxygenSlider.value = oxygen;
    }
    //Oxygen
    public void IncreaseMaxOxygen(float amount){
        maxOxygen = Mathf.Clamp01(maxOxygen + amount);
    }
    public void DecreaseMaxOxygen(float amount)
    {
        maxOxygen = Mathf.Clamp01(maxOxygen - amount);
        oxygen = Mathf.Clamp(oxygen - amount, 0, maxOxygen);
    }
    public void IncreaseOxygen(float amount)
    {
        oxygen = Mathf.Clamp(oxygen + amount, 0, maxOxygen);
    }
    public void DecreaseOxygen(float amount)
    {
        oxygen = Mathf.Clamp(oxygen - amount, 0, maxOxygen);
    }
    //Energy
    public void IncreaseEnergy(float amount){
        energy = Mathf.Clamp01(energy + amount);
    }
    public void DecreaseEnergy(float amount)
    {
        energy = Mathf.Clamp01(energy - amount);
    }
    //HeartRate
    public void IncreaseHeartRate(float amount){
        heartRate = Mathf.Clamp01(heartRate + amount);
    }
    public void DecreaseHeartRate(float amount){
        heartRate = Mathf.Clamp01(heartRate - amount);
    }
    public float GetOxygen(){
        return oxygen;
    }
    public float GetEnergy(){
        return energy;
    }
    public float GetHeartRate(){
        return heartRate;
    }
}
