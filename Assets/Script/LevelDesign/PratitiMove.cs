using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PratitiMove : MonoBehaviour, ISaveable
{
    private enum MoveState
    {
        IDLE,      //原地
        WARNING, // 注意到玩家
        CHASE,      //追擊玩家
        ISDEFEAT // 被擊敗
    }
    private MoveState currentState;
    private bool isPlayedAnime;
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
    public GameObject WarningIcon;

    public int test;
    
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
            case MoveState.WARNING:
                if(isPlayedAnime)
                    return; 
                Direction(playerGO.transform.position.x - transform.position.x);
                this.StartCoroutine(StartToChase());
                break;
            case MoveState.CHASE:
                float distanceX = playerGO.transform.position.x - transform.position.x;
                int dir = distanceX > 0 ? 1: -1;
                Move(dir);
                Direction(dir);
                
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
        {
            if(currentState == MoveState.CHASE) // 如果整在追蹤，不需要再設為警告
                return;
            currentState = MoveState.WARNING;
        }
        else
            currentState = MoveState.IDLE;
    }

    private IEnumerator StartToChase()
    {
        isPlayedAnime = true;
        ShowWarningIcon(true);
        yield return new WaitForSeconds(1);
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
        GO.SetActive(true);
    }

    public void IsDefeat(){
        isDefeat = true;
        currentState = MoveState.ISDEFEAT;
        GO.SetActive(false);
    }

    public void Move(int i)
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

    // 將目前檔案匯出
    public object CaptureState(){
        return new SaveData{
            isDefeat = isDefeat,
            test = test
        };
    }

    // 讀取檔案近來
    public void RestoreState (object state){
        var saveData = (SaveData)state;

        isDefeat = saveData.isDefeat;
        test = saveData.test;
    }

    [System.Serializable]
    private struct SaveData{
        public bool isDefeat;
        public int test;
    }
}

// 3D追蹤
// transform.Translate(Vector3.forward * Time.deltaTime * runSpeed);
// //朝向玩家位置
// targetRotation = Quaternion.LookRotation(playerGO.transform.position - transform.position, Vector3.up);
// float rotationx = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed).x;
// transform.rotation = new Vector3 (rotationx, 0f, 0f);