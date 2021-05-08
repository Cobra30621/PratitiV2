// This code is part of the Fungus library (https://github.com/snozbot/fungus)
// It is released for free under the MIT open source license (https://github.com/snozbot/fungus/blob/master/LICENSE)

using UnityEngine;

namespace Fungus
{

    [CommandInfo("ForPratiti", 
                 "IsTalking", 
                 "是否開始對話")]
    [AddComponentMenu("")]
    public class IsTalking : Command 
    {
        [SerializeField] private bool bo;
        // [SerializeField] private bool setBlur = true;
        public override void OnEnter()
        {
            GameMediator.Instance.SetIsTalking(bo);
            GameMediator.Instance.SetBlurPanel(bo);
            Continue();
        }

        public override string GetSummary()
        {
            string info = bo ? "開始對話" : "結束對話";

            // if(setBlur)
            //     info += ",開啟背景模糊" ;
            // else
            //     info += ",關閉背景模糊";
            
            return info;
        }

        public override Color GetButtonColor()
        {
            return new Color32(216, 228, 170, 255);
        }

    }
}