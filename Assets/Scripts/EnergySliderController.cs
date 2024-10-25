using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergySliderController : MonoBehaviour {

    public Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();
    }

    void Update()
    {
        // 體力歸零時心率還是會增加
        // 衝刺時慢慢減少體力(體力充足) 
        // slider.value > 0.2 體力條被擋到
        if (Input.GetKey(KeyCode.LeftShift) && slider.value > 0.2)
        {
            slider.value -= 0.15f * Time.deltaTime;
        }
        // 未衝刺時 恢復體力
        else
        {
            slider.value += 0.05f * Time.deltaTime;
        }
        
    }
}