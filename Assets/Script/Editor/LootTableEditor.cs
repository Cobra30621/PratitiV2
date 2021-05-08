using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LootTable))]
public class LootTableEditor : Editor
{
    LootTable m_Target;
    private ItemType _itemType;

    public int probability;

    public override void OnInspectorGUI(){
        this.serializedObject.Update();
        
        m_Target = (LootTable)target;

        EditorGUILayout.PropertyField(this.serializedObject.FindProperty("lootName"));
        
        GUILayout.Label("【掉落道具清單】");
        EditorGUILayout.PropertyField(this.serializedObject.FindProperty("_loots"), true);
        // GUIStyle style = new GUIStyle(); 
        // style.richText = true; // 使其可以判斷顏色
        // GUILayout.Label("<color=#FF0000>注意：掉落道具的機率要由大排到小</color>", style);

        GUILayout.Label("");
        GUILayout.Label("【一次掉落道具量】");
        EditorGUILayout.PropertyField(this.serializedObject.FindProperty("dropItemCounts"));
    
        if(GUILayout.Button("確認道具掉落機率")){
            // Debug.Log("hihi");
            m_Target.SetProbability();
        }
        this.serializedObject.ApplyModifiedProperties();
    }
}
