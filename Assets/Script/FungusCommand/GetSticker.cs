// This code is part of the Fungus library (https://github.com/snozbot/fungus)
// It is released for free under the MIT open source license (https://github.com/snozbot/fungus/blob/master/LICENSE)

using UnityEngine;

namespace Fungus
{

    [CommandInfo("ForPratiti", 
                 "GetSticker", 
                 "獲得貼紙")]
    [AddComponentMenu("")]
    public class GetSticker : Command 
    {
        public StickerType type;
        public int addNum;
        public override void OnEnter()
        {
            GameMediator.Instance.AddSticker(type, addNum);
            Continue();
        }

        public override string GetSummary()
        {
            return $"獲得{addNum}個{type}的貼紙";
        }

        public override Color GetButtonColor()
        {
            return new Color32(216, 228, 170, 255);
        }

    }
}