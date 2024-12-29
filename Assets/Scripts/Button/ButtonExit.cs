using UnityEngine;

public class ExitGame : MonoBehaviour
{
    // 當按下按鈕時退出遊戲
    public void Exit()
    {
        // 如果是在編輯模式，則停止播放
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // 如果是打包後的遊戲，則退出
        Application.Quit();
#endif
    }
}
