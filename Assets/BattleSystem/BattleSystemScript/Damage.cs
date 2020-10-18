using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NameBattleSystem;

public class Damage : MonoBehaviour
{
    public Rigidbody2D rig2d;
    public CanvasGroup canvas;
    public float timer;
    public float t = 1;
    public float T = 0.5f;
    void Start()
    {
        timer = 2;
        rig2d.AddForce(new Vector2(0, 6),ForceMode2D.Impulse);
    }

    void FixedUpdate()
    {
        if (timer <= 0)
        {
            Destroy(gameObject);
        }
        timer -= Time.fixedDeltaTime;
        T -= Time.fixedDeltaTime;
        if (T <= 0)
        {
            t -= 3.5f * Time.fixedDeltaTime;
            canvas.alpha = t;
        }
    }
}
