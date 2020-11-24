using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace QuestSystem 
{
    public class QuestPath 
    {
        public QuestEvent startEvent; 
        public QuestEvent endEvent; 
        public QuestPath(QuestEvent to)
        {
            endEvent = to;
        }
    }    
}
