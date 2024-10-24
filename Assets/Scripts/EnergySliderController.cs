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
        // 衝刺時慢慢減少體力(體力充足)
        if (Input.GetKey(KeyCode.LeftShift) && slider.value > 0)
        {
            slider.value -= 0.1f * Time.deltaTime;
        }
        // 未衝刺時 恢復體力
        else
        {
            slider.value += 0.05f * Time.deltaTime;
        }
        
    }
}