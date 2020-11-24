using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using InventorySystem;

namespace QuestSystem 
{
    public class QuestManager : MonoBehaviour 
    {
        static QuestManager instance;

        public GameObject questSlot;
        public GameObject questEventSlotGrid;
        public GameObject questEventSlot;

        private GameObject cloneSlot;

        private Quest selectQuest;

        public GameObject OKButton;

        private QuestData questData;
        private static List<Quest> questList;

        public void Awake()
        {
            questData = GameObject.Find("GameData").GetComponent<QuestData>();

            questData.questManager = this.gameObject;
            questData.OKButton = OKButton;

            Debug.Log(questData);
            questList = questData.GetQuestList();
        }

        public void Initialize()
        {
            Debug.Log("InitialQuestPanel");
            this.gameObject.transform.parent.gameObject.SetActive(true);
            questData.CheckAcceptedQuest();

            if (questData.GetCurrentQuest() == null)
            {
                Debug.Log("questData.GetCurrentQuest() == null)");
                questSlot.transform.GetChild(0).gameObject.GetComponent<Text>().text = "";
                questSlot.transform.GetChild(1).gameObject.GetComponent<Text>().text = "";
                DestoryQuestPanel();
            }
            else
            {
                RefreshQuestPanel();
            }


        }

        public void Escape()
        {
            this.gameObject.transform.parent.gameObject.SetActive(false);
        }

        void Update()
        {
            if (this.gameObject.activeSelf && Input.GetKeyDown(KeyCode.Escape))
            {
                Escape();
            }
        }

        public void RefreshQuestPanel()
        {
            if(questData.GetAcceptedQuest() == null)
            {
                Debug.Log("no accepted quest now");
                return;
            }

            DestoryQuestPanel();

            for (int i = 0; i < 2; i++)
            {
                questSlot.transform.GetChild(i).GetComponent<Text>().text = (i == 0) ? questData.GetAcceptedQuest().questName.ToString() : questData.GetAcceptedQuest().questInfo;
            }

            foreach (QuestEvent questEvent in questData.GetAcceptedQuest().questEvents)
            {
                if (questEvent.status == QuestEvent.EventStatus.waiting)
                    return;
                cloneSlot = Instantiate(questEventSlot, questEventSlotGrid.transform);
                cloneSlot.transform.GetChild(0).GetComponent<Text>().text = questEvent.eventName;
                cloneSlot.transform.GetChild(1).GetComponent<Text>().text = questEvent.eventInfo;
            }
        }

        public void DestoryQuestPanel()
        {
            foreach (Transform slot in questEventSlotGrid.transform)
            {
                Destroy(slot.gameObject);
            }
        }

    }    
}
