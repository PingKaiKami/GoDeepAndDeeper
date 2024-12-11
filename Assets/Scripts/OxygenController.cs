using System;
using UnityEngine;
using UnityEngine.UI;

public class OxygenController : MonoBehaviour
{
    public static OxygenController Instance;

    public Slider currentOxygenSlider;   // 當前氧氣條 (白色)
    public Slider maxOxygenSlider;      // 最大氧氣條 (淺藍色)

    private float maxOxygen = 1.0f;      // 最大氧氣值
    private float currentOxygen = 1.0f; // 當前氧氣值
    private float heartRate = 0.4f;     // 心率

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
        //currentOxygen = currentOxygenSlider.value;
        //maxOxygen = maxOxygenSlider.value;
        // 初始化滑條
        if (maxOxygenSlider != null)
        {
            maxOxygenSlider.maxValue = 1.0f;
            maxOxygenSlider.value = maxOxygen;
        }

        if (currentOxygenSlider != null)
        {
            currentOxygenSlider.maxValue = maxOxygen;
            currentOxygenSlider.value = currentOxygen;
        }
    }
    void Update()
    {
        // 衝刺時 心率與耗氧量的計算
        if (Input.GetMouseButton(0) && Input.GetKey(KeyCode.LeftShift))
        {
            heartRate = Mathf.Clamp(heartRate + 0.08f * Time.deltaTime, 0, 1);
            if (currentOxygenSlider.value > 0)
            {
                currentOxygenSlider.value -= heartRate / 4000f;
            }

        }
        // 未衝刺 心率與耗氧量的計算
        else
        {
            heartRate = Mathf.Clamp(heartRate - 0.05f * Time.deltaTime, 0, 1);
            if (currentOxygenSlider.value > 0)
            {
                if (heartRate == 0)
                {
                    currentOxygenSlider.value -= 0.05f/4000f;
                }
                currentOxygenSlider.value -= heartRate / 4000f;
            }
        }
    }
    // 撞到垃圾扣除當前氧氣與最大氧氣，並設定上限值
    public void DecreaseMaxOxygen(float amount)
    {
        //maxOxygen = Mathf.Max(maxOxygen - amount, 0.1f); // 最大氧氣不得低於 0.1
        maxOxygen = Mathf.Clamp(maxOxygen - amount, 0, 1);
        if (maxOxygenSlider != null)
        {
            maxOxygenSlider.value = maxOxygen; // 更新最大氧氣條
        }

        if (currentOxygenSlider != null)
        {
            currentOxygenSlider.value = Mathf.Clamp(currentOxygenSlider.value - amount, 0, maxOxygen);
        }
    }
    // 增加當前氧氣
    public void IncreaseOxygen(float amount)
    {
        if (currentOxygenSlider != null)
        {
            // 確保滑條的最大值與最大氧氣值同步
            //currentOxygenSlider.maxValue = maxOxygen;

            // 增加當前氧氣值並限制在 maxOxygen 範圍內
            currentOxygen = currentOxygenSlider.value;
            currentOxygen = Mathf.Clamp(currentOxygen + amount, 0, maxOxygen);
            currentOxygenSlider.value = currentOxygen; // 更新滑條的值
        }
    }

    // 減少當前氧氣
    public void DecreaseOxygen(float amount)
    {
        if (currentOxygenSlider != null)
        {
            // 確保滑條的最大值與最大氧氣值同步
            //currentOxygenSlider.maxValue = maxOxygen;

            // 減少當前氧氣值並限制在 0 以上
            currentOxygen = currentOxygenSlider.value;
            currentOxygen = Mathf.Max(currentOxygen - amount, 0);
            currentOxygenSlider.value = currentOxygen; // 更新滑條的值
        }
    }

}