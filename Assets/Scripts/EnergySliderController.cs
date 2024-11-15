using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergySliderController : MonoBehaviour {

    public Slider greenSlider;

    void Start()
    {
        greenSlider = GetComponent<Slider>();
    }

    void Update()
    {
        // 衝刺時慢慢減少體力(體力充足) 
        // slider.value > 0.2 體力條被擋到
        if (Input.GetMouseButton(0) && Input.GetKey(KeyCode.LeftShift) && greenSlider.value > 0.2)
        {
            greenSlider.value -= 0.15f * Time.deltaTime;
        }
        // 未衝刺時 恢復體力
        else
        {
            greenSlider.value += 0.05f * Time.deltaTime;
        }
        
    }
}