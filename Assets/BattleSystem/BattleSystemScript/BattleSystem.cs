using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;
//using NameMapSystem;

namespace NameBattleSystem
{
    public class BattleSystem : MonoBehaviour
    {
        public class pratiti
        {
            public float atk;
            public float mAtk;
            public float def;
            public float mDef;
            public float agi;
            public float hp;
            public float energy;
            public float maxDef;
            public float maxMDef;
            public int defEmpty;
            public int mDefEmpty;
            public int defStatus;
            public float hurtTime;
            public float attackStartHitRecoveryTime;
            public float attackCompleteHitRecoveryTime;
            public float attackMissHitRecoveryTime;
            public float abilityStartHitRecoveryTime;
            public float abilityEndHitRecoveryTime;
            public float hitRecoveryTime;
            public float hitRecoveryTimeMax;
            public GameObject gameObj01;
            public GameObject gameObj02;
            public GameObject gameObj03;
            public GameObject gameObj04;
            public GameObject gameObj05;
            public GameObject gameObj06;
            public GameObject gameObj07;
            public bool isHitRecovery;
            public bool isParryReady;
            public bool isParry;
            public bool ParrySuccess;
            public pratiti(float attack, float magicAttack, float defense, float magicDefense, float agility, float hitpoint, float Energy, float maxDefense, float maxMagicDefense, int defenseEmpty, int magicDefenseEmpty, int defenseStatus, float hurt_Time, float AttackStartHitRecoveryTime, float AttackCompleteHitRecoveryTime, float AttackMissHitRecoveryTime, float AbilityStartHitRecoveryTime, float AbilityEndHitRecoveryTime, float HitRecoveryTime, float HitRecoveryTimeMax, GameObject gameObject01, GameObject gameObject02, GameObject gameObject03, GameObject gameObject04, GameObject gameObject05, GameObject gameObject06, GameObject gameObject07, bool IsHitReacovery, bool IsParryReady, bool IsParry, bool ParrySuccessful)
            {
                atk = attack;
                mAtk = magicAttack;
                def = defense;
                mDef = magicDefense;
                agi = agility;
                hp = hitpoint;
                energy = Energy;
                maxDef = maxDefense;
                maxMDef = maxMagicDefense;
                defEmpty = defenseEmpty;
                mDefEmpty = magicDefenseEmpty;
                defStatus = defenseStatus;
                hurtTime = hurt_Time;
                attackStartHitRecoveryTime = AttackStartHitRecoveryTime;
                attackCompleteHitRecoveryTime = AttackCompleteHitRecoveryTime;
                attackMissHitRecoveryTime = AttackMissHitRecoveryTime;
                abilityStartHitRecoveryTime = AbilityStartHitRecoveryTime;
                abilityEndHitRecoveryTime = AbilityEndHitRecoveryTime;
                hitRecoveryTime = HitRecoveryTime;
                hitRecoveryTimeMax = HitRecoveryTimeMax;
                gameObj01 = gameObject01;
                gameObj02 = gameObject02;
                gameObj03 = gameObject03;
                gameObj04 = gameObject04;
                gameObj05 = gameObject05;
                gameObj06 = gameObject06;
                gameObj07 = gameObject07;
                isHitRecovery = IsHitReacovery;
                isParryReady = IsParryReady;
                isParry = IsParry;
                ParrySuccess = ParrySuccessful;
            }
        }
        public class ability
        {
            public float power;
            public float cd;
            public float hitRate;
            public float gainEnergy;
            public float needEnergy;
            public float leftSide;
            public float rightSide;
            public int hitTimes;
            public int totalHitTimes;
            public ability(float Power, float coolDown, float hit_Rate, float gainingEnergy)
            {
                //一般攻擊的建構元
                power = Power;
                cd = coolDown;
                hitRate = hit_Rate;
                gainEnergy = gainingEnergy;
            }
            public ability(float Power, float hit_Rate, float neededEnergy, float LeftSide, float RightSide, int HitTimes, int TotalHitTimes)
            {
                //技能的建構元
                power = Power;
                hitRate = hit_Rate;
                needEnergy = neededEnergy;
                leftSide = LeftSide;
                rightSide = RightSide;
                hitTimes = HitTimes;
                totalHitTimes = TotalHitTimes;
            }
        }
        public static pratiti player;
        public static pratiti enemy;
        public static ability ability01 = new ability(75, 3, 90, 10);
        public ability ability02 = new ability(150, 85, 20, -5, 7.5f, 0, 1);
        public static ability ability04 = new ability(75, 3, 90, 10);
        public ability ability05 = new ability(30, 85, 30, -5.5f, 3, 0, 5);
        public GameObject Player;
        public GameObject PlayerPart01;
        public GameObject PlayerPart02;
        public GameObject PlayerPart03;
        public GameObject PlayerPart04;
        public GameObject PlayerPart05;
        public GameObject PlayerPart06;
        public GameObject PlayerPart07;
        public GameObject Enemy;
        public GameObject EnemyEmpty;
        public GameObject leftHpBar;
        public GameObject leftDefBar;
        public GameObject leftMdefBar;
        public GameObject leftEnergyBar;
        public GameObject rightEnergyBar;
        public GameObject rightHpBar;
        public GameObject rightDefBar;
        public GameObject rightMdefBar;
        public GameObject CDBar;
        public GameObject CDBar02;
        public GameObject hitRecoveryBarMask01;
        public GameObject hitRecoveryBarMask02;
        public GameObject Defense;
        public GameObject PressDefense;
        public GameObject playerShield;
        public GameObject playerParryShield;
        public GameObject enemyShield;
        public GameObject miss;
        public GameObject damage;
        public GameObject PlayerHealth;
        public GameObject EnemyHealth;
        public GameObject PlayerAbility01;
        public GameObject PlayerAbility01Obj;
        public GameObject EnemyAbility01;
        public GameObject EnemyAbility01Obj;
        public GameObject PlayerAbility;
        public GameObject PlayerAbilityObj;
        public GameObject EnemyAbility;
        public GameObject EnemyAbilityObj;
        public GameObject SceneChange;
        public GameObject AttackButton;
        public GameObject AbilityButton;
        public GameObject ParryButton;
        public GameObject PressAttackButton;
        public GameObject PressAbilityButton;
        public GameObject PressParryButton;
        public GameObject MENU;
        public GameObject Camera;
        public GameObject PlayerAbilityHint;
        public GameObject EnemyAbilityHint;
        public Animator PlayerAbilityHintAnimator;
        public Animator EnemyAbilityHintAnimator;
        public Animator playerAnimation;
        public Animator enemyAnimation;
        public Animator PlayerParryShieldAnimator;
        public float playerHP;
        public float enemyHP;
        public float playerMaxHp;
        public static float enemyMaxHp;
        public static float LCD;
        public static float RCD01;
        public static float RCD02;
        public float LCDMax;
        public float ButtonPressTime;
        public float chance;
        public float random01;
        public float random02;
        public float Damage;
        public float ParryTime;
        public float DelayNumber;
        public static bool isEnd;
        public static bool motionStop;
        public Color playerColor;
        public Color enemyColor;

