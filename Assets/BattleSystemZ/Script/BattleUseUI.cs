using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUseUI : MonoBehaviour
{
    [SerializeField] private GameObject panel_tutorial;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.T))
        {
            ShowTutorial();
        }
    }

    public void ShowTutorial(){
        
        if (panel_tutorial.activeSelf)
        {
            panel_tutorial.SetActive(false);
        }
        else if (!panel_tutorial.activeSelf)
        {
            panel_tutorial.SetActive(true);
        }
    }
}
