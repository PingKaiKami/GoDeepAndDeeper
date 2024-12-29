using System.Collections;
using UnityEngine;

public class CP_HTBC : MonoBehaviour
{
    public HighTempBubbleCreator bubbleCreator; // 要修改的目標腳本
    public float newAppearSetInterval = 2f; // 玩家通過後設定的新出現間隔
    public bool newCanAppear = false; // 玩家通過後是否允許氣泡繼續生成
    public BubbleSetData[] newBubbleSets; // 玩家通過後的新 BubbleSetData

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

            if (playerExitPosition.y < playerEnterPosition.y)
            {
                Debug.Log("玩家向下通過 CheckPoint_8，更新 HighTempBubbleCreator 的設定。");

                // 修改目標腳本的變數
                if (bubbleCreator != null)
                {
                    bubbleCreator.appearSetInterval = newAppearSetInterval;
                    bubbleCreator.canAppear = newCanAppear;

                    // 更新 BubbleSetData
                    if (newBubbleSets != null && newBubbleSets.Length > 0)
                    {
                        bubbleCreator.bubbleSets = newBubbleSets;
                    }
                }

                // 如果需要停用該檢查點，可以在這裡禁用自己
                // gameObject.SetActive(false);
            }
        }
    }
}
