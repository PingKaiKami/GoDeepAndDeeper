using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class ButtonStart : MonoBehaviour
{
    public Button buttonStart;            // 按鈕
    public CanvasGroup fadeCanvasGroup;  // 控制漸變的 CanvasGroup
    public float fadeDuration = 1f;      // 漸變時間

    void Start()
    {
        // 設置按鈕點擊事件
        buttonStart.onClick.AddListener(OnStartButtonClick);

        // 初始化透明度
        fadeCanvasGroup.alpha = 0f;
    }

    void OnStartButtonClick()
    {
        // 開始漸變並切換場景
        StartCoroutine(FadeAndChangeScene());
    }

    IEnumerator FadeAndChangeScene()
    {
        // 漸暗畫面
        yield return StartCoroutine(Fade(1f));

        // 切換到下一個場景
        yield return SceneManager.LoadSceneAsync("CombinedLevel_Final");

        // 等待一幀，確保所有物件初始化完成
        yield return null;

        // 漸亮到透明
        //yield return StartCoroutine(Fade(0f));
    }

    IEnumerator Fade(float targetAlpha)
    {
        float startAlpha = fadeCanvasGroup.alpha;
        float timeElapsed = 0f;

        while (timeElapsed < fadeDuration)
        {
            fadeCanvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, timeElapsed / fadeDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        fadeCanvasGroup.alpha = targetAlpha;
    }
}
