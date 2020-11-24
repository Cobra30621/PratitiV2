using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace NameBattleSystem
{
    public class Player : MonoBehaviour
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
        public float force;
        public Vector3 position;

        /*1.65f
         2.3f
         3.5f
         */
        void Update()
        {
        }
        void FixedUpdate()
        {
            //print(Ani.GetCurrentAnimatorStateInfo(0).normalizedTime);
            if (Ani.GetCurrentAnimatorStateInfo(0).IsName("Attack") && BattleSystem.LCD <= 0)
            {
                //若角色開始攻擊則開始進入命中判定
                isAttacking = true;
                //currentFrame++; //計算動畫持續的幀數
            }
            if (Ani.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.97f && Ani.GetCurrentAnimatorStateInfo(0).IsName("Attack")) //角色攻擊動畫進度超過一般攻擊判定期間
            {
                //角色攻擊未命中，攻擊判定結束
                isAttacking = false;
                //currentFrame = 1;
                Ani.SetTrigger("Hit Miss");
                BattleSystem.LCD = BattleSystem.ability01.cd;   //攻擊進入冷卻
            }
            if (isInterrputed)  //角色處於提前中斷攻擊狀態，角色開始跳躍回初始位置
            {
                //角色水平位置漸漸回到初始水平位置
                this.transform.position -= new Vector3(0.025f * distanceX, 0);
                if (isWait)
                {
                    //若角色已回到初始位置則回到靜止初始狀態
                    if (distanceX > 0 && this.transform.position.x <= -4 * this.transform.localScale.x)
                    {
                        this.transform.position = new Vector3(-4 * this.transform.localScale.x, this.transform.position.y);
                    }
                    else if (distanceX < 0 && this.transform.position.x >= -4 * this.transform.localScale.x)
                    {
                        this.transform.position = new Vector3(-4 * this.transform.localScale.x * this.transform.localScale.x, this.transform.position.y);
                    }

                    if (distanceX > 0 && this.transform.position.x <= -4 * this.transform.localScale.x && this.transform.position.y <= -0.5f)
                    {
                        rig2d.constraints = RigidbodyConstraints2D.FreezePositionY;
                        this.transform.position = new Vector3(-4 * this.transform.localScale.x, -0.5f);
                        isInterrputed = false;
                        isWait = false;
                    }
                    else if (distanceX < 0 && this.transform.position.x >= -4 && this.transform.position.y <= -0.5f)
                    {
                        rig2d.constraints = RigidbodyConstraints2D.FreezePositionY;
                        this.transform.position = new Vector3(-4, -0.5f);
                        isInterrputed = false;
                        isWait = false;
                    }
                }
            }
            else if (Ani.GetCurrentAnimatorStateInfo(0).IsName("Hit Interrupt") && Ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f) 
            {
                this.transform.position = new Vector3(0, 0);
            }
        }

        public void OnTriggerStay2D(Collider2D collider)    //角色與角色的碰撞函式
        {
            print("碰撞到" + collider.name);
            if (Ani.GetCurrentAnimatorStateInfo(0).IsName("Attack")) 
            {
                if (collider.gameObject.tag == "BattlePratiti" && isAttacking == true) //撞擊對象為敵人且處於攻擊判定
                {
                    //print(currentFrame);
                    if (Ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.92f && Ani.GetCurrentAnimatorStateInfo(0).normalizedTime <= 0.97f) //角色攻擊動畫進度處於一般攻擊判定期間
                    {
                        print("player attack complete");
                        //呼叫攻擊函式
                        GameObject.Find("battle").GetComponent<BattleSystem>().attack(BattleSystem.player, BattleSystem.enemy, BattleSystem.ability01);
                        //角色攻擊完整地結束，攻擊判定結束
                        isAttacking = false;
                        //currentFrame = 1;
                        Ani.SetTrigger("Hit Complete");
                        BattleSystem.LCD = BattleSystem.ability01.cd;   //攻擊進入冷卻
                    }
                    else if (Ani.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.92f)   //角色攻擊動畫進度提前於一般攻擊判定期間
                    {
                        print("player attack interrupt");
                        //呼叫攻擊函式
                        GameObject.Find("battle").GetComponent<BattleSystem>().attack(BattleSystem.player, BattleSystem.enemy, BattleSystem.ability01);
                        //角色攻擊提前中斷結束，攻擊判定結束
                        isAttacking = false;
                        //currentFrame = 1;
                        interrupt();    //呼叫提前中斷攻擊的函式
                        BattleSystem.LCD = BattleSystem.ability01.cd;   //攻擊進入冷卻
                    }
                }
            }
        }
        public void interrupt() //提前中斷攻擊函式
        {
            //角色處於提前中斷攻擊狀態
            isInterrputed = true;
            //宣告角色碰撞時的座標以及與角色初始位置的水平距離
            position = Gameobj.transform.position;
            distanceX = Gameobj.transform.position.x * this.transform.localScale.x - (-4 * this.transform.localScale.x);
            //distanceX = distanceX * this.transform.localScale.x;
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
            
            print("玩家上升力為" + force);

            StartCoroutine(Position());

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