using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainGameSystemControl : MonoBehaviour
{
    public GameObject MainButtons;
    public GameObject TrainingSystem;
    public GameObject MultiplicationSystem;
    public GameObject InventorySystem;
    public GameObject Player;
    void Update()
    {
        if (TrainingSystem.activeSelf == true && Input.GetKeyDown(KeyCode.Escape))
        {
            TrainingSystem.SetActive(false);
            MainButtons.SetActive(true);
        }
        else if (MultiplicationSystem.activeSelf == true && Input.GetKeyDown(KeyCode.Escape))
        {
            MultiplicationSystem.SetActive(false);
            MainButtons.SetActive(true);
        }
        else if (InventorySystem.activeSelf == true && Input.GetKeyDown(KeyCode.Escape))
        {
            InventorySystem.SetActive(false);
            MainButtons.SetActive(true);
        }
    }
    public void EnterTrainingSystem()
    {
        TrainingSystem.SetActive(true);
        MainButtons.SetActive(false);
    }
    public void EnterMultiplicationSystem()
    {
        MultiplicationSystem.SetActive(true);
        MainButtons.SetActive(false);
    }
    public void EnterInventorySystem()
    {
        InventorySystem.SetActive(true);
        MainButtons.SetActive(false);
    }
}
