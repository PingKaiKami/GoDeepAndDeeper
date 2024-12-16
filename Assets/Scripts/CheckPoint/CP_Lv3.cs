using UnityEngine;
using UnityEngine.Rendering.Universal;  // 引用 Light2D 所在的命名空间
using System.Collections;

public class CP_Lv3 : MonoBehaviour
{
    public bool canPassAfterTriggered = false;
    public Light2D globalLight2D; // 全域光源 2D
    public float targetIntensity = 0.1f; // 目標亮度
    public float fadeDuration = 3f; // 漸變時間

    private Vector2 playerEnterPosition; // 玩家進入觸發區的位置
    private Collider2D objectCollider;  // 當前物體的 2D Collider

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
                Debug.Log("玩家向下離開，開始漸變光源亮度！");
                // 如果需要，還可以使此物體變為不可通過
                if (objectCollider != null)
                {
                    objectCollider.isTrigger = canPassAfterTriggered; // 關閉觸發器模式
                }

                // 開始漸變光源亮度
                if (globalLight2D != null)
                {
                    StartCoroutine(FadeLight(globalLight2D, globalLight2D.intensity, targetIntensity, fadeDuration));
                }
            }
        }
    }

    // 漸變光源亮度的 Coroutine
    private IEnumerator FadeLight(Light2D light2D, float startIntensity, float endIntensity, float duration)
    {
        float timeElapsed = 0f;

        // 漸變過程
        while (timeElapsed < duration)
        {
            light2D.intensity = Mathf.Lerp(startIntensity, endIntensity, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // 確保最終達到目標亮度
        light2D.intensity = endIntensity;
    }
}
