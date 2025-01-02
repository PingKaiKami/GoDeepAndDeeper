using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CP_Submarine : MonoBehaviour
{
    public SubmarineCreator submarineCreator; // 將 SharkCreator 腳本拖入此欄位
    public bool newCanSummonSubmarine = true; // 通過後是否允許生成鯊魚
    public bool canPassAfterTriggered = false;
    public int newSummonTime = 20;        // 修改的生成間隔

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
                Debug.Log("玩家向下離開，修改 SharkCreator 參數！");

                // 修改 SharkCreator 腳本的參數
                if (submarineCreator != null)
                {
                    submarineCreator.canSummonSubmarine = newCanSummonSubmarine;
                    submarineCreator.summonTime = newSummonTime;
                    Debug.Log($"SharkCreator: canSummonShark = {newCanSummonSubmarine}, summonTime = {newSummonTime}");
                }

                // 如果需要，還可以使此物體變為不可通過
                if (objectCollider != null)
                {
                    objectCollider.isTrigger = canPassAfterTriggered; // 關閉觸發器模式
                }
            }
        }
    }
}
