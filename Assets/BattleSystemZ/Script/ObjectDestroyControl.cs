using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroyControl : MonoBehaviour
{
    [SerializeField]
    float lifeTime;

    void Start()
    {
        StartCoroutine(DestroyCountDown());
    }

    IEnumerator DestroyCountDown()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(this.gameObject);
    }
}
