using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;

public class OxygenSliderController : MonoBehaviour {

    public static OxygenSliderController Instance;
    public Slider whiteSlider;
    //同步更新 心率
    private float heartRate = 0.4f;
    void Awake()
    {
        // 確保只有一個 OxygenSliderController 實例
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);  // 在場景切換時不銷毀
        }
        else
        {
            Destroy(gameObject);  // 防止重複創建
        }

        // 確保 whiteSlider 已經設置
        if (whiteSlider == null)
        {
            whiteSlider = GetComponent<Slider>();
        }
    }
    void Start()
    {
        whiteSlider = GetComponent<Slider>();
    }
    // 單純shift耗體力
    void Update()
    {
        // 衝刺時 心率與耗氧量的計算
        if (Input.GetMouseButton(0) && Input.GetKey(KeyCode.LeftShift))
        {
            heartRate = Mathf.Clamp(heartRate + 0.08f * Time.deltaTime, 0, 1);
            if (whiteSlider.value > 0)
            {
                whiteSlider.value -= heartRate / 4000f;
            }

        }
        // 未衝刺 心率與耗氧量的計算
        else
        {
            heartRate = Mathf.Clamp(heartRate - 0.05f * Time.deltaTime, 0, 1);
            if (whiteSlider.value > 0)
            {
                if (heartRate == 0)
                {
                    whiteSlider.value -= 3f/4000f;
                }
                whiteSlider.value -= heartRate / 4000f;
            }
        }
    }
    public void IncreaseOxygen(float amount)
    {
        if (whiteSlider != null)
        {
            whiteSlider.value += amount;
            whiteSlider.value = Mathf.Clamp(whiteSlider.value, 0, 1);  // 保證值在 0 到 1 之間
        }
    }
}