using UnityEngine;
using System;
using System.Collections;

// 遊戲主迴圈
public class GameLoop : MonoBehaviour
{
    [Header("Player Place")]
    public Vector3 playerStartPos;
    public string smallMap;

    [Header("Pratiti Setting")]
    public bool isRandom;
    public int baseStatistic;
    public PratitiBrand.PratitiName pratitiName;
    [Range(0, 100)]
    public int sexDetermination;
    [Range(0, 100)]
    public int characteristic;
    [Range(0, 100)]
    public int IV;
    [Range(0, 100)]
    public int level;

    // 跨場景物件
    static GameLoop instance;
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if(this != instance)
        {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start()
    {
        SystemController.Instance.Initinal();
        Debug.Log("初始化地圖在GameLoop");
        UData.Map = "Map";
        UData.smallMap = smallMap;
        UData.PlayerPos = playerStartPos;
        SystemController.Instance.PlayBlock("S", "InitialStoryFlow");
        SystemController.Instance.PlayBlock("S", "Start");

        SystemController.Instance.ClearPratitiData();
        SystemController.Instance.AddPratiti(PratitiIntialize());
    }

    private Pratiti PratitiIntialize()
    {
        PratitiController pC = new PratitiController();

        Debug.Log(PratitiBrand.GetPratitiBrand(pratitiName));
        if (isRandom)
            pC.PratitiInitialize(baseStatistic, PratitiBrand.GetPratitiBrand(pratitiName), sexDetermination, characteristic, IV, level);
        else
            pC.RandomData(PratitiBrand.GetPratitiBrand(pratitiName), level);

        return pC.toControl;
    }
}
