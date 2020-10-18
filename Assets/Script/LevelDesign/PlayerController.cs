using UnityEngine;
using System.Collections;

namespace MapSystemSpace{
	public class PlayerController : MonoBehaviour
	{
		[Header("玩家移動數值")]
		[Range(0f,1000f)]
		public float speed = 140f;
		[Range(0f, 100f)]
		public float jumpHeight = 10f;
		[Header("玩家物件")]
		public GameObject sprite;

		internal bool isGround = false;
		internal Rigidbody2D body;
		// internal Animator anim;
		// internal AnimatorStateInfo state;

		public bool IsTalking
		{
			get{return GameMediator.Instance.WhetherIsTalking();}
		}
		// internal bool lockMove;

		public void Awake()
		{

			// 忽略碰撞，Player(9)與NPC(8)
			//Physics2D.IgnoreLayerCollision(8, 9);
			// 忽略碰撞，NPC與NPC(9)
			// Physics2D.IgnoreLayerCollision(9, 9);
			// 取得各類元
			body = GetComponent<Rigidbody2D>();
			// anim = sprite.GetComponentInChildren<Animator>();
		}

		public void FixedUpdate()
		{
			Control();
			StateMachine();

		}

		public virtual void Control()
		{
			Move(0);

			if (IsTalking)
			{
				Move(0);
			}
			if (Input.GetKey(KeyCode.RightArrow) && !IsTalking)
			{
				Move(1);
				Direction(1);
			}
			if (Input.GetKey(KeyCode.LeftArrow)&& !IsTalking)
			{
				Move(-1);
				Direction(0);
			}
			if (Input.GetKey(KeyCode.Space)&& !IsTalking)
			{
				Jump();
				/*
				if (anim.GetCurrentAnimatorStateInfo(0).IsName("Blend Tree"))
				{
					anim.SetBool("Jump", true);
					Jump();
				}
				else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Jump finish") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.3f)
				{
					anim.SetBool("Jump", true);
					Jump();
				}*/
			}
			if (body.velocity.y < 0)
			{
				// anim.SetBool("Jump", false);
			}
			if (Input.GetKeyUp(KeyCode.RightArrow))
			{
				Move(0);
			}
			if (Input.GetKeyUp(KeyCode.LeftArrow))
			{
				Move(0);
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
			body.velocity = new Vector2(i * speed * Time.deltaTime, body.velocity.y);

			// anim.SetFloat("Move", Mathf.Abs(i + 0f));
		}

		public virtual void Direction(int i)
		{
			transform.eulerAngles = new Vector3(0, 180 * i, 0);
		}

		//==========
		// 動畫
		//==========
		public void StateMachine()
		{
			// anim.SetBool("Ground", isGround);
			// anim.SetFloat("Y", body.velocity.y);
			//anim.SetFloat("Ammo", Game.sav.HasAmmo() ? 1 : 0);
			// state = anim.GetCurrentAnimatorStateInfo(0);
		}

		//=======
		// 碰撞
		//=======
		void OnCollisionStay2D(Collision2D col)
		{
			if (col.contacts[0].normal != Vector2.up) { return; }
			isGround = true;
		}

		void OnCollisionExit2D(Collision2D col)
		{
			isGround = false;
		}

	}
}

