using UnityEngine;
using UnityEngine.UI;

public class ButtonTransparency : MonoBehaviour
{
    private Image buttonImage;

    // 設定按鈕透明度的變化量
    public float hoverAlpha = 0.5f;
    public float normalAlpha = 1f;

    void Start()
    {
        buttonImage = GetComponent<Image>();
    }

    // 當滑鼠移動到按鈕上時，改變透明度
    public void OnMouseEnter()
    {
        Color color = buttonImage.color;
        color.a = hoverAlpha;  // 設定透明度
        buttonImage.color = color;
    }

    // 當滑鼠移開按鈕時，恢復正常透明度
    public void OnMouseExit()
    {
        Color color = buttonImage.color;
        color.a = normalAlpha;  // 恢復正常透明度
        buttonImage.color = color;
    }
}
