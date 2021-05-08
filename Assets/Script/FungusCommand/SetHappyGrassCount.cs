// This code is part of the Fungus library (https://github.com/snozbot/fungus)
// It is released for free under the MIT open source license (https://github.com/snozbot/fungus/blob/master/LICENSE)

using UnityEngine;

namespace Fungus
{

    [CommandInfo("ForPratiti", 
                 "SetHappyGrassCount", 
                 "設定快樂草數量到FlowChart變數中")]
    [AddComponentMenu("")]
    public class SetHappyGrassCount : Command 
    {
        public Flowchart flowchart;
        public override void OnEnter()
        {
            int num = GameMediator.Instance.GetHappyGrassCount();
            flowchart.SetIntegerVariable("happyGrassNum", num);
            Continue();
        }

        public override string GetSummary()
        {
            return $"設定快樂草變數";
        }

        public override Color GetButtonColor()
        {
            return new Color32(216, 228, 170, 255);
        }

    }
}