        // 敵我雙方帕拉提提
        BattleUseData m_battleUseData;
        Pratiti Ed;
        Pratiti Pd;

        void Start()
        {
            Player = GameObject.Find("player").transform.GetChild(3).gameObject;
            Enemy = GameObject.Find("enemy").transform.GetChild(3).gameObject;
            playerShield = GameObject.Find("player").transform.GetChild(0).gameObject;
            enemyShield = GameObject.Find("enemy").transform.GetChild(0).gameObject;
            playerParryShield = GameObject.Find("player").transform.GetChild(1).gameObject;
            PlayerAbilityHint = GameObject.Find("player").transform.GetChild(2).gameObject;
            EnemyAbilityHint = GameObject.Find("enemy").transform.GetChild(2).gameObject;
            playerAnimation = Player.GetComponent<Animator>();
            enemyAnimation = Enemy.GetComponent<Animator>();
            PlayerParryShieldAnimator = playerParryShield.GetComponent<Animator>();
            PlayerAbilityHintAnimator = PlayerAbilityHint.transform.GetChild(0).gameObject.GetComponent<Animator>();
            EnemyAbilityHintAnimator = EnemyAbilityHint.transform.GetChild(0).gameObject.GetComponent<Animator>();

            Time.timeScale = 1f;


            if (GameObject.Find("GameData") != null)
            {
                m_battleUseData = GameObject.Find("GameData").GetComponent<BattleUseData>();
                Ed = m_battleUseData.EnemyPratiti;
                Pd = m_battleUseData.PlayerPratiti;
            }
            else
            {
                Ed = null;
                Pd = null;
                Debug.Log("BattleSystem找不到GameData");
            }

            // Cobra新增：讀取帕拉提提資料
            // PratitiBattleData m_EnemyData;
            if (Ed == null || Pd == null)
            {
                player = new pratiti(550, 550, 500, 500, 500, 500, 0, 500, 500, 1, 1, 0, 0, 1.466f, 1.33f, 1.33f, 0.9f, 0.8f, 0, 0, null, null, null, null, null, null, null, false, false, false, false);
                enemy = new pratiti(550, 500, 550, 500, 500, 500, 0, 550, 500, 1, 1, 1, 0, 0.35f, 1.5f, 1.5f, 1.2f, 1.5f, 0, 0, null, null, null, null, null, null, null, false, false, false, false);
            }
            else
            {

                player = new pratiti(Pd.atk, Pd.mAtk, Pd.def, Pd.mDef, Pd.agi, Pd.hp, 0, Pd.mDef, Pd.mDef, 1, 1, 0, 0, 1.466f, 1.33f, 1.33f, 0.9f, 0.8f, 0, 0, null, null, null, null, null, null, null, false, false, false, false);
                enemy = new pratiti(Ed.atk, Ed.mAtk, Ed.def, Ed.mDef, Ed.agi, Ed.hp, 0, Ed.mDef, Ed.mDef, 1, 1, 1, 0, 0.35f, 1.5f, 1.5f, 1.2f, 1.5f, 0, 0, null, null, null, null, null, null, null, false, false, false, false);
            }

            // Y.A你要用時，將上面註解，下面解除註解
            //player = new pratiti(550, 550, 500, 500, 500, 500, 0, 500, 500, 1, 1, 0, 0, 0, 0, 0, 20, null, null, null, null, false, false, false, false);
            //enemy = new pratiti(550, 500, 550, 500, 500, 500, 0, 550, 500, 1, 1, 1, 0, 0, 0, 0, 20, null, null, null, null, false, false, false, false);

            LCD = 0;
            LCDMax = ability01.cd;
            RCD01 = ability04.cd;
            RCD02 = ability05.cd;
            player.hitRecoveryTime = 0;
            player.hitRecoveryTimeMax = 1;
            enemy.hitRecoveryTime = 0;
            enemy.hitRecoveryTimeMax = 1;
            playerMaxHp = player.hp;
            enemyMaxHp = enemy.hp;
            ParryTime = 0.1f;
            ButtonPressTime = 0.3f;
            player.gameObj01 = PlayerPart01;
            player.gameObj02 = PlayerPart02;
            player.gameObj03 = PlayerPart03;
            player.gameObj04 = PlayerPart04;
            player.gameObj05 = PlayerPart05;
            player.gameObj06 = PlayerPart06;
            player.gameObj07 = PlayerPart07;
            enemy.gameObj01 = Enemy;
            enemy.gameObj02 = Enemy;
            enemy.gameObj03 = Enemy;
            enemy.gameObj04 = Enemy;
            enemy.gameObj05 = Enemy;
            enemy.gameObj06 = Enemy;
            enemy.gameObj07 = Enemy;
            playerParryShield.transform.position = new Vector3(-4, playerParryShield.transform.position.y);
            playerColor = Player.GetComponent<SpriteRenderer>().color;
            enemyColor = Enemy.GetComponent<SpriteRenderer>().color;
            isEnd = false;
            motionStop = false;
        }
        void Update()
        {
            if (Input.GetKey(KeyCode.Equals))
            {
                enemy.hp = 5000;
                enemyMaxHp = enemy.hp;
            }
            leftHpBar.transform.localScale = new Vector3(8.5f * (player.hp / playerMaxHp), this.transform.localScale.y);

            leftDefBar.transform.localScale = new Vector3(this.transform.localScale.x, 3.15f * (player.def / player.maxDef));

            leftMdefBar.transform.localScale = new Vector3(this.transform.localScale.x, 3.15f * (player.mDef / player.maxMDef));

            leftEnergyBar.transform.localScale = new Vector3(6.676f * (player.energy / ability02.needEnergy), this.transform.localScale.y);

            rightHpBar.transform.localScale = new Vector3(8.5f * (enemy.hp / enemyMaxHp), this.transform.localScale.y);

            rightDefBar.transform.localScale = new Vector3(this.transform.localScale.x, 3.15f * (enemy.def / enemy.maxDef));

            rightMdefBar.transform.localScale = new Vector3(this.transform.localScale.x, 3.15f * (enemy.mDef / enemy.maxMDef));

            rightEnergyBar.transform.localScale = new Vector3(6.676f * (enemy.energy / ability05.needEnergy), this.transform.localScale.y);
            //leftDefBar.transform.rotation = Quaternion.Euler(0, 0, 177 - 153 * (player.def / player.maxDef));
            //leftMdefBar.transform.rotation = Quaternion.Euler(0, 0, -177 + 151.2f * (player.mDef / player.maxMDef));
            //rightDefBar.transform.rotation = Quaternion.Euler(0, 0, -177.2f + 153.4f * (enemy.def / enemy.maxDef));
            //rightMdefBar.transform.localRotation = Quaternion.Euler(0, 0, -176.8f + 150.9f * (enemy.mDef / enemy.maxMDef));

            CDBar.transform.localScale = new Vector3(this.transform.localScale.x, (LCD / LCDMax), this.transform.localScale.z);

            hitRecoveryBarMask01.transform.localScale = new Vector3(this.transform.localScale.x, (player.hitRecoveryTime / player.hitRecoveryTimeMax), this.transform.localScale.z);
            hitRecoveryBarMask02.transform.localScale = new Vector3(this.transform.localScale.x, (player.hitRecoveryTime / player.hitRecoveryTimeMax), this.transform.localScale.z);
            PlayerAbilityHint.transform.position = new Vector3(Camera.transform.position.x, Camera.transform.position.y);
            EnemyAbilityHint.transform.position = new Vector3(Camera.transform.position.x, Camera.transform.position.y);
        }
        void FixedUpdate()
        {
            if (enemy.hp <= 0)
            {
                print("Win");
                isEnd = true;
                UData.battleResult = BattleResult.win;
                // 切換到獎勵場景
                SceneManager.LoadScene("EndBattle");
                //SceneManager.LoadScene("Win");
            }
            if (player.hp <= 0)
            {
                print("Lose");
                isEnd = true;
                UData.battleResult = BattleResult.lose;
                SceneManager.LoadScene("LoseBattle");
                //SceneManager.LoadScene("Lose");
            }

            if (PlayerAbility01Obj != null)
            {
                PlayerAbility01Obj.transform.position = Enemy.transform.position;
            }
            if (EnemyAbility01Obj != null)
            {
                EnemyAbility01Obj.transform.position = Player.transform.position + new Vector3(0, 4.5f);
            }
            EnemyEmpty.GetComponent<Transform>().position = new Vector3(960 + 107 * Enemy.transform.position.x, 540 + 107 * Enemy.transform.position.y);

            PlayerHealth.GetComponent<Text>().text = (int)player.hp + "/" + (int)playerMaxHp;
            EnemyHealth.GetComponent<Text>().text = (int)enemy.hp + "/" + (int)enemyMaxHp;
            damage.GetComponentInChildren<Text>().text = "-" + Damage.ToString();
            playerAnimation.enabled = true;
            enemyAnimation.enabled = true;
            player.defStatus = defense.defStatus;
            player.hurtTime -= Time.fixedDeltaTime;
            enemy.hurtTime -= Time.fixedDeltaTime;

            if (enemy.energy >= 0.95f * ability05.needEnergy && RCD02 <= 0 && enemy.isHitRecovery == false && Time.timeScale == 1 && player.isHitRecovery == false && control.isCatching == false && motionStop == false)
            {
                print("敵人使用技能");
                enemy.energy = ability05.needEnergy;
                StartCoroutine(NumberChange(ability05.needEnergy, enemy, "costEnergy"));
                enemy.hitRecoveryTime = enemy.abilityStartHitRecoveryTime + enemy.abilityEndHitRecoveryTime;
                enemy.hitRecoveryTimeMax = enemy.hitRecoveryTime;
                enemy.isHitRecovery = true;
                enemyAnimation.SetTrigger("Ability");
                EnemyAbilityHintAnimator.SetTrigger("Slide");
                StartCoroutine(UseStrongAbility(enemy, player, Enemy, Player, ability05));
                EnemyAbilityObj = Instantiate(EnemyAbility, EnemyAbility.transform.position, EnemyAbility.transform.rotation);
            }
            if (RCD01 <= 0 && enemy.isHitRecovery == false && Time.timeScale == 1 && control.isCatching == false && motionStop == false)
            {
                print("敵人使用攻擊");
                enemy.hitRecoveryTime = enemy.attackStartHitRecoveryTime + enemy.attackCompleteHitRecoveryTime;
                enemy.hitRecoveryTimeMax = enemy.hitRecoveryTime;
                enemy.isHitRecovery = true;
                enemyAnimation.SetTrigger("Attack");
            }

            if (player.energy >= ability02.needEnergy)
            {
                CDBar02.SetActive(false);
            }
            else
            {
                CDBar02.SetActive(true);
            }
            if (enemy.isHitRecovery == false && motionStop == false)
            {
                if (enemy.defStatus == 0)
                {
                    //enemy.def -= enemy.maxDef * 0.05f;
                    //enemy.mDef -= enemy.maxMDef * 0.05f;
                }
                if (enemy.defEmpty != 0 || enemy.mDefEmpty != 0)
                {
                    enemy.defStatus = 1;
                }
            }
            else
            {
                enemy.defStatus = 0;
            }

            if (player.isParryReady == true)
            {
                ParryButton.SetActive(true);
                CDBar.SetActive(false);

                if (player.isParry == false && Input.GetKey(KeyCode.LeftArrow) && Time.timeScale == 1 && player.isHitRecovery == false && control.isCatching == false && motionStop == false)
                {
                    print("玩家使用盾反");
                    PlayerParryShieldAnimator.SetTrigger("Start");
                    player.isParry = true;
                    StartCoroutine(ParryTimeCount(player));
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    PressParryButton.SetActive(true);
                    ParryButton.SetActive(false);

                }
                else if (Input.GetKeyUp(KeyCode.LeftArrow))
                {
                    ParryButton.SetActive(true);
                    PressParryButton.SetActive(false);
                }
            }
            else if (player.isParryReady == false)
            {
                ParryButton.SetActive(false);
                PressParryButton.SetActive(false);
                CDBar.SetActive(true);
                if (Input.GetKey(KeyCode.LeftArrow) && LCD <= 0 && Time.timeScale == 1 && player.isHitRecovery == false && control.isCatching == false && motionStop == false)
                {
                    print("玩家使用攻擊");
                    player.hitRecoveryTime = player.attackStartHitRecoveryTime + player.attackCompleteHitRecoveryTime;
                    player.hitRecoveryTimeMax = player.attackStartHitRecoveryTime + player.attackCompleteHitRecoveryTime;
                    player.isHitRecovery = true;
                    playerAnimation.SetTrigger("Attack");
                    PressAttackButton.SetActive(true);
                    AttackButton.SetActive(false);
                    StartCoroutine(AttackButtonPress());
                }
            }

            if (player.energy >= 0.95f * ability02.needEnergy && Input.GetKey(KeyCode.DownArrow) && Time.timeScale == 1 && player.isHitRecovery == false && control.isCatching == false && motionStop == false)
            {
                print("玩家使用技能");
                player.energy = ability02.needEnergy;
                StartCoroutine(NumberChange(ability02.needEnergy, player, "costEnergy"));
                player.hitRecoveryTime = player.abilityStartHitRecoveryTime + player.abilityEndHitRecoveryTime;
                player.hitRecoveryTimeMax = player.abilityStartHitRecoveryTime + player.abilityEndHitRecoveryTime;
                player.isHitRecovery = true;
                playerAnimation.SetTrigger("Ability");
                PlayerAbilityHintAnimator.SetTrigger("Slide");
                StartCoroutine(AbilityButtonPress());
                Invoke("playerAbility015", 0.9f - 0.25f);
                StartCoroutine(UseStrongAbility(player, enemy, Player, Enemy, ability02));
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                if (Time.timeScale == 1 && control.isCatching == false && motionStop == false)
                {
                    //print("玩家開始防禦");
                    if (player.def >= 0.1f * player.maxDef && player.mDef >= 0.1f * player.maxMDef)
                    {
                        //print("玩家準備盾反");
                        player.isParryReady = true;
                    }
                    else
                    {
                        player.isParryReady = false;
                    }
                    if (player.isHitRecovery == false)
                    {
                        if (player.defEmpty == 1 || player.mDefEmpty == 1)
                        {
                            player.defStatus = 1;
                        }
                        PressDefense.SetActive(true);
                        Defense.SetActive(false);
                    }
                }
            }
            else
            {
                player.defStatus = 0;
                player.isParryReady = false;
                Defense.SetActive(true);
                PressDefense.SetActive(false);
            }

            if (player.hurtTime <= 0)
            {
                Player.GetComponent<SpriteRenderer>().color = playerColor;
                PlayerPart01.GetComponent<SpriteRenderer>().color = playerColor;
                PlayerPart02.GetComponent<SpriteRenderer>().color = playerColor;
                PlayerPart03.GetComponent<SpriteRenderer>().color = playerColor;
                PlayerPart04.GetComponent<SpriteRenderer>().color = playerColor;
                PlayerPart05.GetComponent<SpriteRenderer>().color = playerColor;
                PlayerPart06.GetComponent<SpriteRenderer>().color = playerColor;
                PlayerPart07.GetComponent<SpriteRenderer>().color = playerColor;
            }
            if (enemy.hurtTime <= 0)
            {
                Enemy.GetComponent<SpriteRenderer>().color = enemyColor;
            }
            if (player.isHitRecovery == true)
            {
                player.defStatus = 0;
                playerShield.SetActive(false);
                Defense.SetActive(true);
                PressDefense.SetActive(false);
            }

            player.hitRecoveryTime -= Time.fixedDeltaTime * Time.timeScale;
            enemy.hitRecoveryTime -= Time.fixedDeltaTime * Time.timeScale;
            LCD -= Time.fixedDeltaTime * Time.timeScale;
            RCD01 -= Time.fixedDeltaTime * Time.timeScale;
            RCD02 -= Time.fixedDeltaTime * Time.timeScale;

            if (player.defStatus == 1 && motionStop == false)
            {
                if (player.def > 0 || player.mDef > 0)
                {
                    player.def -= 0.05f * player.maxDef * Time.fixedDeltaTime;
                    player.mDef -= 0.05f * player.maxMDef * Time.fixedDeltaTime;
                    if (PlayerParryShieldAnimator.GetCurrentAnimatorStateInfo(0).IsName("Parry Shield idle"))
                    {
                        playerShield.SetActive(true);
                    }
                }
            }
            else
            {
                playerShield.SetActive(false);
            }

            if (enemy.defStatus == 1 && motionStop == false)
            {
                enemyShield.SetActive(true);
                enemy.def -= 0.05f * enemy.maxDef * Time.fixedDeltaTime;
                enemy.mDef -= 0.05f * enemy.maxDef * Time.fixedDeltaTime;
            }
            else
            {
                enemyShield.SetActive(false);
            }
            if (player.def <= 0)
            {
                player.def = 0;
                player.defEmpty = 0;
            }
            if (enemy.def <= 0)
            {
                enemy.def = 0;
                enemy.defEmpty = 0;
            }
            if (player.mDef <= 0)
            {
                player.mDef = 0;
                player.mDefEmpty = 0;
            }
            if (enemy.mDef <= 0)
            {
                enemy.mDef = 0;
                enemy.mDefEmpty = 0;
            }
            if (player.hitRecoveryTime <= 0 && control.DecideingCatch == false)
            {
                player.isHitRecovery = false;
                player.hitRecoveryTime = 0;
            }
            if (enemy.hitRecoveryTime <= 0 && control.DecideingCatch == false)
            {
                enemy.isHitRecovery = false;
                enemy.hitRecoveryTime = 0;
            }
        }
        public void button01()
        {
            if (LCD <= 0 && Time.timeScale == 1 && player.isHitRecovery == false && control.isCatching == false && motionStop == false)
            {
                player.hitRecoveryTime = player.attackStartHitRecoveryTime + player.attackCompleteHitRecoveryTime;
                player.hitRecoveryTimeMax = player.attackStartHitRecoveryTime + player.attackCompleteHitRecoveryTime;
                player.isHitRecovery = true;
                playerAnimation.SetInteger("Status", 1);
            }
        }
        public void button02()
        {
            if (player.energy >= ability02.needEnergy && Time.timeScale == 1 && player.isHitRecovery == false && control.isCatching == false && motionStop == false)
            {
                player.hitRecoveryTime = player.abilityStartHitRecoveryTime + player.abilityEndHitRecoveryTime;
                player.hitRecoveryTimeMax = player.abilityStartHitRecoveryTime + player.abilityEndHitRecoveryTime;
                player.isHitRecovery = true;
                playerAnimation.SetInteger("Status", 2);
                Invoke("playerAbility015", 0.9f - 0.1f);
                Invoke("playerAbility02", 0.9f);
            }
        }
        public void attack(pratiti atker, pratiti defer, ability ability)
        {
            StartCoroutine(CameraShake());
            if (defer.def < 0)
            {
                defer.def = 0;
            }
            if (defer.mDef < 0)
            {
                defer.mDef = 0;
            }
            chance = UnityEngine.Random.Range(1, 101);

            if ((chance + 0.1f * (defer.agi - atker.agi)) <= ability.hitRate)
            {
                if (defer.isParry == true)
                {
                    print("盾反成功");
                    defer.isParry = false;
                    StartCoroutine(ParryTimeSlowDown());
                    defer.ParrySuccess = true;
                    PlayerParryShieldAnimator.SetTrigger("Success");

                    Damage = 0.0005f * ability.power * 0.1f * (atker.atk + atker.mAtk);
                    Damage = (float)Math.Ceiling((double)Damage);
                    StartCoroutine(NumberChange(Damage, defer, "hp"));
                    StartCoroutine(NumberChange(ability.gainEnergy, atker, "gainEnergy"));

                    damage.GetComponentInChildren<Text>().text = "-" + Damage.ToString();
                    random01 = UnityEngine.Random.Range(-3, 5);
                    random02 = UnityEngine.Random.Range(3, 6);
                    Instantiate(damage, defer.gameObj01.transform.position + new Vector3(random01 * 0.2f, random02 * 0.2f), new Quaternion(0, 0, 0, 0));
                    defer.gameObj01.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0);
                    defer.gameObj02.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0);
                    defer.gameObj03.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0);
                    defer.gameObj04.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0);
                    defer.gameObj05.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0);
                    defer.gameObj06.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0);
                    defer.gameObj07.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0);
                    defer.hurtTime = 0.25f;
                    if (ability.gainEnergy > 0)
                    {
                        if (defer.gameObj01 == Player)
                        {
                            EnemyAbility01Obj = Instantiate(EnemyAbility01, defer.gameObj01.transform.position + new Vector3(0, 4.5f), new Quaternion(0, 0, 0, 0));
                        }
                        else
                        {

                            PlayerAbility01Obj = Instantiate(PlayerAbility01, defer.gameObj01.transform.position, new Quaternion(0, 0, 0, 0));
                        }
                    }
                    Damage = 0.0005f * ability.power * (atker.atk + atker.mAtk);
                    Damage = (float)Math.Ceiling((double)Damage);
                    StartCoroutine(NumberChange(Damage, atker, "hp"));

                    damage.GetComponentInChildren<Text>().text = "-" + Damage.ToString();
                    random01 = UnityEngine.Random.Range(-3, 5);
                    random02 = UnityEngine.Random.Range(3, 6);
                    Instantiate(damage, atker.gameObj01.transform.position + new Vector3(random01 * 0.2f, random02 * 0.2f), new Quaternion(0, 0, 0, 0));
                    //Instantiate(damage, new Vector3(-3.1f,3.1f), new Quaternion(0, 0, 0, 0));
                    //Instantiate(damage, new Vector3(3.1f, 2.9f), new Quaternion(0, 0, 0, 0));
                    atker.gameObj01.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0);
                    atker.gameObj02.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0);
                    atker.gameObj03.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0);
                    atker.gameObj04.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0);
                    defer.gameObj05.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0);
                    defer.gameObj06.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0);
                    defer.gameObj07.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0);
                    atker.hurtTime = 0.25f;
                }
                else
                {
                    //傷害值計算                                                                  //90%是滿減傷
                    Damage = 0.0005f * ability.power * (atker.atk + atker.mAtk - defer.defStatus * 0.9f *
                             (atker.atk * defer.def / defer.maxDef + atker.mAtk * defer.mDef / defer.maxMDef));
                    Damage = (float)Math.Ceiling((double)Damage);

                    //呼叫各數值遞減的協程函式
                    StartCoroutine(NumberChange(Damage, defer, "hp"));
                    StartCoroutine(NumberChange(0.0005f * ability.power * atker.atk * defer.defStatus * defer.defEmpty, defer, "def"));
                    StartCoroutine(NumberChange(0.0005f * ability.power * atker.mAtk * defer.defStatus * defer.mDefEmpty, defer, "mDef"));
                    StartCoroutine(NumberChange(ability.gainEnergy, atker, "gainEnergy"));

                    //產生顯示傷害數值位置的偏移亂數
                    random01 = UnityEngine.Random.Range(-3, 5);
                    random02 = UnityEngine.Random.Range(3, 6);
                    damage.GetComponentInChildren<Text>().text = "-" + Damage.ToString(); //將傷害數值文字化
                    //在畫面上產生傷害數值
                    Instantiate(damage, defer.gameObj01.transform.position + new Vector3(random01 * 0.2f, random02 * 0.2f), new Quaternion(0, 0, 0, 0));

                    //受傷害角色身體顏色轉為紅色
                    defer.gameObj01.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0);
                    defer.gameObj02.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0);
                    defer.gameObj03.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0);
                    defer.gameObj04.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0);
                    defer.gameObj05.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0);
                    defer.gameObj06.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0);
                    defer.gameObj07.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0);
                    defer.hurtTime = 0.25f;
                    if (ability.gainEnergy > 0)
                    {
                        if (defer.gameObj01 == Player)
                        {
                            EnemyAbility01Obj = Instantiate(EnemyAbility01, defer.gameObj01.transform.position + new Vector3(0, 4.5f), new Quaternion(0, 0, 0, 0));
                        }
                        else
                        {

                            PlayerAbility01Obj = Instantiate(PlayerAbility01, defer.gameObj01.transform.position, new Quaternion(0, 0, 0, 0));
                        }
                    }
                }
            }
            else
            {
                if (defer.isParry == true)
                {
                    defer.ParrySuccess = true;
                }
                random01 = UnityEngine.Random.Range(-4, 5);
                random02 = UnityEngine.Random.Range(1, 6);
                Instantiate(miss, defer.gameObj01.transform.position + new Vector3(random01 * 0.3f, random02 * 0.3f), new Quaternion(0, 0, 0, 0));
            }
        }
        public void playerAbility015()
        {
            PlayerAbilityObj = Instantiate(PlayerAbility, PlayerAbility.transform.position, PlayerAbility.transform.rotation);
        }
        IEnumerator UseStrongAbility(pratiti Atker, pratiti Defer, GameObject AtkerObj, GameObject DeferObj, ability AtkerAbility)
        {
            yield return new WaitForSeconds(Atker.abilityStartHitRecoveryTime);
            while (AtkerAbility.hitTimes < AtkerAbility.totalHitTimes)
            {
                AtkerAbility.hitTimes += 1;
                if (AtkerAbility.leftSide <= DeferObj.transform.position.x && DeferObj.transform.position.x < AtkerAbility.rightSide)
                {
                    attack(Atker, Defer, AtkerAbility);
                }
                yield return new WaitForSeconds(0.2f);
            }
            AtkerAbility.hitTimes = 0;
        }
        IEnumerator AttackButtonPress()
        {
            PressAttackButton.SetActive(true);
            AttackButton.SetActive(false);
            yield return new WaitForSecondsRealtime(ButtonPressTime);
            AttackButton.SetActive(true);
            PressAttackButton.SetActive(false);
        }
        IEnumerator AbilityButtonPress()
        {
            PressAbilityButton.SetActive(true);
            AbilityButton.SetActive(false);
            yield return new WaitForSecondsRealtime(ButtonPressTime);
            AbilityButton.SetActive(true);
            PressAbilityButton.SetActive(false);
        }

        IEnumerator ParryTimeCount(pratiti parryer)
        {
            parryer.hitRecoveryTime = 0.5f;
            parryer.hitRecoveryTimeMax = 0.5f;
            parryer.isHitRecovery = true;
            yield return new WaitForSeconds(ParryTime);
            parryer.isParry = false;
            if (parryer.ParrySuccess == true)
            {
                parryer.ParrySuccess = false;
            }
            else
            {
                print("盾反失敗");
                StartCoroutine(NumberChange(parryer.maxDef * 0.1f, parryer, "def"));
                StartCoroutine(NumberChange(parryer.maxMDef * 0.1f, parryer, "mDef"));
            }
        }
        IEnumerator ParryTimeSlowDown()
        {
            Time.timeScale = 0.25f;
            yield return new WaitForSecondsRealtime(0.5f);
            if (MENU.activeSelf != true)
            {
                Time.timeScale = 1f;
            }
        }
        IEnumerator NumberChange(float Number, pratiti Pratiti, string DataType)
        {
            while (Number >= 1)
            {
                if (Time.timeScale != 0)
                {
                    switch (DataType)
                    {
                        case "hp":
                            if (Number < 10)
                            {
                                Pratiti.hp -= (Number * 0.5f);
                                Number -= (Number * 0.5f);
                            }
                            else
                            {
                                Pratiti.hp -= (Number * 0.1f);
                                Number -= (Number * 0.1f);
                            }
                            break;
                        case "def":
                            Pratiti.def -= (Number * 0.1f);
                            Number *= 0.9f;
                            break;
                        case "mDef":
                            Pratiti.mDef -= (Number * 0.1f);
                            Number *= 0.9f;
                            break;
                        case "gainEnergy":
                            Pratiti.energy += (Number * 0.12f);
                            Number *= 0.9f;
                            break;
                        case "costEnergy":
                            Pratiti.energy -= (Number * 0.11f);
                            Number *= 0.9f;
                            if (Pratiti.energy < 0)
                            {
                                Pratiti.energy = 0;
                            }
                            break;
                    }
                }
                yield return null;
            }
            switch (DataType)
            {
                case "hp":
                    Pratiti.hp = (int)Pratiti.hp;
                    break;
                case "def":
                    Pratiti.def = (int)Pratiti.def;
                    break;
                case "mDef":
                    Pratiti.mDef = (int)Pratiti.mDef;
                    break;
                case "energy":
                    Pratiti.energy = (int)Pratiti.energy;
                    if (Pratiti.energy < 0)
                    {
                        Pratiti.energy = 0;
                    }
                    break;
            }
        }
        IEnumerator CameraShake()
        {
            int times = 1;
            while (times <= 10)
            {
                float x = UnityEngine.Random.Range(0, 0.3f);
                float y = UnityEngine.Random.Range(0, 0.15f);
                Camera.transform.position = new Vector3(x, y, Camera.transform.position.z);
                //PlayerAbilityHint.transform.position = new Vector3(x, y);
                //EnemyAbilityHint.transform.position = new Vector3(x, y);
                times += 1;
                yield return new WaitForFixedUpdate();
                yield return new WaitForFixedUpdate();
                yield return new WaitForFixedUpdate();
                yield return new WaitForFixedUpdate();
            }
            Camera.transform.position = new Vector3(0, 0, Camera.transform.position.z);
            //PlayerAbilityHint.transform.position = new Vector3(0, 0, 0);
            //EnemyAbilityHint.transform.position = new Vector3(0, 0, 0);
            print("抖動結束");
        }
    }
}