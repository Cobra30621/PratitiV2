// This code is part of the Fungus library (https://github.com/snozbot/fungus)
// It is released for free under the MIT open source license (https://github.com/snozbot/fungus/blob/master/LICENSE)

using UnityEngine;

namespace Fungus
{

    [CommandInfo("ForPratiti", 
                 "EnterEnforceBattle", 
                 "進入強制戰鬥")]
    [AddComponentMenu("")]
    public class EnterEnforceBattle : Command 
    {
        public MapPratiti _pratiti;
        public string blockName;
        
        public override void OnEnter()
        {
            GameMediator.Instance.SetEndBattlerStory(blockName);

            GameMediator.Instance.SetEnemyPratiti(_pratiti);
            GameMediator.Instance.EnterBattle();
        }

        public override string GetSummary()
        {
            return $"戰鬥結束後，播放劇情:{blockName}";
        }

        public override Color GetButtonColor()
        {
            return new Color32(216, 228, 170, 255);
        }

    }
}