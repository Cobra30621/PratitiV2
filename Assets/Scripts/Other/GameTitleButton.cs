using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameTitleButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject Title;
    public GameObject start;
    public GameObject Setting;
    public GameObject Exit;
    private bool isGamesStart = false;
    public Image BackGround;
    public AudioSource Audio;
    // GameManager gameManager;
    // StoryManager storyManager;
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (this.gameObject.name == "Start" && isGamesStart == false) 
        {
            start.SetActive(true);
        }
        else if (this.gameObject.name == "Setting")
        {
            Setting.SetActive(true);
        }
        else if (this.gameObject.name == "Exit")
        {
            Exit.SetActive(true);
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (this.gameObject.name == "Start" && isGamesStart == false) 
        {
            start.SetActive(false);
        }
        else if (this.gameObject.name == "Setting")
        {
            Setting.SetActive(false);
        }
        else if (this.gameObject.name == "Exit")
        {
            Exit.SetActive(false);
        }
    }
    public void GAMESTART()
    {
        // 初始化遊戲數值
        Debug.Log("之後會補充初始化設定");
        /*gameManager = FindObjectOfType<GameManager>();
        gameManager.gamedata.PlaxyerPos = new Vector3(-16, 27, 0);
        gameManager.gamedata.Map = "Map";
        gameManager.gamedata.smallMap = "Map1";

        // 初始化故事bool
        storyManager = FindObjectOfType<StoryManager>();
        storyManager.InitialStoryBoolTrue();
        */
        isGamesStart = true;
        StartCoroutine(FadeEffect());
    }
    IEnumerator FadeEffect()
    {
        while(BackGround.color.a < 1)
        {
            BackGround.color = new Color(BackGround.color.r, BackGround.color.g, BackGround.color.b, BackGround.color.a + Time.deltaTime);
            Audio.volume -= Time.deltaTime;
            yield return null;
        }
        Audio.Stop();
        SceneManager.LoadScene("Map");
    }

}
