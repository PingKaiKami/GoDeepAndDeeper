using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OxygenSliderController : MonoBehaviour {

    public Slider whiteSlider;
    //同步更新 心率
    private float heartRate = 0.4f;

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
                whiteSlider.value -= heartRate / 5000f;
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
                    whiteSlider.value -= 0.3f/5000f;
                }
                whiteSlider.value -= heartRate / 5000f;
            }
        }
    }
}