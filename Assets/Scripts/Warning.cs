using System.Collections;
using JetBrains.Annotations;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class Flash : MonoBehaviour
{
    public GameObject bubbles;
    private Image image;
    private Color originalColor;
    private int flashTimes = 0;
    private bool isFlashing = false;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        image = GetComponent<Image>();
        originalColor = image.color;
    }

    void Update()
    {
        if (!isFlashing)
        {
            StartCoroutine(Flashing());
            isFlashing = true;
        }
    }

    public float offsetX;
    public float offsetY;
    IEnumerator Flashing()
    {
        while (flashTimes < 2)
        {
            yield return new WaitForSeconds(1);
            // Disappear
            for (float t = 0; t < 1f; t += Time.deltaTime)
            {
                SetAlpha(Mathf.Lerp(1f, 0f, t));
                yield return null;
            }
            SetAlpha(0f);

            // Appear
            for (float t = 0; t < 1f; t += Time.deltaTime)
            {
                SetAlpha(Mathf.Lerp(0f, 1f, t));
                yield return null;
            }
            SetAlpha(1f);

            flashTimes++;
        }
        yield return new WaitForSeconds(1);

        RectTransform rectTransform = GetComponent<RectTransform>();
        //horizontal
        if(rectTransform.anchoredPosition.x == -550){
            Vector3 CameraPos = mainCamera.ScreenToViewportPoint(rectTransform.position);
            Vector3 InstantiatePos = mainCamera.ViewportToWorldPoint(CameraPos) + new Vector3(-20, offsetY, 0);
            InstantiatePos.z = 0;
            Instantiate(bubbles, InstantiatePos, Quaternion.Euler(0, 0, 90));
        }
        //vertical
        else{
            Vector3 CameraPos = mainCamera.ScreenToViewportPoint(rectTransform.position);
            Vector3 InstantiatePos = mainCamera.ViewportToWorldPoint(CameraPos) + new Vector3(offsetX, -80, 0);
            InstantiatePos.z = 0;
            Instantiate(bubbles, InstantiatePos, Quaternion.identity);
        }
        
        Destroy(gameObject);
    }

    private void SetAlpha(float alpha)
    {
        Color newColor = originalColor;
        newColor.a = Mathf.Clamp01(alpha);
        image.color = newColor;
    }
}
