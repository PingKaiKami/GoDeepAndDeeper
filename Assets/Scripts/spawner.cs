using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<GameObject> Items = new List<GameObject>();
    public float spawnTime = 2.5f;
    public Transform player; // 玩家物件 (從 Inspector 指定玩家)

    public float spawnRadius = 4f; 

    void Start()
    {
        InvokeRepeating("spawnItems", spawnTime, spawnTime);
    }
    void Update()
    {
        
    }
    
    public void spawnItems()
    {
        // 隨機偏移量
        Vector3 randomOffset = new Vector3(
            Random.Range(-spawnRadius, spawnRadius), 
            6f,
            -1f
        );

        // 計算生成位置
        Vector3 spawnPosition = player.position + randomOffset;

        int some = Random.Range(0, Items.Count);
        GameObject item = Instantiate(Items[some], spawnPosition, Quaternion.identity);
        Renderer renderer = item.GetComponent<Renderer>();
        if (renderer != null && !renderer.enabled)
        {
            Debug.LogWarning("Renderer is disabled on the instantiated item.");
            renderer.enabled = true; // 啟用 Renderer
        }
        StartCoroutine(DestroyPopoAfterDelay(item, 15f));
    }
    IEnumerator DestroyPopoAfterDelay(GameObject item, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(item);  // 銷毀物件
    }
}
