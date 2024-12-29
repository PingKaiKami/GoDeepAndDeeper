using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal; // 引用 Light2D 所在的命名空間

public class CP_LightChange_Trigger : MonoBehaviour
{
    public Light2D light2D; // 需要控制的 Light2D
    public Color targetColor = Color.red; // 進入時的目標顏色（紅色）
    public Color originalColor = new Color(1f, 0f, 1f); // 離開時的顏色（紫色）
    public float targetIntensity = 1f; // 進入時的目標亮度
    public float originalIntensity = 0.5f; // 離開時的亮度
    public float changeDuration = 1f; // 顏色和亮度漸變持續時間

    private Coroutine colorAndIntensityCoroutine; // 用於管理正在運行的協程

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // 當玩家進入時，漸變至目標顏色和亮度
            StartColorAndIntensityChange(targetColor, targetIntensity);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // 當玩家離開時，漸變回原本的顏色和亮度
            StartColorAndIntensityChange(originalColor, originalIntensity);
        }
    }

    private void StartColorAndIntensityChange(Color newColor, float newIntensity)
    {
        // 如果有正在運行的協程，先停止它
        if (colorAndIntensityCoroutine != null)
        {
            StopCoroutine(colorAndIntensityCoroutine);
        }

        // 啟動新的漸變協程
        colorAndIntensityCoroutine = StartCoroutine(ChangeColorAndIntensity(newColor, newIntensity));
    }

    private IEnumerator ChangeColorAndIntensity(Color newColor, float newIntensity)
    {
        // 確保 Light2D 存在
        if (light2D == null)
        {
            yield break;
        }

        // 獲取當前顏色和亮度
        Color currentColor = light2D.color;
        float currentIntensity = light2D.intensity;

        float elapsedTime = 0f;

        // 漸變過程
        while (elapsedTime < changeDuration)
        {
            elapsedTime += Time.deltaTime;

            // 使用 Lerp 計算顏色和亮度的過渡
            light2D.color = Color.Lerp(currentColor, newColor, elapsedTime / changeDuration);
            light2D.intensity = Mathf.Lerp(currentIntensity, newIntensity, elapsedTime / changeDuration);

            yield return null;
        }

        // 確保最終顏色和亮度正確
        light2D.color = newColor;
        light2D.intensity = newIntensity;
    }
}
