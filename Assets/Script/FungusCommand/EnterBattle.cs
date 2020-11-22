// This code is part of the Fungus library (https://github.com/snozbot/fungus)
// It is released for free under the MIT open source license (https://github.com/snozbot/fungus/blob/master/LICENSE)

using UnityEngine;

namespace Fungus
{

    [CommandInfo("ForPratiti", 
                 "EnterBattle", 
                 "進入戰鬥")]
    [AddComponentMenu("")]
    public class EnterBattle : Command 
    {
        // [Header("將自己放進來")]
        public MapPratiti _pratiti;

        public override void OnEnter()
        {
            GameMediator.Instance.SetEnemyPratiti(_pratiti);
            GameMediator.Instance.EnterBattle();

        }

        public override string GetSummary()
        {
            return "進入戰鬥";
        }

        public override Color GetButtonColor()
        {
            return new Color32(216, 228, 170, 255);
        }

    }
}