using System.Collections;
using UnityEngine;

public class ButtonObject : MonoBehaviour {
	Transform pl ;
	
	void Awake(){
		pl = GameObject.FindWithTag("Player").transform ;
	}
	
	void Update(){
		transform.position = pl.position + new Vector3(-0.7f, 2.2f, 0) ;
	}
}
