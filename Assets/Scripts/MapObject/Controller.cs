using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour
{
    [Header("玩家移動數值")]
    [Range(0f,1000f)]
	public float speed = 140f;
	public float AirSpped = 1f;
	public float BackSpped = 1f;
	[Range(0f, 100f)]
    public float jumpHeight = 10f;
    [Header("玩家物件")]
    public GameObject buttonObject;
	internal GameObject btn;
    public GameObject sprite;
    public bool hasbtn;

    internal bool isGround = false;
	internal Rigidbody2D body;
	internal Animator anim;
	internal AnimatorStateInfo state;
	// internal bool lockMove;

	public void Awake()
	{
        hasbtn = false;

        // 忽略碰撞，Player(9)與NPC(8)
        Physics2D.IgnoreLayerCollision(8, 9);
        // 忽略碰撞，NPC與NPC(9)
        Physics2D.IgnoreLayerCollision(9, 9);
        // 取得各類元
        body = GetComponent<Rigidbody2D>();
		anim = sprite.GetComponentInChildren<Animator>();
	}

    public void FixedUpdate()
    {
        Control();
        StateMachine();

    }

	public virtual void Control()
	{
        // bool upKey = Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow);
        if (UData.IsTalking)
        {
            Move(0);
        }
		if (Input.GetKey(KeyCode.RightArrow) && !UData.IsTalking)
		{
			Move(1);
			Direction(1);
		}
		if (Input.GetKey(KeyCode.LeftArrow)&& !UData.IsTalking)
        {
			Move(-1);
			Direction(0);
		}
        if (Input.GetKey(KeyCode.Space)&& !UData.IsTalking)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Blend Tree"))
            {
                anim.SetBool("Jump", true);
                Jump();
            }
            else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Jump finish") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.3f)
            {
                anim.SetBool("Jump", true);
                Jump();
            }
		}
        if (body.velocity.y < 0)
        {
            anim.SetBool("Jump", false);
        }
		if (Input.GetKeyUp(KeyCode.RightArrow))
		{
			Move(0);
		}
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            Move(0);
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 10000;
			jumpHeight = 30;
        }
        else
        {
			speed = 1000;
			jumpHeight = 20;
		}
    }

	//=======
	// 行動
	//=======
	public virtual void Jump()
	{
		if (!isGround) 
        {
            return; 
        }
		body.velocity = new Vector2(body.velocity.x, jumpHeight);
    }

	public virtual void Move(int i)
	{
		body.velocity = new Vector2(BackSpped * i * speed * Time.deltaTime, body.velocity.y);

		anim.SetFloat("Move", Mathf.Abs(i + 0f));
	}

	public virtual void Direction(int i)
	{
		if (isGround)
		{
			transform.eulerAngles = new Vector3(0, 180 * i, 0);
		}
		else
		{
			if (transform.eulerAngles != new Vector3(0, 180 * i, 0))
			{
				BackSpped = 0.65f;
			}
			else
			{
				BackSpped = 1f;
			}
        }
	}

	//==========
	// 動畫
	//==========
	public void StateMachine()
	{
		anim.SetBool("Ground", isGround);
		anim.SetFloat("Y", body.velocity.y);
		//anim.SetFloat("Ammo", Game.sav.HasAmmo() ? 1 : 0);
		state = anim.GetCurrentAnimatorStateInfo(0);
	}

	//=======
	// 碰撞
	//=======
	void OnCollisionStay2D(Collision2D col)
	{
		if (col.contacts[0].normal == Vector2.up && col.gameObject.tag == "Ground")
		{
			isGround = true;
			BackSpped = 1;
		}
	}

	void OnCollisionExit2D(Collision2D col)
	{
		if (col.gameObject.tag == "Ground") 
		{
			isGround = false;
		}
	}

	//=======
	// 事件
	//=======

	public void SpawnButton()
	{
        if (!hasbtn)
        {
            btn = Instantiate(buttonObject);
            hasbtn = true;
        }
	}

	public void DestroyButton()
	{
		Destroy(btn);
        hasbtn = false;
	}
}
