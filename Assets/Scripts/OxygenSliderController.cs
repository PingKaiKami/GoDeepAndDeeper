using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OxygenSliderController : MonoBehaviour {

    public Slider slider;
    private float heartRate = 0.5f;

    void Start()
    {
        slider = GetComponent<Slider>();
    }

    void Update()
    {
        // 衝刺時 心率與耗氧量的計算
        if (Input.GetKey(KeyCode.LeftShift))
        {
            heartRate = Mathf.Clamp(heartRate + 0.05f * Time.deltaTime, 0, 1);
            if (slider.value > 0)
            {
                slider.value -= heartRate / 1000f;
            }
        }
        // 未衝刺 心率與耗氧量的計算
        else
        {
            heartRate = Mathf.Clamp(heartRate - 0.05f * Time.deltaTime, 0, 1);
            if (slider.value > 0)
            {
                slider.value -= heartRate / 1000f;
            }
        }
    }
}