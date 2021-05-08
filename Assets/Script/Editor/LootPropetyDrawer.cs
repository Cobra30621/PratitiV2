using UnityEditor;
using System.Collections.Generic;
using UnityEngine;

[CustomPropertyDrawer(typeof(Loot))]
public class LootProDrawer : PropertyDrawer {
    private float singeline = EditorGUIUtility.singleLineHeight;
    private Rect _position; 
    private Rect rawField;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        _position = position;
        EditorGUI.BeginProperty(position, label, property);
        // 前置設定
        Rect rLabel = position;
        rawField = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
        rawField.height = singeline;

        SerializedProperty spItemType = property.FindPropertyRelative("_itemType");
        SerializedProperty spSticker = property.FindPropertyRelative("_stickerType");
        SerializedProperty spProbability = property.FindPropertyRelative("_probability");
        SerializedProperty spF_probability = property.FindPropertyRelative("f_probability");
        ItemType itemType = (ItemType ) spItemType.enumValueIndex ;

        // 開始放置資料位置
        EditorGUI.LabelField(GetLabel(1), "選擇道具類型");
        EditorGUI.PropertyField(GetField(1), property.FindPropertyRelative("_itemType"), GUIContent.none);
        
        switch(itemType){
            case ItemType.Sticker :
                EditorGUI.LabelField(GetLabel(1, 2), "選擇貼紙類型");
                EditorGUI.PropertyField(GetField(2), spSticker, GUIContent.none);
                break;
            case ItemType.StickerChip :
                EditorGUI.LabelField(GetLabel(1, 2), "選擇貼紙碎片類型");
                EditorGUI.PropertyField(GetField(2), spSticker, GUIContent.none);
                break;
            case ItemType.Dessert :
                EditorGUI.LabelField(GetLabel(1, 2), "選擇甜點類型");
                // EditorGUI.PropertyField(GetField(2), spSticker, GUIContent.none);
                break;
        }

        // 設置道具機率
        EditorGUI.LabelField(GetLabel(3), "設置道具機率");
        EditorGUI.PropertyField(GetField(3), spProbability, GUIContent.none);

        // 顯示掉落機率
        float probability = spF_probability.floatValue;
        GUIStyle style = new GUIStyle(); 
        style.richText = true; // 使其可以判斷顏色
        EditorGUI.LabelField(GetLabel(4), $"      <color=#8E00F3> 掉落機率 : {probability}</color>", style);
        // EditorGUI.PropertyField(GetField(4), spF_probability, GUIContent.none);

        EditorGUI.EndProperty();
    }

    public Rect GetField(int lineCount){
        Rect field = rawField;
        field.y +=singeline * lineCount;
        return field;
    }

    public Rect GetLabel(int lineCount){
        return GetLabel(0, lineCount);
    }

    public Rect GetLabel(int labelNum, int lineCount){
        Rect label = _position;
        label.y += singeline * lineCount;
        label.x += 10 * labelNum + 10;
        return label;
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        int lineCount = 5;
        return EditorGUIUtility.singleLineHeight * lineCount;
    }

}
