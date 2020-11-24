using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PratitiCloudControl : MonoBehaviour
{
    public GameObject CloudBody;
    public GameObject CloudLittleBodyBomb;
    public GameObject CloudLittleBodyBombObj;

    void Update()
    {
        print(CloudBody.transform.position.x);
        if (CloudBody.transform.position.x > 3.8f && CloudBody.transform.position.x < 4 && CloudLittleBodyBombObj == null) 
        {
            CloudLittleBodyBombObj = Instantiate(CloudLittleBodyBomb, CloudBody.transform.position, CloudBody.transform.rotation);
        }
    }

}
