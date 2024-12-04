using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartSliderController : MonoBehaviour {

    public static HeartSliderController Instance;

    public Image heartImage;
    private float energy;
    //private float initHeartFillAmount;

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
        heartImage.fillAmount = 0.4f;
        energy = 1.0f;
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && Input.GetKey(KeyCode.LeftShift) && energy > 0.0f)
        {
            // 增加 fillAmount，並確保不超過 1
            if (energy > 0.2f)
            {
                heartImage.fillAmount = Mathf.Clamp(heartImage.fillAmount + 0.08f * Time.deltaTime, 0, 1);
                energy -= 0.15f * Time.deltaTime;
            }
        }
        else
        {
            // 減少 fillAmount，並確保不低於 0
            heartImage.fillAmount = Mathf.Clamp(heartImage.fillAmount - 0.05f * Time.deltaTime, 0, 1);
            if (energy < 1.0f)
            {
                energy += 0.05f * Time.deltaTime;
            }
        }
    }

    public void IncreaseHeartRate(float amount)
    {
        // 增加 heartImage.fillAmount，並確保不超過 1
        heartImage.fillAmount = Mathf.Clamp(heartImage.fillAmount + amount, 0, 1);       
    } 
}