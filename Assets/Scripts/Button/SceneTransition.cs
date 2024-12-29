using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTransition : MonoBehaviour
{
    public CanvasGroup fadeCanvasGroup;  // 控制漸變的 CanvasGroup
    public float fadeDuration = 1f;      // 漸變時間

    void Start()
    {
        // 初始化透明度為完全遮罩（即完全隱藏）
        fadeCanvasGroup.alpha = 1f;

        // 開始漸入過程
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        float startAlpha = fadeCanvasGroup.alpha;
        float timeElapsed = 0f;

        // 漸變到透明度 0，即畫面顯示出來
        while (timeElapsed < fadeDuration)
        {
            fadeCanvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, timeElapsed / fadeDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // 確保透明度最終是 0
        fadeCanvasGroup.alpha = 0f;
    }
}
