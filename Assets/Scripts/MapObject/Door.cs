using UnityEngine;


public class Door : MonoBehaviour
{
    internal bool inView; // 是否在視野內
    public string map;
    [Header("現在的地圖是否為主地圖")]
    public bool nowIsMainMap;
    [Header("要去的地圖是否為主地圖")]
    public bool goIsMainMap;

    private void Awake()
    {
        inView = false;
    }

    private void Update()
    {
        // 按下Z鍵且可以開始對話啟動（inView = true）
        if (Input.GetKeyDown(KeyCode.Z) && inView)
        {
            // 如果現在是主地圖，儲存玩家位置
            if (nowIsMainMap)
            {
                SystemController.Instance.SaveMapData();
            }

            // 如果要去的是主地圖，初始化地圖
            if (goIsMainMap)
            {
                // SystemController.Instance.MainMap = true;
            }
            else
            {
                // SystemController.Instance.MainMap = false;
            }

            // SystemController.Instance.FadeAndGo(map);
            Debug.Log("新增轉場");
        }
    }


    private void OnTriggerEnter2D(UnityEngine.Collider2D other)
    {
        // 當玩家靠夠近時，可以開始對話（inView = true;）
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent < Controller>().SpawnButton();
            inView = true;
        }
    }

    private void OnTriggerExit2D(UnityEngine.Collider2D other)
    {
        // 當玩家靠太遠時，無法開始對話（inView = true;）
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<Controller>().DestroyButton();
            inView = false;
        }
    }
}
