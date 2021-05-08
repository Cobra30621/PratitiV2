
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TeachingUI: MonoBehaviour
{
    private static TeachingUI instance;

    // UI
    [SerializeField] private GameObject _panel;

    [SerializeField] private Text lab_info;


    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Start()
    {
        instance = this;
        Initialize();
    }

    public void Initialize(){
    }   

    public static void Open(string info){
        instance.Show(info);
    }

    public void Show(string info){
        SetInfo(info);
        _panel.SetActive(true);
        // if (_panel.activeSelf)
        // {
        //     _panel.SetActive(false);
        // }
        // else if (!_panel.activeSelf)
        // {
        //     _panel.SetActive(true);
        // }
    }

    public void SetInfo(string info){
        lab_info.text = info;
    }   

    public static void Close(){
        instance.Close2();
    }

    public void Close2(){
        _panel.SetActive(false);
    }

    
}