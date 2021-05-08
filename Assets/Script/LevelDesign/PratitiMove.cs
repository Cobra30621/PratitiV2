using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveState
{
    IDLE,      //原地
    WARNING, // 注意到玩家
    CHASE,      //追擊玩家
    STOPCHASE, // 追到玩家，停止追擊
    ISDEFEAT // 被擊敗
}

public class PratitiMove : MonoBehaviour, ISaveable
{
    
    public MoveState currentState;
    private bool isPlayedAnime;
    public bool isDefeat;

    [Range(0f,1000f)]
    public float runSpeed = 140f;
    public float startSpeed = 2f;
   

    private  Rigidbody2D body;
    public Vector3 startPos;
    private GameObject playerGO;
    private float diatanceToPlayer;
    public float chaseRadius; 
     public float stopChargeRadius = 1f;
    private Quaternion targetRotation;         //怪物的目標朝向
    public GameObject GO;
    public GameObject WarningIcon;
    public MapName mapName;
    private TalkObject talkObject;

    
    // Start is called before the first frame update
    void Start()
    {
        talkObject = GetComponent<TalkObject>();
        body = GetComponent<Rigidbody2D>();
        startPos = transform.position;
        playerGO = GameMediator.Instance.GetPlayerObject();
        if(isDefeat == true)
            IsDefeat();
    }

    // Update is called once per frame
    void Update()
    {
        bool IsTalking = GameMediator.Instance.WhetherIsTalking();
        if(IsTalking)
            return; 

        bool UsingUI = GameMediator.Instance.WhetherUsingUI();
        if(UsingUI)
            return;

        UpdateProcess();
    }

    public void UpdateProcess(){
        
        switch(currentState){
            case MoveState.IDLE:
                DistanceCheck();
                break;
            case MoveState.WARNING:
                if(isPlayedAnime)
                    return; 
                float distanceX = playerGO.transform.position.x - transform.position.x;
                int dir = distanceX > 0 ? 1: -1;
                Move(dir * startSpeed);
                Direction(playerGO.transform.position.x - transform.position.x);
                this.StartCoroutine(StartToChase());
                break;
            case MoveState.CHASE:
                DistanceCheck();
                float distanceX2 = playerGO.transform.position.x - transform.position.x;
                int dir2 = distanceX2 > 0 ? 1: -1;
                Move(dir2);
                Direction(dir2);
                break;
            case MoveState.STOPCHASE:
                DistanceCheck();
                break;
            case MoveState.ISDEFEAT:
                break;
            default:
                break;
        }
    }

    private void DistanceCheck()
    {
        diatanceToPlayer = Mathf.Abs(playerGO.transform.position.x - transform.position.x) ;
        // Vector2.Distance(playerGO.transform.position, transform.position);
        if (diatanceToPlayer < chaseRadius)
        {
            if(currentState == MoveState.CHASE ||currentState == MoveState.STOPCHASE) // 如果整在追蹤，不需要再設為警告
            {
                if(diatanceToPlayer < stopChargeRadius){ // 距離夠近，停止追擊
                    currentState = MoveState.STOPCHASE;
                }
                else{
                    currentState = MoveState.CHASE;
                }
                return;
            }
            currentState = MoveState.WARNING;
        }
        else
            currentState = MoveState.IDLE;
    }

    private IEnumerator StartToChase()
    {
        isPlayedAnime = true;
        ShowWarningIcon(true);
        yield return new WaitForSeconds(0.7f);
        currentState = MoveState.CHASE;
        WarningIcon.SetActive(false);
        isPlayedAnime = false;
    }

    private void ShowWarningIcon(bool bo){
        WarningIcon.SetActive(bo);
        
    }


    public void Reset(){
        isDefeat = false;
        transform.position = startPos;
        body.velocity = Vector3.zero;
        talkObject = GetComponent<TalkObject>();
        talkObject.CanTalk(true);
        GO.SetActive(true);
        currentState = MoveState.IDLE;
    }

    public void IsDefeat(){
        isDefeat = true;
        talkObject = GetComponent<TalkObject>();
        talkObject.CanTalk(false);
        currentState = MoveState.ISDEFEAT;
        GO.SetActive(false);
    }

    public void Move(float i)
    {
        body.velocity = new Vector2(i * runSpeed * Time.deltaTime, body.velocity.y);

        // anim.SetFloat("Move", Mathf.Abs(i + 0f));
    }

    public void Direction(float i)
    {
        if(i < 0)
            transform.eulerAngles = new Vector3(0, 0 , 0);
        else
            transform.eulerAngles = new Vector3(0, 180 , 0);
    }

    public string GetID(){
        if (GetComponent<SaveableEntity>() == null){
            Debug.Log("沒掛SaveableEntity，請掛上");
            return "";
        }
        if ( GetComponent<SaveableEntity>().ID == ""){
            Debug.Log(mapName + "的"+ gameObject + "尚未產生ID，請產生");
            return "";
        }

        return GetComponent<SaveableEntity>().ID;
    }

    // 將目前檔案匯出
    public object CaptureState(){
        return new SaveData{
            isDefeat = isDefeat
        };
    }

    // 讀取檔案近來
    public void RestoreState (object state){
        var saveData = (SaveData)state;

        isDefeat = saveData.isDefeat;
    }

    [System.Serializable]
    private struct SaveData{
        public bool isDefeat;
    }
}

// 3D追蹤
// transform.Translate(Vector3.forward * Time.deltaTime * runSpeed);
// //朝向玩家位置
// targetRotation = Quaternion.LookRotation(playerGO.transform.position - transform.position, Vector3.up);
// float rotationx = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed).x;
// transform.rotation = new Vector3 (rotationx, 0f, 0f);