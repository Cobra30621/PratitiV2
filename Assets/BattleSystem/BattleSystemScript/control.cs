using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.EventSystems;
namespace NameBattleSystem
{
    public class control : MonoBehaviour
    {
        public GameObject MENU;
        public GameObject DessertMenu;
        public UnityEvent Exit;
        public UnityEvent Continue;
        public UnityEvent Empty;
        public UnityEvent Pause;
        public UnityEvent Play;
        public UnityEvent DessertSelect;
        public static GameObject DessertClone;
        public GameObject Dessert01;
        public GameObject Dessert02;
        public GameObject Dessert03;
        public GameObject Dessert04;
        public GameObject Dessert05;
        public GameObject Dessert06;
        public static bool isCatching;
        public static bool DecideingCatch;
        public static bool isSelect;
        void Start()
        {
            isCatching = false;
            DecideingCatch = false;
        }
        void Update()
        {
            if (EventSystem.current.currentSelectedGameObject == null)
            {
                if(Input.GetKey(KeyCode.UpArrow))
                {
                    this.Continue.Invoke();
                }
                else if (Input.GetKey(KeyCode.DownArrow))
                {
                    this.Exit.Invoke();
                }
            }
            if (Time.timeScale == 0)
            {
                this.Pause.Invoke();
            }
            else
            {
                this.Play.Invoke();
            }
            if (BattleSystem.isEnd)
            {
                if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.X) || Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.Escape))
                {
                    SceneManager.LoadScene("Battle");
                }
            }
            if (Input.GetKeyDown(KeyCode.Escape) && MENU.activeSelf == false && DessertMenu.activeSelf == false && DecideingCatch == false)
            {
                Time.timeScale = 0;
                MENU.SetActive(true);
                this.Continue.Invoke();
            }
            else if (Input.GetKeyDown(KeyCode.Escape) && MENU.activeSelf == true)
            {
                Time.timeScale = 1;
                MENU.SetActive(false);
            }
            if (DessertMenu.activeSelf == true) 
            {
                if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.X))
                {
                    Time.timeScale = 1;
                    DessertMenu.SetActive(false);
                }
            }
            if (Input.GetKeyDown(KeyCode.X) && MENU.activeSelf == true)
            {
                Time.timeScale = 1;
                MENU.SetActive(false);
            }
            if (MENU.activeSelf == true || DessertMenu.activeSelf == true)
            {
                /*if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    this.Restart.Invoke();
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    this.Cancel.Invoke();
                }*/
            }
            else
            {
                this.Empty.Invoke();
            }
        }
        public void cancel()
        {
            Time.timeScale = 1;
            MENU.SetActive(false);
        }
        public void pause()
        {
            if (MENU.activeSelf == false && DecideingCatch == false)
            {
                Time.timeScale = 0;
                MENU.SetActive(true);
                this.Continue.Invoke();
            }
            else if (MENU.activeSelf == true)
            {
                Time.timeScale = 1;
                MENU.SetActive(false);
            }
        }
        public void restart()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("Battle");
        }
        public void dessert()
        {
            if (isCatching == false && BattleSystem.player.isHitRecovery == false)
            {
                print("Choose Dessert");
                MENU.SetActive(false);
                DessertMenu.SetActive(true);
                this.DessertSelect.Invoke();
            }
        }
        public void SelectDessert01()
        {
            isCatching = true;
            BattleSystem.player.isHitRecovery = true;
            DessertMenu.SetActive(false);
            Time.timeScale = 1;
            DessertClone = Instantiate(Dessert01, new Vector3(0, -353), new Quaternion(0, 0, 0, 0));
        }
        public void SelectDessert02()
        {
            isCatching = true;
            BattleSystem.player.isHitRecovery = true;
            DessertMenu.SetActive(false);
            Time.timeScale = 1;
            DessertClone = Instantiate(Dessert02, new Vector3(0, -353), new Quaternion(0, 0, 0, 0));
        }
        public void SelectDessert03()
        {
            isCatching = true;
            BattleSystem.player.isHitRecovery = true;
            DessertMenu.SetActive(false);
            Time.timeScale = 1;
            DessertClone = Instantiate(Dessert03, new Vector3(0, -353), new Quaternion(0, 0, 0, 0));
        }
        public void SelectDessert04()
        {
            isCatching = true;
            BattleSystem.player.isHitRecovery = true;
            DessertMenu.SetActive(false);
            Time.timeScale = 1;
            DessertClone = Instantiate(Dessert04, new Vector3(0, -353), new Quaternion(0, 0, 0, 0));
        }
        public void SelectDessert05()
        {
            isCatching = true;
            BattleSystem.player.isHitRecovery = true;
            DessertMenu.SetActive(false);
            Time.timeScale = 1;
            DessertClone = Instantiate(Dessert05, new Vector3(0, -353), new Quaternion(0, 0, 0, 0));
        }
        public void SelectDessert06()
        {
            isCatching = true;
            BattleSystem.player.isHitRecovery = true;
            DessertMenu.SetActive(false);
            Time.timeScale = 1;
            DessertClone = Instantiate(Dessert06, new Vector3(0, -353), new Quaternion(0, 0, 0, 0));
        }
    }
}
