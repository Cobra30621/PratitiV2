// This code is part of the Fungus library (https://github.com/snozbot/fungus)
// It is released for free under the MIT open source license (https://github.com/snozbot/fungus/blob/master/LICENSE)

using UnityEngine;

namespace Fungus
{

    [CommandInfo("ForPratiti", 
                 "SetBlur", 
                 "背景模糊")]
    [AddComponentMenu("")]
    public class SetBlur : Command 
    {
        [SerializeField] private bool setBlur = true;
        public override void OnEnter()
        {
            GameMediator.Instance.SetBlurPanel(setBlur);
            Continue();
        }

        public override string GetSummary()
        {
            return  setBlur ? "開啟背景模糊" : "關閉背景模糊";
        }

        public override Color GetButtonColor()
        {
            return new Color32(216, 228, 170, 255);
        }

    }
}