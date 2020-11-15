// This code is part of the Fungus library (https://github.com/snozbot/fungus)
// It is released for free under the MIT open source license (https://github.com/snozbot/fungus/blob/master/LICENSE)

using UnityEngine;

namespace Fungus
{

    [CommandInfo("ForPratiti", 
                 "AddPratiti", 
                 "產生包包帕拉提提")]
    [AddComponentMenu("")]
    public class AddPratiti : Command 
    {
        public PratitiType type;
        public override void OnEnter()
        {
            GameMediator.Instance.CreateBagPratiti(type);
            Continue();
        }

        public override string GetSummary()
        {
            return $"產生{type}的包包帕拉提提";
        }

        public override Color GetButtonColor()
        {
            return new Color32(216, 228, 170, 255);
        }

    }
}