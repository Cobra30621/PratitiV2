using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
public class MainGameEmptyButton : MonoBehaviour, IPointerExitHandler
{
    public GameObject BreedButton;
    public GameObject BagButton;
    public GameObject ManualButton;
    public bool isWait = false;
    public void OnPointerExit(PointerEventData eventData)
    {
        print(000);
        if (isWait == false)
        {
            StartCoroutine(Wait());
        }
    }
    IEnumerator Wait()
    {
        isWait = true;
        yield return new WaitForSeconds(1.5f);
        print(111);
        BreedButton.SetActive(false);
        BagButton.SetActive(false);
        ManualButton.SetActive(false);
        isWait = false;
        StopCoroutine("Wait");
    }
}

