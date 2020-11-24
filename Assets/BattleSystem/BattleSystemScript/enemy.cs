using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace NameBattleSystem
{
    public class enemy : MonoBehaviour
    {
        public GameObject Gameobj;
        public Animator Ani;
        public Rigidbody2D rig2d;
        public int hitStatus = 0;
        public int currentFrame = 1;
        public bool isAttacking = false;
        public bool isInterrputed = false;
        public bool isWait = false;
        public float distanceX;
        public float timer;
        public float force;
        public Vector3 position;
        void FixedUpdate()
        {
            //print(Ani.GetCurrentAnimatorStateInfo(0).normalizedTime);
            if (Ani.GetCurrentAnimatorStateInfo(0).IsName("Attack") && BattleSystem.RCD01 <= 0)
            {
                //currentFrame++;
                isAttacking = true;
            }
            if (Ani.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.3f && Ani.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                Ani.SetTrigger("Hit Miss");
                //currentFrame = 1;
                isAttacking = false;
                BattleSystem.RCD01 = BattleSystem.ability04.cd;
            }
            if (isInterrputed)//角色處於提前中斷攻擊狀態，角色開始跳躍回初始位置
            {
                //角色水平位置漸漸回到初始水平位置
                this.transform.position -= new Vector3(0.025f * distanceX, 0);
                if (isWait)
                {
                    if (distanceX > 0 && this.transform.position.x <= 4)
                    {
                        this.transform.position = new Vector3(4, this.transform.position.y);
                    }
                    else if (distanceX < 0 && this.transform.position.x >= 4) 
                    {
                        this.transform.position = new Vector3(4, this.transform.position.y);
                    }

                    if (distanceX > 0 && this.transform.position.x <= 4 && this.transform.position.y <= -0.5f)
                    {
                        rig2d.constraints = RigidbodyConstraints2D.FreezePositionY;
                        isInterrputed = false;
                        isWait = false;
                        this.transform.position = new Vector3(4, -1);
                    }
                    else if (distanceX < 0 && this.transform.position.x >= 4 && this.transform.position.y <= -0.5f)
                    {
                        rig2d.constraints = RigidbodyConstraints2D.FreezePositionY;
                        isInterrputed = false;
                        isWait = false;
                        this.transform.position = new Vector3(4, -1);
                    }
                }
            }
            else if (Ani.GetCurrentAnimatorStateInfo(0).IsName("Hit Interrupt") && Ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
            {
                this.transform.position = new Vector3(0, 0);
                BattleSystem.enemy.hitRecoveryTime = 0;
            }
        }
        public void OnTriggerStay2D(Collider2D collider)
        {
            if (collider.gameObject.tag == "BattlePratiti" && isAttacking == true)
            {
                if (Ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.19f && Ani.GetCurrentAnimatorStateInfo(0).normalizedTime <= 0.3f)
                {
                    print("enemycomplete");
                    GameObject.Find("battle").GetComponent<BattleSystem>().attack(BattleSystem.enemy, BattleSystem.player, BattleSystem.ability04);
                    //角色攻擊完整地結束，攻擊判定結束
                    //currentFrame = 1;
                    isAttacking = false;
                    Ani.SetTrigger("Hit Complete");
                    BattleSystem.RCD01 = BattleSystem.ability04.cd;

                }
                else if (Ani.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.19f) 
                {
                    print("enemyinterrupt");
                    GameObject.Find("battle").GetComponent<BattleSystem>().attack(BattleSystem.enemy, BattleSystem.player, BattleSystem.ability04); ;
                    //currentFrame = 1;
                    isAttacking = false;
                    interrupt();    //呼叫提前中斷攻擊的函式
                    BattleSystem.RCD01 = BattleSystem.ability04.cd;   //攻擊進入冷卻
                }
            }
        }
        public void interrupt() //提前中斷攻擊函式
        {
            //角色處於提前中斷攻擊狀態
            isInterrputed = true;
            //宣告角色碰撞時的座標以及與角色初始位置的水平距離
            position = Gameobj.transform.position;
            distanceX = Gameobj.transform.position.x - 4;
            force = Math.Abs(85 / distanceX);//計算角色跳躍上升力的大小
            Ani.SetTrigger("Hit Interrupt");
            //規定角色跳躍上升力大小的範圍
            if (force > 30)
            {
                force = 30;
            }
            else if (force < 25)
            {
                force = 25;
            }

            //給予角色遊戲物件物理跳躍上升力
            rig2d.constraints = RigidbodyConstraints2D.None;
            rig2d.AddForce(new Vector2(0, force), ForceMode2D.Impulse);

            StartCoroutine(Position());

            print("敵人上升力為"+force);

            //角色進入硬直時間
            BattleSystem.player.hitRecoveryTimeMax = 0.95f;
            BattleSystem.player.hitRecoveryTime = 0.95f;
        }
        IEnumerator Wait()
        {
            yield return new WaitForFixedUpdate();
            yield return new WaitForFixedUpdate();
            yield return new WaitForFixedUpdate();
            yield return new WaitForFixedUpdate();
            yield return new WaitForFixedUpdate();
            isWait = true;
            StopCoroutine("Wait");
        }
        IEnumerator Position()
        {
            StartCoroutine(Wait());
            yield return new WaitForFixedUpdate();
            yield return new WaitForFixedUpdate();
            this.transform.position = position;
            StopCoroutine("Position");
        }
    }
}


