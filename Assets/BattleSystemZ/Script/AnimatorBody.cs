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
    GameObject hurt_effectPrefab;
    [SerializeField]
    int CharacterNumber;
    public Transform parent;
    public Animator ani;
    [SerializeField]
    PlayerController playerConScript;
    [SerializeField]
    PlayerController enemyConScript;
    [SerializeField]
    BattleCameraControl cameraConScript;

    public Rigidbody2D enemyRigidbody;
    private float x, y, damage;
    bool hitTarget;
    private float atk = 1;
    private float enemyDef = 1;

    void Start()
    {
        if(CharacterNumber == 0){
            atk = BattlePratitiData.player_atk;
            enemyDef = BattlePratitiData.enemy_maxDef;
            Debug.LogWarning($"Enemy:def{enemyDef }");
        }  
        else{
            atk = BattlePratitiData.enemy_atk;
            enemyDef = BattlePratitiData.player_maxDef;
            Debug.LogWarning($"Player:def{enemyDef }");
        }
            
        //playerController = parent.GetComponent<PlayerController>();
    }
    private void Update()
    {
        if(!playerConScript.isHitting)
        {
            hitTarget = false;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && playerConScript.isHitting && !ani.GetCurrentAnimatorStateInfo(0).IsName("Idle")) 
        {
            hitTarget = true;
            if (ani.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                x = 10;
                y = 15;
                damage = 21;
            }
            else if (ani.GetCurrentAnimatorStateInfo(0).IsName("UpAttack"))
            {
                x = 0;
                y = 20;
                damage = 14;
            }
            else if (ani.GetCurrentAnimatorStateInfo(0).IsName("AirAttack"))
            {
                x = 10;
                y = 15;
                damage = 10;
            }
            else if (ani.GetCurrentAnimatorStateInfo(0).IsName("SpecialAttack"))
            {
                x = 6;
                y = 8;
                damage = 1;
            }
            if (playerObj.transform.localScale.x < 0) 
            {
                x *= -1;
            }

            // 加上攻擊,防禦參數參數
            float defPlus =  100f / enemyDef;

            float df = damage * atk *  defPlus;
            damage = Mathf.Ceil(df);

            if(CharacterNumber == 0){
                Debug.LogWarning($"Player Hit Enemy:{damage}");
            }  
            else{
                Debug.LogWarning($"Enemy hit Player:{damage}");
            }
            

            Damage(x, y, enemyObj.transform.position.x - playerObj.transform.position.x, enemyObj.transform.position.y - playerObj.transform.position.y);
        }
        
    }
    void Damage(float xVelocity, float yVelocity, float x_direction, float y_direction)
    {
        // print("Hit!");
        //hitTarget = true;
        playerConScript.isHitting = false;
        StartCoroutine(HitWiggle(damage));
        if(ani.GetCurrentAnimatorStateInfo(0).IsName("SpecialAttack"))
        {
            StartCoroutine(enemyConScript.StartWiggle(damage / 100, xVelocity, yVelocity, combo: true));
        } else
        {
            StartCoroutine(enemyConScript.StartWiggle(damage / 100, xVelocity, yVelocity));
        }
        StartCoroutine(cameraConScript.CameraShake(0.1f, 10, 0.05f));
        BattleControl.InstantiateEffect(enemyObj, hurt_effectPrefab);
        if (CharacterNumber == 0)
        {
            // print(damage);
            BattlePratitiData.enemy_hp -= damage;
            BattlePratitiData.player_energy += 0.1f * damage;
            //BattlePratitiData.enemy_hp -= Mathf.Pow((x * x + y * y), 0.5f);
            //BattlePratitiData.player_energy += 0.1f * Mathf.Pow((x * x + y * y), 0.5f);
        }
        else
        {
            // print(damage);
            BattlePratitiData.player_hp -= damage;
            BattlePratitiData.enemy_energy += 0.1f * damage;
            //BattlePratitiData.player_hp -= Mathf.Pow((x * x + y * y), 0.5f);
            //BattlePratitiData.enemy_energy += 0.1f * Mathf.Pow((x * x + y * y), 0.5f);
        }
    }
    IEnumerator HitWiggle(float power)
    {
        StartCoroutine(enemyConScript.StartHurt(1));
        int times = 0;
        float x_move, y_move, moveRange;
        moveRange = power;
        while (times < 5)
        {
            x_move = 0.02f * Random.Range(-moveRange, moveRange + 1);
            y_move = 0.005f * Random.Range(-moveRange, moveRange/2 + 1);
            enemyShakeObj.transform.position = new Vector3(x_move, y_move, 0);
            times++;
            //print(x_move);
            yield return new WaitForSeconds(0.03f);
        }
        enemyShakeObj.transform.position = Vector3.zero;
    }
}
