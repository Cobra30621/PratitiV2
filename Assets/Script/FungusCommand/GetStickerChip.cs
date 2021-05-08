// This code is part of the Fungus library (https://github.com/snozbot/fungus)
// It is released for free under the MIT open source license (https://github.com/snozbot/fungus/blob/master/LICENSE)

using UnityEngine;

namespace Fungus
{

    [CommandInfo("ForPratiti", 
                 "GetStickerChip", 
                 "獲得貼紙碎片")]
    [AddComponentMenu("")]
    public class GetStickerChip : Command 
    {
        public StickerType type;
        public int addNum;
        public override void OnEnter()
        {
            GameMediator.Instance.AddStickerChip(type, addNum);
            Continue();
        }

        public override string GetSummary()
        {
            return $"獲得{addNum}個{type}的貼紙碎片";
        }

        public override Color GetButtonColor()
        {
            return new Color32(216, 228, 170, 255);
        }

    }
}