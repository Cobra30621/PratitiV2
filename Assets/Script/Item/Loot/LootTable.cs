using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


[CreateAssetMenu(fileName= "LootTable", menuName= "Pratiti/Craete LootTable")]
public class LootTable : ScriptableObject
{
    public string lootName;
    // [Header("注意：機率要由大排到小")]
    public Loot[] _loots;

    [Range(0,100)]
    public int dropItemCounts;

    private int total;

    public Loot[] DropItem(){
        SetProbability();
        SetLootZero();

        // dropItemCounts 連抽
        for(int i = 0; i < dropItemCounts ; i++){
            float randomNum = Random.Range(0f, total);

            bool hasDrop = false; //是否已掉落物品
            // 一次隨機掉落物品
            for (int j =0; j < _loots.Length; j++){
                if(!hasDrop){
                    if(randomNum <= _loots[j]._probability){
                        _loots[j]._dropCount ++;
                        hasDrop = true;
                    }
                    else{
                        randomNum -= _loots[j]._probability;
                    }
                }
            }
        }
        // PrintDropItem();

        return _loots;
    }

    public void PrintDropItem(){
        foreach (Loot l in _loots)
        {
            Debug.Log($"掉落{l._dropCount}個{l._stickerType}{l._itemType} ");
        }
    }

    public void SetLootZero(){
        foreach (Loot l in _loots)
        {
            l._dropCount = 0;
        }
    }

    [ContextMenu("確認機率")]
    public void SetProbability(){
        total = 0;

        foreach (Loot l in _loots)
        {
            total += l._probability;
        }

        foreach (Loot l in _loots)
        {
            l.f_probability = (float)l._probability/total;
        }
    }
}
