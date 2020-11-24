using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCamera : MonoBehaviour
{
    public GameObject[] Cameras;
    // Start is called before the first frame update
    public void ChangeCam(string Map)
    {
        foreach (GameObject Camera in Cameras)
        {
            Camera.SetActive(false);
            if (Camera.name == Map)
            {
                Camera.SetActive(true);
            }
        }
            
    }
}
