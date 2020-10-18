// This code is part of the Fungus library (https://github.com/snozbot/fungus)
// It is released for free under the MIT open source license (https://github.com/snozbot/fungus/blob/master/LICENSE)

using UnityEngine;

namespace Fungus
{

    [CommandInfo("ForPratiti", 
                 "MovePlayer", 
                 "轉移玩家+轉場")]
    [AddComponentMenu("")]
    public class MovePlayer : Command 
    {
        [SerializeField] private Transform  pos;
        [SerializeField] private MapName map;
        public override void OnEnter()
        {
            GameMediator.Instance.ChangeScene(pos.position, map);
        }

        public override string GetSummary()
        {
            return $"轉移至{map}的{pos.position}";
        }

        public override Color GetButtonColor()
        {
            return new Color32(216, 228, 170, 255);
        }

    }
}