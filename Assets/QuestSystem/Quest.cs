using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace QuestSystem 
{
    public class Quest 
    {
        public enum QuestStatus{ notAvailable = 0, available , accepted , done};

        public QuestName questName;
        [TextArea]
        public string questInfo;
        public QuestStatus questStatus;
        public List<QuestEvent> questEvents = new List<QuestEvent>();
        public QuestReward questReward;
        public bool isDone = false;
        //public string questID;

        public enum QuestName
        {
            firstQuest,
            seconQuest,
            nullQuest
        }

        public Quest (QuestName questName , string questInfo)
        {
            this.questName = questName;
            //this.questType = questType;
            this.questInfo = questInfo;
            //questID = Guid.NewGuid().ToString();
        }

        public QuestEvent GetCurrentEvent()
        {
            foreach (QuestEvent qE in questEvents)
            {
                if (qE.status == QuestEvent.EventStatus.current)
                    return qE;
            }
            return null;
        }

        public QuestEvent AddQuestEvent(string name , string info)
        {
            QuestEvent questEvent = new QuestEvent(name , info , this);
            questEvents.Add(questEvent);
            return questEvent;
        }

        public void AddQuestEvent(QuestEvent questEvent)
        {
            questEvent.belongTo = this;
            questEvents.Add(questEvent);
        }

        public void AddPath(string from , string to)
        {
            QuestEvent fromQuestEvent = FindQuestEvent(from);
            QuestEvent toQuestEvent = FindQuestEvent(to);
            if (fromQuestEvent != null && toQuestEvent != null)
            {
                QuestPath path = new QuestPath(toQuestEvent);
                fromQuestEvent.goTo = FindQuestEvent(to);
            }
        }

        public void QuestEventLinkerAndMarker()
        {
            for (int i = 0; i < questEvents.Count; i++)
            {
                if (i != questEvents.Count - 1)
                {
                    questEvents[i].goTo = questEvents[i + 1];
                    Debug.Log("from" + questEvents[i].eventName + "to" + questEvents[i].goTo.eventName);
                }
                else
                {
                    questEvents[i].goTo = null;
                }

                foreach (QuestEvent qE in questEvents)
                {
                    qE.status = QuestEvent.EventStatus.waiting;
                }
            }

            questEvents[0].UpDateQuestEvent(QuestEvent.EventStatus.current);
            Debug.Log(GetCurrentEvent().eventName);
        }

        public QuestEvent FindQuestEvent(string id)
        {
            foreach (QuestEvent questEvent in questEvents)
            {
                if(questEvent.GetID() == id)
                    return questEvent;
            }
            Debug.Log("no event");
            return null;
        }

        public QuestEvent FindQuestEvent(QuestEvent qE)
        {
            foreach (QuestEvent questEvent in questEvents)
            {
                if (questEvent.GetID() == qE.GetID())
                    return questEvent;
            }
            Debug.Log("no event");
            return null;
        }

        public void BFS(QuestEvent qE, int orderNumber = 1)    //傳入首個事件，爲之後的事件排序
        {
            QuestEvent thisEvent = FindQuestEvent(qE.GetID());
            thisEvent.order = orderNumber;

            if (orderNumber > 1)
            {
                thisEvent.UpDateQuestEvent(QuestEvent.EventStatus.waiting);
            }
            else if (orderNumber == 1)
            {
                thisEvent.UpDateQuestEvent(QuestEvent.EventStatus.current);
            }
            else
            {
                thisEvent.UpDateQuestEvent(QuestEvent.EventStatus.done);
                thisEvent.goTo.UpDateQuestEvent(QuestEvent.EventStatus.current);
            }

            foreach (QuestEvent qES in questEvents)
            {
                if (qES.order == -1)
                    BFS(qES, orderNumber + 1);
            }
        }

        public void PrintPath()
        {
            foreach(QuestEvent questEvent in questEvents)
            {
                Debug.Log(questEvent.eventName.ToString() + " " + questEvent.order);
            }
        }
    }    
}
