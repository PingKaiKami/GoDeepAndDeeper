using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using UnityEditor;

public class MergeScenes : MonoBehaviour
{
    [ContextMenu("Merge Levels")]
    public void MergeLevels()
    {
        // 加載 Level1 + Level2 合併後的場景（即 CombinedLevel）
        EditorSceneManager.OpenScene("Assets/Scenes/CombinedLevel_Final.unity", OpenSceneMode.Single);
        var mainScene = SceneManager.GetActiveScene();

        // 加載 Level4 作為附加場景
        var level4Scene = EditorSceneManager.OpenScene("Assets/Scenes/temp.unity", OpenSceneMode.Additive);

        // 遍歷 Level4 的所有根物件
        foreach (GameObject obj in level4Scene.GetRootGameObjects())
        {
            // 顯示選擇對話框，詢問用戶如何處理每個物件
            string message = $"物件 '{obj.name}' 存在於 temp 中，您希望如何處理？";
            int option = EditorUtility.DisplayDialogComplex(
                "物件處理",
                message,
                "刪除",  // 按鈕 1
                "保留",  // 按鈕 2
                "取消"   // 按鈕 3
            );

            if (option == 0) // 刪除
            {
                DestroyImmediate(obj); // 刪除該物件
                continue;
            }
            else if (option == 1) // 保留
            {
                // 不刪除，將物件移動到主場景並調整位置
                obj.transform.position += new Vector3(39, 23, 0); // 偏移位置，避免與 Level1 和 Level2 重疊
                SceneManager.MoveGameObjectToScene(obj, mainScene);
                continue;
            }
            else if (option == 2) // 取消
            {
                Debug.LogWarning("合併操作已取消。");
                return; // 停止合併
            }
        }

        // 確保 Camera 在合併後依然有效
        Camera.main.transform.position = new Vector3(100, 0, -10); // 調整 Camera 位置
        Camera.main.fieldOfView = 60;  // 設定適當的視野範圍
        Camera.main.nearClipPlane = 0.3f;
        Camera.main.farClipPlane = 1000f;

        // 保存合併後的場景
        EditorSceneManager.SaveScene(mainScene, "Assets/Scenes/Final.unity");

        Debug.Log("場景合併完成！所有物件處理完畢。");
    }
}
