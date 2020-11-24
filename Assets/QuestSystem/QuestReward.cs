using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using InventorySystem;


namespace QuestSystem
{
    public class QuestReward
    {
        public QuestReward questReward;

        public Item item1;
        public Item item2;
        public Item item3;
        public List<Item> items;

        public bool flag1;
        public bool flag2;
        public bool flag3;
        public List<bool> flags;

        public QuestReward(Item item1 , bool flag1)
        {
            this.item1 = item1;
            this.items = new List<Item>();
            this.items.Add(item1);
            this.flag1 = flag1;
            this.flags = new List<bool>();
            this.flags.Add(flag1);
        }
    }
}
