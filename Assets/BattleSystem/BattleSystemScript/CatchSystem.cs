using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NameBattleSystem;
using UnityEngine.UI;
public class CatchSystem : MonoBehaviour
{
    public GameObject GameObject;
    public Image Image;
    public static bool isTop;
    public float distanceX;
    public float distanceY;
    void Start()
    {
        BattleSystem.player.isHitRecovery = true;
        GameObject = this.gameObject;
        Image = this.GetComponent<Image>();
        isTop = false;
        GameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 90), ForceMode2D.Impulse);
    }

    void FixedUpdate()
    {
        /*print(Image.rectTransform.position.x);
        print(107f * (GameObject.Find("Enemy").GetComponent<Transform>().position.x));
        /*Chocolate.rectTransform.position = 107f*(GameObject.Find("Enemy").GetComponent<Transform>().position) + new Vector3(950,550);
        Egg.rectTransform.position = 107f * (GameObject.Find("Enemy").GetComponent<Transform>().position) + new Vector3(950, 550);
        Blueberry.rectTransform.position = 107f * (GameObject.Find("Enemy").GetComponent<Transform>().position) + new Vector3(950, 550);
        Milk.rectTransform.position = 107f * (GameObject.Find("Enemy").GetComponent<Transform>().position) + new Vector3(950, 550);
        */
        if (Image.rectTransform.position.x <= 1200)
        {
            Image.rectTransform.position += new Vector3(4f, 0, 0);
            Image.rectTransform.sizeDelta -= new Vector2(4, 4);
        }
        if (Image.rectTransform.position.y >= 700 && isTop == false)
        {
            isTop = true;
        }
        if (isTop == true && Image.rectTransform.position.y <= 390)
        {
            GameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
        }
    }
}

