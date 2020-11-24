using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace InventorySystem {
    public class ItemAssets : MonoBehaviour {
        public static ItemAssets Instance {get; private set;}

        public Sprite blueberrySprite;
        public Sprite butterSprite;
        public Sprite chocolateSprite;
        public Sprite eggSprite;
        public Sprite happyweedSprite;
        public Sprite milkSprite;
        public Sprite nutSprite;
        public Sprite reinSprite;
        public Sprite cakeSprite;
        void Awake() {
            Instance = this;
        }
    }
}
