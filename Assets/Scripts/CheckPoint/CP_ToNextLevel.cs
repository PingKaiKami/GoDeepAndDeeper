using UnityEngine;
using UnityEngine.SceneManagement; // 引入 SceneManager 命名空間

public class CheckPoint_ToNextLevel : MonoBehaviour
{
    public string nextSceneName; // 下個場景的名稱
    private Vector3 playerEnterPosition; // 玩家進入的位置
    private Collider2D triggerCollider; // 觸發檢查點的 Collider2D

    private void Start()
    {
        // 確保觸發區有 Collider2D 並設置為觸發器
        triggerCollider = GetComponent<Collider2D>();
        if (triggerCollider != null)
        {
            triggerCollider.isTrigger = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 檢查是否是玩家進入
        if (other.CompareTag("Player"))
        {
            playerEnterPosition = other.transform.position;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // 檢查是否是玩家並確保是向下離開
        if (other.CompareTag("Player"))
        {
            Vector3 playerExitPosition = other.transform.position;

            // 在這裡您可以根據需要添加離開條件，如果有需要
            if (playerExitPosition.y < playerEnterPosition.y)
            {
                Debug.Log("玩家向下通過 CheckPoint_toNextLevel，切換至下一場景。");

                // 加載下一個場景
                LoadNextScene();
            }
        }
    }

    // 加載下一個場景
    void LoadNextScene()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName); // 使用場景名稱加載下一個場景
        }
        else
        {
            Debug.LogError("下一場景名稱未設置！");
        }
    }
}
