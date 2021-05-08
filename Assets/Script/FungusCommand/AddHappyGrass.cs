// This code is part of the Fungus library (https://github.com/snozbot/fungus)
// It is released for free under the MIT open source license (https://github.com/snozbot/fungus/blob/master/LICENSE)

using UnityEngine;

namespace Fungus
{

    [CommandInfo("ForPratiti", 
                 "AddHappyGrass", 
                 "增加快樂草")]
    [AddComponentMenu("")]
    public class AddHappyGrass : Command 
    {
        public int num;
        public override void OnEnter()
        {
            GameMediator.Instance.AddHappyGrass(num);
            Continue();
        }

        public override string GetSummary()
        {
            return $"增加{num}個快樂草";
        }

        public override Color GetButtonColor()
        {
            return new Color32(216, 228, 170, 255);
        }

    }
}