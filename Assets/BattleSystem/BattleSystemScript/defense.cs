using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
namespace NameBattleSystem
{
    public class defense : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public static int defStatus;
        public GameObject playerShield;

        void Update()
        {
            if (BattleSystem.player.isHitRecovery == true)
            {
                defStatus = 0;
                playerShield.SetActive(false);
            }
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            if (defense.defStatus != 1 && Time.timeScale == 1 && BattleSystem.player.isHitRecovery == false && control.isCatching == false)
            {
                if (BattleSystem.player.defEmpty != 0 || BattleSystem.player.defEmpty != 0)
                {
                    defense.defStatus = 1;
                }
                BattleSystem.player.def -= BattleSystem.player.maxDef * 0.05f;
                BattleSystem.player.mDef -= BattleSystem.player.maxMDef * 0.05f;
            }
        }
        public void OnPointerUp(PointerEventData eventData)
        {
            if (Time.timeScale == 1)
            {
                defStatus = 0;
                playerShield.SetActive(false);
            }
        }
    }
}
