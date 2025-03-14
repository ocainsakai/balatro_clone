using UnityEngine;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif
public abstract class CollectableSO : ScriptableObject
{
    public string Id;
    public string Name;
    public Sprite Icon;
    public string Description;

#if UNITY_EDITOR
    [ContextMenu("Generate ID")]
    protected void GenerateIdMenuItem()
    {
        // Tìm tất cả các CollectableSO để xác định ID tiếp theo
        string[] guids = AssetDatabase.FindAssets("t:CollectableSO");
        HashSet<string> existingIds = new HashSet<string>();

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            CollectableSO item = AssetDatabase.LoadAssetAtPath<CollectableSO>(path);
            if (item != null && !string.IsNullOrEmpty(item.Id))
            {
                existingIds.Add(item.Id);
            }
        }

        // Tạo tiền tố từ tên class
        string prefix = this.GetType().Name;

        // Tìm số tiếp theo
        int counter = 1;
        string newId;
        do
        {
            newId = $"{prefix}_{counter:D4}";
            counter++;
        } while (existingIds.Contains(newId));

        Id = newId;

        // Đánh dấu object đã thay đổi
        EditorUtility.SetDirty(this);
    }

    protected virtual void OnValidate()
    {
        if (string.IsNullOrEmpty(Id))
        {
            GenerateIdMenuItem();
        }
    }
#endif
    
}
