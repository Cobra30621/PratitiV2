using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Fungus;

public class Portal : MonoBehaviour {
	public GameObject NowMap ;
    public GameObject ChangeMap;
    public bool fadeScreen ;
    public static Flowchart FungusMap;



    void OnTriggerEnter2D (Collider2D co) {
		if(co.tag != "Player") return ;

        if (fadeScreen)
        {

            FungusMap = GameObject.Find("FungusMap").GetComponent<Flowchart>();
            SystemController.Instance.PlayBlock(FungusMap, "FadeOut");
            StartCoroutine(WaitChange());
            //SystemController.Instance.PlayBlock(FungusMap, "FadeIn");

        }
        else
        {
            Change();
        }
        

    }


    private IEnumerator WaitChange()
    {
        yield return new WaitForSeconds(1);
        Change();
    }


    private void Change()
    {
        NowMap.SetActive(false);
        ChangeMap.SetActive(true);
        UData.smallMap = ChangeMap.name;
    }


}
