using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NameBattleSystem;
using UnityEngine.SceneManagement;

public class EnemyEmptyCollide : MonoBehaviour 
{
    public GameObject Heart;
    public GameObject HeartClone;
    public GameObject HeartBroke;
    public GameObject HeartBrokeClone;
    public GameObject Enemy;
    public int chance;
    private void FixedUpdate()
    {
        if (HeartClone != null)
        {
            HeartClone.transform.position = new Vector3(Enemy.transform.position.x - 0.64f, Enemy.transform.position.y + 1f);
        }
        if (HeartBrokeClone != null)
        {
            HeartBrokeClone.transform.position = new Vector3(Enemy.transform.position.x - 1.17f, Enemy.transform.position.y - 0.3f);
        }
    }
    public void OnTriggerStay2D(Collider2D collider)
    {
        print(collider.gameObject);
        if (CatchSystem.isTop == true && BattleSystem.enemy.isHitRecovery == false)
        {
            control.DecideingCatch = true;
            if (collider.gameObject.name == "Chocolate")
            {
                chance = 50;
                StartCoroutine(DecideCatch(chance));
            }
            if (collider.gameObject.name == "Blueberry")
            {
                chance = 60;
                StartCoroutine(DecideCatch(chance));
            }
            if (collider.gameObject.name == "Egg")
            {
                chance = 70;
                StartCoroutine(DecideCatch(chance));
            }
            if (collider.gameObject.name == "Butter")
            {
                chance = 80;
                StartCoroutine(DecideCatch(chance));
            }
            if (collider.gameObject.name == "Milk")
            {
                chance = 90;
                StartCoroutine(DecideCatch(chance));
            }
            if (collider.gameObject.name == "Nut")
            {
                chance = 100;
                StartCoroutine(DecideCatch(chance));
            }
        }
    }
    IEnumerator DecideCatch(int chance)
    {
        BattleSystem.enemy.isHitRecovery = true;
        print("Start Deciding");
        HeartClone = Instantiate(Heart, new Vector3(Enemy.transform.position.x - 0.64f, Enemy.transform.position.y + 1f), new Quaternion(0, 0, 0, 0));
        print(1);
        yield return new WaitForSecondsRealtime(0.5f);
        print(1);
        HeartClone.transform.localScale += new Vector3(0.02f, 0.02f);
        yield return new WaitForSecondsRealtime(0.5f);
        print(1);
        HeartClone.transform.localScale -= new Vector3(0.02f, 0.02f);
        yield return new WaitForSecondsRealtime(0.5f);
        print(1);
        HeartClone.transform.localScale += new Vector3(0.02f, 0.02f);
        yield return new WaitForSecondsRealtime(0.5f);
        print(1);
        HeartClone.transform.localScale -= new Vector3(0.02f, 0.02f);
        yield return new WaitForSecondsRealtime(0.5f);
        print(1);
        HeartClone.transform.localScale += new Vector3(0.02f, 0.02f);
        yield return new WaitForSecondsRealtime(0.5f);
        print(1);
        if (chance >= Random.Range(1, 101) - 50*((BattleSystem.enemyMaxHp - BattleSystem.enemy.hp)/BattleSystem.enemyMaxHp))
        {
            HeartClone.transform.localScale += new Vector3(0.03f, 0.03f);
            yield return new WaitForSecondsRealtime(0.5f);
            Destroy(HeartClone.gameObject);
            BattleSystem.isEnd = true;
        }
        else
        {
            Destroy(HeartClone.gameObject);
            HeartBrokeClone = Instantiate(HeartBroke, new Vector3(Enemy.transform.position.x - 1.17f, Enemy.transform.position.y - 0.3f), new Quaternion(0, 0, 0, 0));
            yield return new WaitForSecondsRealtime(0.5f);
            Destroy(HeartBrokeClone.gameObject);
        }
        Destroy(control.DessertClone.gameObject);
        BattleSystem.player.isHitRecovery = false;
        BattleSystem.enemy.isHitRecovery = false;
        control.DecideingCatch = false;
        control.isCatching = false;
        StopCoroutine("DecideCatch");
    }
}