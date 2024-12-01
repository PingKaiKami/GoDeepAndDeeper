using System;
using UnityEngine;
using UnityEngine.UI;

public class DepthSliderController : MonoBehaviour
{
    public Slider depthSlider;       // 深度滑條
    public Transform player;         // 玩家物件
    private float maxDepth = 100f;         // 地圖總高度
    private float bottomY;           // 玩家出生點作為最底部

    void Start()
    {
         // 設置地圖底部為玩家出生點
        bottomY = player.position.y;

        // 初始化滑條
        depthSlider.minValue = 0;
        depthSlider.maxValue = maxDepth;
    }
    void Update()
    {
         if (player != null && depthSlider != null)
        {
            // 計算玩家當前深度
            float playerDepth = player.position.y - bottomY;

            // 更新滑條值，確保不超過最大深度
            depthSlider.value = Mathf.Clamp(playerDepth, 0, maxDepth);
        }
    }
}