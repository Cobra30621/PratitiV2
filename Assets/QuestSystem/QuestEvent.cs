using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using InventorySystem;

namespace QuestSystem 
{
    public class QuestEvent 
    {
        public enum EventStatus { waiting , current , done };

        public string eventName;
        public string eventInfo;
        private string id;
        public int order = -1;
        public EventStatus status;
        public Quest belongTo = new Quest(Quest.QuestName.nullQuest , null);
        public QuestDemand questDemand;
        public QuestReward questReward;
        private bool isDone = false;

        //public QuestPath pathList = new QuestPath(null);
        public QuestEvent goTo;

        public QuestEvent(string name , string info , Quest quest)
        {
            id = Guid.NewGuid().ToString();
            eventName = name;
            eventInfo = info;
            belongTo = quest;

            status = EventStatus.waiting;
        }

        //標記當前任務狀態並處理currentEvent；
        public void UpDateQuestEvent(EventStatus eS)
        {
            status = eS;

            if (eS == EventStatus.done && goTo != null)
            {
                isDone = true;
                Debug.Log(goTo);
                goTo.UpDateQuestEvent(EventStatus.current);
                Debug.Log("goto next");
            }
            else if (eS == EventStatus.done && goTo == null)
            {
                belongTo.isDone = true;
                belongTo.questStatus = Quest.QuestStatus.done;
            }
        }

        public string GetID()
        {
            return id;
        }
    }
}
