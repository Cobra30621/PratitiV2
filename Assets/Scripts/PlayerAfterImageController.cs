using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAfterImageController : MonoBehaviour
{
    [SerializeField]
    private float activeTime = 0.15f;
    private float timeActivated;
    private float alpha;
    [SerializeField]
    private float alphaSet = 0.4f;
    [SerializeField]
    private float alphaDecay = 0.6f;

    public GameObject player;

    private Transform playerTmpTF;
    private Transform tmpTF;

    private SpriteRenderer playerTmpSR;
    private SpriteRenderer tmpSR;

    private Color color;

    private void OnEnable()
    {
        this.transform.position = player.transform.position;

        for(int i = 0; i < 6; i++)
        {
            playerTmpTF = player.transform.GetChild(i).transform;
            tmpTF = this.transform.GetChild(0).transform.GetChild(i);

            tmpTF.localScale = playerTmpTF.localScale;
            tmpTF.position = playerTmpTF.position;
            tmpTF.rotation = playerTmpTF.rotation;

            playerTmpSR = playerTmpTF.GetComponent<SpriteRenderer>();
            tmpSR = tmpTF.GetComponent<SpriteRenderer>();

            tmpSR = playerTmpSR;
        }

        alpha = alphaSet;
        timeActivated = Time.time;
    }

    private void Update()
    {
        alpha -= alphaDecay * Time.deltaTime;
        color = new Color(1f, 1f, 1f, alpha);

        for(int i = 0; i < 6; i++)
        {
            tmpSR = this.transform.GetChild(0).transform.GetChild(i).GetComponent<SpriteRenderer>();
            tmpSR.color = color;
        }

        if(Time.time >= (timeActivated + activeTime))
        {
            PlayerAfterImagePool.Instance.AddToPool(gameObject);
        }

    }
}
