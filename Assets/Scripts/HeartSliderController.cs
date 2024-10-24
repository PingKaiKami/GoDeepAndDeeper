using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartSliderController : MonoBehaviour {

    public Image heartImage;
    //private float initHeartFillAmount;

    void Start()
    {
        heartImage.fillAmount = 0.5f;
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            // 增加 fillAmount，並確保不超過 1
            heartImage.fillAmount = Mathf.Clamp(heartImage.fillAmount + 0.05f * Time.deltaTime, 0, 1);
        }
        else
        {
            // 減少 fillAmount，並確保不低於 0
            heartImage.fillAmount = Mathf.Clamp(heartImage.fillAmount - 0.05f * Time.deltaTime, 0, 1);
        }
    }

}