using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using InventorySystem;


namespace QuestSystem
{
    public class QuestDemand
    {
        public QuestDemand questDemand;

        public bool flag1;
        public bool flag2;
        public bool flag3;
        public List<bool> flags = new List<bool>();

        public Item item1;
        public Item item2;
        public Item item3;
        public List<Item> items = new List<Item>();

        public QuestDemand(Item item1 , Item item2 , Item item3 )
        {
            this.item1 = item1;
            this.item2 = item2;
            this.item3 = item3;
            this.items.Add(item1);
            this.items.Add(item2);
            this.items.Add(item3);
            this.flags = null;
        }

        public QuestDemand(Item item1 , Item item2 )
        {
            this.item1 = item1;
            this.item2 = item2;
            this.items.Add(item1);
            this.items.Add(item2);
            this.flags = null;
        }

        public QuestDemand(Item item1)
        {
            this.item1 = item1;
            this.items.Add(item1);
            this.flags = null;
        }
        public QuestDemand(bool flag1)
        {
            this.flag1 = flag1;
            this.flags.Add(flag1);
            this.items = null;
        }

        public QuestDemand(bool flag1 , Item item1)
        {
            this.flag1 = flag1;
            this.item1 = item1;
            this.flags.Add(flag1);
            this.items.Add(item1);
        }
    }
}
