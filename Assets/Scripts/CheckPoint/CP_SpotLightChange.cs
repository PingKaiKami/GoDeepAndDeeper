using UnityEngine;
using UnityEngine.Rendering.Universal; // 引用 Light2D 所在的命名空间
using System.Collections;

public class CP_SpotLightChange : MonoBehaviour
{
    public bool canPassAfterTriggered = false;
    public Light2D spotLight2D; // SpotLight2D 光源
    public float targetIntensity = 0.1f; // 目標亮度
    public float fadeDuration = 3f; // 漸變時間
    public float targetInnerRadius = 0.5f; // 目標內半徑
    public float targetOuterRadius = 1.5f; // 目標外半徑

    private Vector2 playerEnterPosition; // 玩家進入觸發區的位置
    private Collider2D objectCollider; // 當前物體的 2D Collider

    private void Start()
    {
        // 獲取物體的 Collider2D
        objectCollider = GetComponent<Collider2D>();

        // 確保物體初始為觸發器模式
        if (objectCollider != null)
        {
            objectCollider.isTrigger = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 檢查是否是玩家
        if (other.CompareTag("Player"))
        {
            playerEnterPosition = other.transform.position; // 記錄玩家進入位置
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // 檢查是否是玩家
        if (other.CompareTag("Player"))
        {
            Vector2 playerExitPosition = other.transform.position;

            // 判斷是否向下離開
            if (playerExitPosition.y < playerEnterPosition.y)
            {
                Debug.Log("玩家向下離開，開始漸變 SpotLight2D 的亮度和半徑！");
                
                // 如果需要，還可以使此物體變為不可通過
                if (objectCollider != null)
                {
                    objectCollider.isTrigger = canPassAfterTriggered; // 關閉觸發器模式
                }

                // 開始漸變 SpotLight2D 的亮度和半徑
                if (spotLight2D != null)
                {
                    StartCoroutine(FadeSpotLight(
                        spotLight2D,
                        spotLight2D.intensity, targetIntensity,
                        spotLight2D.pointLightInnerRadius, targetInnerRadius,
                        spotLight2D.pointLightOuterRadius, targetOuterRadius,
                        fadeDuration
                    ));
                }
                else
                {
                    Debug.LogWarning("目標光源不是 SpotLight2D 或未設置！");
                }
            }
        }
    }

    // 漸變 SpotLight2D 的亮度和半徑的 Coroutine
    private IEnumerator FadeSpotLight(
        Light2D spotLight,
        float startIntensity, float endIntensity,
        float startInnerRadius, float endInnerRadius,
        float startOuterRadius, float endOuterRadius,
        float duration)
    {
        float timeElapsed = 0f;

        // 漸變過程
        while (timeElapsed < duration)
        {
            spotLight.intensity = Mathf.Lerp(startIntensity, endIntensity, timeElapsed / duration);
            spotLight.pointLightInnerRadius = Mathf.Lerp(startInnerRadius, endInnerRadius, timeElapsed / duration);
            spotLight.pointLightOuterRadius = Mathf.Lerp(startOuterRadius, endOuterRadius, timeElapsed / duration);

            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // 確保最終達到目標值
        spotLight.intensity = endIntensity;
        spotLight.pointLightInnerRadius = endInnerRadius;
        spotLight.pointLightOuterRadius = endOuterRadius;
    }
}
