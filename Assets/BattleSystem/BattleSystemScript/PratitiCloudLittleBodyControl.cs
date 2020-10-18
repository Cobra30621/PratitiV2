using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace NameBattleSystem
{

    public class PratitiCloudLittleBodyControl : MonoBehaviour
    {
        public GameObject explosion;
        void Start()
        {
            StartCoroutine(DestroyLittleBodyBomb());
        }

        public void OnTriggerStay2D(Collider2D collider)
        {
            if (collider.gameObject.name == "Enemy")
            {
                GameObject.Find("battle").GetComponent<BattleSystem>().attack(BattleSystem.player, BattleSystem.enemy, BattleSystem.ability01);
                Destroy(this.gameObject);
                //Instantiate(explosion, explosion.transform.position, explosion.transform.rotation);
            }
        }
        IEnumerator DestroyLittleBodyBomb()
        {
            yield return new WaitUntil(() => this.gameObject.transform.position.y < -1.8f);
            Destroy(this.gameObject);
        }
    }
}
