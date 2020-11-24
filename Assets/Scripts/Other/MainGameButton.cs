using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class MainGameButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject QuestButton;
    public GameObject BreedButton;//todo
    public GameObject InventoryButton;
    public GameObject ManualButton;
    public bool isWait = false;
    public bool isActivated = false;
    public void OnPointerEnter(PointerEventData eventData)
    {
        isActivated = true;
        BreedButton.SetActive(true);
        InventoryButton.SetActive(true);
        ManualButton.SetActive(true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        isActivated = false;
        if (isWait == false) 
        {
            StartCoroutine(Wait());
        }
    }
    IEnumerator Wait()
    {
        isWait = true;
        yield return new WaitForSeconds(1.5f);
        if (isActivated == false)
        {
            BreedButton.SetActive(false);
            InventoryButton.SetActive(false);
            ManualButton.SetActive(false);
        }
        isWait = false;
        StopCoroutine("Wait");
    }
}

