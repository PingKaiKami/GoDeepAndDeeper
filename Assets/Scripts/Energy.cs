using UnityEngine;
using UnityEngine.UI;

public class Energy : MonoBehaviour
{
    public Slider SP;
    void Start()
    {
        SP = GetComponent<Slider>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && Input.GetKey(KeyCode.LeftShift) && SP.value > 0.2)
        {
            SP.value -= 0.15f * Time.deltaTime;
        }
        else
        {
            SP.value += 0.05f * Time.deltaTime;
        }
    }
    public void DecreaseEnergy(float amount)
    {
        SP.value = Mathf.Clamp(SP.value -= amount, 0, 1);
    }
}
