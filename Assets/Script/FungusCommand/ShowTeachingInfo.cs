// This code is part of the Fungus library (https://github.com/snozbot/fungus)
// It is released for free under the MIT open source license (https://github.com/snozbot/fungus/blob/master/LICENSE)

using UnityEngine;

namespace Fungus
{

    [CommandInfo("ForPratiti", 
                 "ShowTeachingInfo", 
                 "顯示教學訊息")]
    [AddComponentMenu("")]
    public class ShowTeachingInfo : Command 
    {
        [SerializeField] private bool show;
        [SerializeField] private string info;
        // [SerializeField] private bool setBlur = true;
        public override void OnEnter()
        {
            if(show)
                TeachingUI.Open(info);
            else
                TeachingUI.Close();

            Continue();
        }

        public override string GetSummary()
        {
            string infos = show ? "顯示訊息：" : "關閉訊息：";
            infos += info;
            
            return infos;
        }

        public override Color GetButtonColor()
        {
            return new Color32(216, 228, 170, 255);
        }

    }
}