using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PratitiMove : MonoBehaviour
{
    private enum MoveState
    {
        IDLE,      //原地
        CHASE,      //追擊玩家
        ISDEFEAT // 被擊敗
    }
    private MoveState currentState;
    public bool isDefeat;

    [Range(0f,1000f)]
    public float runSpeed = 140f;
     public float turnSpeed = 0.1f; 

    private  Rigidbody2D body;
    public Vector3 startPos;
    private GameObject playerGO;
    private float diatanceToPlayer;
    public float chaseRadius; 
    private Quaternion targetRotation;         //怪物的目標朝向
    public GameObject GO;
    
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        startPos = transform.position;
        playerGO = GameMediator.Instance.GetPlayerObject();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateProcess();
    }

    public void UpdateProcess(){
        DistanceCheck();
        switch(currentState){
            case MoveState.IDLE:
                break;
            case MoveState.CHASE:
                transform.Translate(Vector3.forward * Time.deltaTime * runSpeed);
                //朝向玩家位置
                targetRotation = Quaternion.LookRotation(playerGO.transform.position - transform.position, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed);
                break;
            case MoveState.ISDEFEAT:
                break;
            default:
                break;
        }
    }

    private void DistanceCheck()
    {
        diatanceToPlayer = Vector2.Distance(playerGO.transform.position, transform.position);
        if (diatanceToPlayer < chaseRadius)
            currentState = MoveState.CHASE;
        else
            currentState = MoveState.IDLE;
    }


    public void Reset(){
        isDefeat = false;
        transform.position = startPos;
        GO.SetActive(true);
    }

    public void IsDefeat(){
        isDefeat = true;
        currentState = MoveState.ISDEFEAT;
        GO.SetActive(false);
    }

    public virtual void Move(int i)
		{
			body.velocity = new Vector2(i * runSpeed * Time.deltaTime, body.velocity.y);

			// anim.SetFloat("Move", Mathf.Abs(i + 0f));
		}

		public virtual void Direction(int i)
		{
			transform.eulerAngles = new Vector3(0, 180 * i, 0);
		}
}
