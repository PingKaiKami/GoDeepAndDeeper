using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class ValueController : MonoBehaviour
{
    public static ValueController Instance;
    public bool isDebug = false;
    public Slider oxygenSlider;
    public Slider SP;
    private float maxOxygen = 1f;
    private float oxygen = 1f;
    private float heartRate = 0f;
    private float energy = 1f;
    private GameObject player;
    private PlayerController playerScript;
    private int currentIndex = 0;
    private KeyCode[] konamiCode = {
        KeyCode.UpArrow, KeyCode.UpArrow,
        KeyCode.DownArrow, KeyCode.DownArrow,
        KeyCode.LeftArrow, KeyCode.RightArrow,
        KeyCode.LeftArrow, KeyCode.RightArrow,
        KeyCode.B, KeyCode.A
    };


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
        // 檢查當前密碼序列中的按鍵是否被按下
        if (Input.GetKeyDown(konamiCode[currentIndex]))
        {
            currentIndex++;
            // 如果完成密碼輸入，執行特定行為
            if (currentIndex >= konamiCode.Length)
            {
                Debug.Log("Konami Code Activated!");
                ActivateCheat(); // 自定義觸發行為
                currentIndex = 0; // 重置序列
            }
        }
        else if (Input.anyKeyDown)
        {
            // 如果輸入錯誤，重置進度
            currentIndex = 0;
        }
        //sprint
        if(playerScript.isAlive() && !isDebug && player.activeSelf){
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

    //^^vv<><>BA
    void ActivateCheat(){
        isDebug = !isDebug;
        Debug.Log("Cheat Mode Activated!");
    }


}
