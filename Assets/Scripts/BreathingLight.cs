using UnityEngine;
using UnityEngine.Rendering.Universal; // 引用 Light2D 所在的命名空間

public class BreathingLight : MonoBehaviour
{
    private Light2D spotLight2D;  // Spotlight 2D 物件
    public float minIntensity = 0.1f;  // 最小亮度
    public float maxIntensity = 1f;  // 最大亮度
    public float breathSpeed = 2f;   // 呼吸速度，數值越大呼吸越快

    private void Start() {
        spotLight2D = GetComponent<Light2D>();
    }
    private void Update()
    {
        // 利用 Mathf.PingPong 創建亮度的來回變化
        float intensity = Mathf.PingPong(Time.time * breathSpeed, maxIntensity - minIntensity) + minIntensity;
        
        // 更新 Spotlight2D 的強度
        if (spotLight2D != null)
        {
            spotLight2D.intensity = intensity;
        }
    }
}
