using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{
    public float fadeSpeed = 0.3f;
    private Image image;
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        Color color = image.color;
        color.a -= fadeSpeed * Time.deltaTime;
        image.color = color;
    }
}
