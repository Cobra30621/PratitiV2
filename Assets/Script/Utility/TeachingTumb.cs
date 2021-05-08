using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeachingTumb : MonoBehaviour
{
    [SerializeField] private GameObject ObTumb;
    public TumbType tumbType;


    public void ShowTumb(){
        ObTumb.SetActive(true);
    }

    public void CloseTumb(){
        ObTumb.SetActive(false);
    }

    public TumbType GetTumbType(){
        return tumbType;
    }
}
