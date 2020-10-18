using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorBody : MonoBehaviour
{
    [SerializeField]
    GameObject playerObj;
    [SerializeField]
    GameObject enemyObj;
    [SerializeField]
    GameObject enemyShakeObj;
    [SerializeField]
    int CharacterNumber;
    public Transform parent;
    public Animator ani;
    [SerializeField]
    PlayerController playerConScript;
    public Rigidbody2D enemyRigidbody;
    private float x, y, damage;

    void Start()
    {
        //playerController = parent.GetComponent<PlayerController>();
    }
    private void Update()
    {
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && !ani.GetCurrentAnimatorStateInfo(0).IsName("Idle")) 
        {

            print("Hit!");
            if (ani.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                x = 6;
                y = 3;
                damage = 20;
            }
            else if (ani.GetCurrentAnimatorStateInfo(0).IsName("UpAttack"))
            {
                x = 0;
                y = 12;
                damage = 5;
            }
            else if (ani.GetCurrentAnimatorStateInfo(0).IsName("AirAttack"))
            {
                x = 3;
                y = 8;
                damage = 5;
            }
            else if (ani.GetCurrentAnimatorStateInfo(0).IsName("SpecialAttack"))
            {
                x = 10;
                y = 10;
                damage = 10;
            }
            if (playerObj.transform.localScale.x < 0) 
            {
                x *= -1;
            }
            Damage(x, y, enemyObj.transform.position.x - playerObj.transform.position.x, enemyObj.transform.position.y - playerObj.transform.position.y);
        }
        
    }
    void Damage(float xVelocity, float yVelocity, float x_direction, float y_direction)
    {
        enemyRigidbody.velocity = new Vector2(xVelocity, yVelocity);
        StartCoroutine(HitWiggle(damage));
        if (CharacterNumber == 0)
        {
            BattlePratitiData.enemy_hp -= damage;
            BattlePratitiData.player_energy += 0.1f * damage;
            //BattlePratitiData.enemy_hp -= Mathf.Pow((x * x + y * y), 0.5f);
            //BattlePratitiData.player_energy += 0.1f * Mathf.Pow((x * x + y * y), 0.5f);
        }
        else
        {
            BattlePratitiData.player_hp -= damage;
            BattlePratitiData.enemy_energy += 0.1f * damage;
            //BattlePratitiData.player_hp -= Mathf.Pow((x * x + y * y), 0.5f);
            //BattlePratitiData.enemy_energy += 0.1f * Mathf.Pow((x * x + y * y), 0.5f);
        }
    }
    IEnumerator HitWiggle(float power)
    {
        int times = 0;
        float x_move, y_move, moveRange;
        moveRange = power;
        while (times < 5)
        {
            x_move = 0.02f * Random.Range(-moveRange, moveRange + 1);
            y_move = 0.005f * Random.Range(-moveRange, moveRange/2 + 1);
            enemyShakeObj.transform.position = new Vector3(x_move, y_move, 0);
            times++;
            print(x_move);
            yield return new WaitForSeconds(0.03f);
        }
        enemyShakeObj.transform.position = Vector3.zero;
    }
}
