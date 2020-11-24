using UnityEngine;
using System.Collections;

public class Navigation2D : MonoBehaviour {

	public float speed = 23f ;
    public float speedRate = 1.0f ;
	public float stoppingDistance = 0.05f ;
	internal Vector3 targetPos ;
	internal bool moving = false ; 
	internal bool enableMove = true ;
	Rigidbody2D body ;

    // Event
    public delegate void MoveEvent() ;
    public MoveEvent OnReach ;

	void Awake(){
		body = GetComponent<Rigidbody2D>() ;
	}

	void FixedUpdate(){
        float movSpeed = Time.fixedDeltaTime*speed*speedRate*Mathf.Sign(targetPos.x-transform.position.x) ;
		Vector3 vel = body.velocity ;
		vel.x = (moving  && enableMove) ?  movSpeed : 0 ;
		body.velocity = vel ;
  
		// 方向判定
		if(body.velocity.x != 0){
			Vector3 angle = transform.eulerAngles ;
			angle.y = body.velocity.x > 0 ? 0 : 180 ;
			transform.eulerAngles = angle ;
		}
		// 到達目標點停止
		if(ReachGoal()){
            // 呼叫綁定的事件
            if(OnReach != null){
                OnReach() ;
                OnReach = null ;
            }
			StopMove() ;
		}
	}

	//======
	// 事件
	//======
	public void StartMove(){
		moving = true ;
	}

	public void StopMove(){
		moving = false ;
	}

	public void MoveTo(Vector3 p){
		targetPos = p ;
		StartMove() ;
	}

    public void FaceTo(int i){
        Vector3 angle = transform.eulerAngles ;
        angle.y = i == 1 ? 0 : 180 ;
        transform.eulerAngles = angle ;
    }
	
	public void Follow(Transform tar){
		targetPos = tar.position ;
		if(!ReachGoal()){
			StartMove() ;
		}
	}
	
	public bool ReachGoal(){
		return Mathf.Abs(transform.position.x-targetPos.x) <= stoppingDistance && moving ;
	}
}
