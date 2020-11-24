
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using InventorySystem;
using UnityEngine.SceneManagement;


//=======
// 基礎對話
//=======
// 進入戰鬥
//A:[CommandInfo("選單位置", "名稱", "簡介")]
[CommandInfo("Pratiti", "Talk", "打勾進入對話，打叉結束對話")]
public class Talk : Command
{
    [Header("開始、結束對話")]
    public bool boolen;
    [Header("是否開啟畫面變模糊")]
    public bool blur = true;

    // 開始時執行 
    public override void OnEnter()
    {
        UData.IsTalking = boolen;
        if (blur)
        {
            SetBlur(boolen);
        }

        Continue();
    }

    public void SetBlur(bool boolen)
    {
        FungusMapObject fungusMapObject = GameObject.Find("Fungus").GetComponent<FungusMapObject>();
        fungusMapObject.SetBlur(boolen);
    }

    // B:給與Command匡的物件文字名稱
    public override string GetSummary()
    {
        if (gameObject != null)
        {
            return "IsTalking:" + boolen  + "   blur:"  + blur;
        }
        return "nothing";
    }

    // C:設定Inspecter右下角顏
    public override Color GetButtonColor()
    {
        return new Color32(111, 222, 0, 221);
    }
}

// 畫面變模糊
//A:[CommandInfo("選單位置", "名稱", "簡介")]
[CommandInfo("Pratiti", "SetBlur", "畫面變模糊")]
public class SetBlur : Command
{
    public bool boolen;
    // 開始時執行 
    public override void OnEnter()
    {
        FungusMapObject fungusMapObject = GameObject.Find("Fungus").GetComponent<FungusMapObject>();
        fungusMapObject.SetBlur(boolen);
        Continue();
    }

    // B:給與Command匡的物件文字名稱
    public override string GetSummary()
    {
        if (gameObject != null)
        {
            return "SetBlur" + boolen;
        }
        return "nothing";
    }

    // C:設定Inspecter右下角顏
    public override Color GetButtonColor()
    {
        return new Color32(111, 222, 0, 221);
    }
}

// 畫面變模糊
//A:[CommandInfo("選單位置", "名稱", "簡介")]
[CommandInfo("Pratiti", "Setblack", "黑頻")]
public class Setblack : Command
{
    public bool boolen;
    // 開始時執行 
    public override void OnEnter()
    {
        FungusMapObject fungusMapObject = GameObject.Find("Fungus").GetComponent<FungusMapObject>();
        fungusMapObject.SetBlack(boolen);
        Continue();
    }

    // B:給與Command匡的物件文字名稱
    public override string GetSummary()
    {
        if (gameObject != null)
        {
            return "SetBlack" + boolen;
        }
        return "nothing";
    }

    // C:設定Inspecter右下角顏
    public override Color GetButtonColor()
    {
        return new Color32(111, 222, 0, 221);
    }
}

//=======
// 戰鬥系統相關
//=======
#region EnterBattle
// 進入戰鬥
//A:[CommandInfo("選單位置", "名稱", "簡介")]
[CommandInfo("Pratiti", "EnterBattle", "轉場")]
public class EnterBattle : Command
{
    // 開始時執行 
    public override void OnEnter()
    {
        SystemController.Instance.SaveMapData();
        SceneManager.LoadScene("Battle");
        Continue();
    }

    // B:給與Command匡的物件文字名稱
    public override string GetSummary()
    {
        if (gameObject != null)
        {
            return "進入戰鬥";
        }
        return "nothing";
    }

    // C:設定Inspecter右下角顏
    public override Color GetButtonColor()
    {
        return new Color32(111, 222, 0, 221);
    }
}
#endregion

#region EnterTutor
// 進入戰鬥
//A:[CommandInfo("選單位置", "名稱", "簡介")]
[CommandInfo("Pratiti", "EnterTutor", "進入戰鬥教學")]
public class EnterTutor : Command
{
    // 開始時執行 
    public override void OnEnter()
    {
        SystemController.Instance.SaveMapData();
        SceneManager.LoadScene("Tutorial");
        Continue();
    }

    // B:給與Command匡的物件文字名稱
    public override string GetSummary()
    {
        if (gameObject != null)
        {
            return "進入戰鬥";
        }
        return "nothing";
    }

    // C:設定Inspecter右下角顏
    public override Color GetButtonColor()
    {
        return new Color32(111, 222, 0, 221);
    }
}
#endregion


#region SetBattleData
// 進入戰鬥
//A:[CommandInfo("選單位置", "名稱", "簡介")]
[CommandInfo("Pratiti", "SetBattleData", "用現有的帕拉提提數值進入戰鬥")]
public class SetBattleData : Command
{
    [Header("Pratiti Setting")]
    public Pratiti thisPratiti = new Pratiti();
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

    [Header("掉落甜點類型")]
    public Item getItem;

    // 開始時執行 
    public override void OnEnter()
    {
        PratitiIntialize();
        Debug.Log("Instance: " + SystemController.Instance);
        SystemController.Instance.SetBattleData(thisPratiti, getItem);
        Continue();
    }

    private void PratitiIntialize()
    {
        PratitiController pC = new PratitiController();
        Debug.Log(PratitiBrand.GetPratitiBrand(pratitiName));
        if (isRandom)
            pC.PratitiInitialize(baseStatistic, PratitiBrand.GetPratitiBrand(pratitiName), sexDetermination, characteristic, IV, level);
        else
            pC.RandomData(PratitiBrand.GetPratitiBrand(pratitiName), level);
        thisPratiti = pC.toControl;
    }


    // B:給與Command匡的物件文字名稱
    public override string GetSummary()
    {
        if (gameObject != null)
        {
            return "設定戰鬥數值";
        }
        return "nothing";
    }

    // C:設定Inspecter右下角顏
    public override Color GetButtonColor()
    {
        return new Color32(111, 222, 0, 221);
    }
}
#endregion

#region EndBattle
// 進入戰鬥
//A:[CommandInfo("選單位置", "名稱", "簡介")]
[CommandInfo("Pratiti", "EndBattle", "結束戰鬥")]
public class EndBattle : Command
{
    // 開始時執行 
    public override void OnEnter()
    {
        SystemController.Instance.EndBattle();
        Continue();
    }

    // B:給與Command匡的物件文字名稱
    public override string GetSummary()
    {
        if (gameObject != null)
        {
            return "結束戰鬥";
        }
        return "nothing";
    }

    // C:設定Inspecter右下角顏
    public override Color GetButtonColor()
    {
        return new Color32(111, 222, 0, 221);
    }
}
#endregion


#region BackToMap
// 戰鬥結束後回到地圖
//A:[CommandInfo("選單位置", "名稱", "簡介")]
[CommandInfo("Pratiti", "BackToMap", "戰鬥結束回到地圖")]
public class BackToMap : Command
{
    // 開始時執行 
    public override void OnEnter()
    {
        //SystemController.Instance.FadeAndGo(UData.Map);
        SceneManager.LoadScene(UData.Map);
        Debug.Log("先轉場");

        Continue();
    }

    // B:給與Command匡的物件文字名稱
    public override string GetSummary()
    {
        if (gameObject != null)
        {
            return "戰鬥結束回到地圖";
        }
        return "nothing";
    }

    // C:設定Inspecter右下角顏
    public override Color GetButtonColor()
    {
        return new Color32(111, 222, 0, 221);
    }
}
#endregion



//=======
// 地圖切換相關
//=======
#region InitialMap
// 初始化地圖
//A:[CommandInfo("選單位置", "名稱", "簡介")]
[CommandInfo("Pratiti", "InitialMap", "打開地圖")]
public class InitialMap : Command
{
    // 開始時執行 
    public override void OnEnter()
    {

        SystemController.Instance.InitialMap();
        Continue();
    }

    // B:給與Command匡的物件文字名稱
    public override string GetSummary()
    {
        if (gameObject != null)
        {
            return "打開地圖";
        }
        return "nothing";
    }

    // C:設定Inspecter右下角顏
    public override Color GetButtonColor()
    {
        return new Color32(111, 222, 0, 221);
    }
}
#endregion

#region Protal
// 切換地圖
//A:[CommandInfo("選單位置", "名稱", "簡介")]
[CommandInfo("Pratiti", "Protal", "同地圖轉移")]
public class Protal : Command
{
    public GameObject NowMap;
    public GameObject ChangeMap;
    // 開始時執行 
    public override void OnEnter()
    {
        NowMap.SetActive(false);
        ChangeMap.SetActive(true);
        UData.smallMap = ChangeMap.name;
        Continue();
    }


    // B:給與Command匡的物件文字名稱
    public override string GetSummary()
    {
        if (gameObject != null)
        {
            return "同地圖轉移";
        }
        return "nothing";
    }

    // C:設定Inspecter右下角顏
    public override Color GetButtonColor()
    {
        return new Color32(111, 222, 0, 221);
    }
}
#endregion

#region SetSmallMap
// 設定小地圖
//A:[CommandInfo("選單位置", "名稱", "簡介")]
[CommandInfo("Pratiti", "SetSmallMap", "設立初始地圖")]
public class SetSmallMap : Command
{
    public string mapName;
    // 開始時執行 
    public override void OnEnter()
    {
        UData.smallMap = mapName;
        Continue();
    }


    // B:給與Command匡的物件文字名稱
    public override string GetSummary()
    {
        if (gameObject != null)
        {
            return "同地圖轉移";
        }
        return "nothing";
    }

    // C:設定Inspecter右下角顏
    public override Color GetButtonColor()
    {
        return new Color32(111, 222, 0, 221);
    }
}
#endregion

//=======
// 甜點系統方法
//=======

// 設定甜點數量
//A:[CommandInfo("選單位置", "名稱", "簡介")]
[CommandInfo("Pratiti", "RemoveItem", "將甜點數與fungus連動")]
public class RemoveItem : Command
{
    public Item item;
    // private string[] desserts = {  "巧克力", "藍莓", "堅果", "牛奶", "雞蛋", "黃油" };
    // 開始時執行 
    public override void OnEnter()
    {
        SystemController.Instance.RemoveItem(item);
        Continue();
    }

    // B:給與Command匡的物件文字名稱
    public override string GetSummary()
    {
        if (gameObject != null)
        {
            return "刪除道具";
        }
        return "nothing";
    }

    // C:設定Inspecter右下角顏
    public override Color GetButtonColor()
    {
        return new Color32(111, 222, 0, 221);
    }
}

// 設定甜點數量
//A:[CommandInfo("選單位置", "名稱", "簡介")]
[CommandInfo("Pratiti", "AddItem", "增加道具")]
public class AddItem : Command
{
    // private Item item = new Item {itemName = Item.ItemName.雞蛋, quantity = 11 };


    public Item item;
    // private string[] desserts = {  "巧克力", "藍莓", "堅果", "牛奶", "雞蛋", "黃油" };
    // 開始時執行 
    public override void OnEnter()
    {
        SystemController.Instance.AddItem(item);
        Continue();
    }

    // B:給與Command匡的物件文字名稱
    public override string GetSummary()
    {
        if (gameObject != null)
        {
            return "將甜點數與fungus連動";
        }
        return "nothing";
    }

    // C:設定Inspecter右下角顏
    public override Color GetButtonColor()
    {
        return new Color32(111, 222, 0, 221);
    }
}


//=======
// 場景物件移動
//=======

// 設定甜點數量
//A:[CommandInfo("選單位置", "名稱", "簡介")]
[CommandInfo("Pratiti", "PlayerMove", "玩家移動")]
public class PlayerMove : Command
{
    public float duration = 1;
    [Header("1是往右，-1是往左")]
    public int direction = 1;

    public float speed;

    private Controller controller;

    // 開始時執行 
    public override void OnEnter()
    {
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<Controller>();
        StartCoroutine(MoveItem());
        Continue();
    }



    IEnumerator MoveItem()
    {
        controller.speed = speed;
        for (float i = 0; i <= duration; i+= Time.deltaTime)
        {
            controller.Move(direction);
            controller.Direction(direction);
            yield return 0;
        }

    }

    // B:給與Command匡的物件文字名稱
    public override string GetSummary()
    {
        if (gameObject != null)
        {
            return "玩家移動" + direction;
        }
        return "nothing";
    }

    // C:設定Inspecter右下角顏
    public override Color GetButtonColor()
    {
        return new Color32(111, 222, 0, 221);
    }
}

// 設定甜點數量
//A:[CommandInfo("選單位置", "名稱", "簡介")]
[CommandInfo("Pratiti", "PlayerStop", "玩家停止移動")]
public class PlayerStop : Command
{

    private Controller controller;

    // 開始時執行 
    public override void OnEnter()
    {
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<Controller>();
        controller.Move(0);
        controller.speed = 1000f;
        Continue();
    }


    // B:給與Command匡的物件文字名稱
    public override string GetSummary()
    {
        if (gameObject != null)
        {
            return "玩家停止移動";
        }
        return "nothing";
    }

    // C:設定Inspecter右下角顏
    public override Color GetButtonColor()
    {
        return new Color32(111, 222, 0, 221);
    }
}


// 設定甜點數量
//A:[CommandInfo("選單位置", "名稱", "簡介")]
[CommandInfo("Pratiti", "MissionIcon", "判斷NPC身上有無任務")]
public class MissionIcon : Command
{
    [Header("對話觸發器")]
    public GameObject Trigger;

    [Header("現在有無任務")]
    public bool haveMission ;
    private StoryTrigger storyTrigger;

    // 開始時執行 
    public override void OnEnter()
    {
        storyTrigger = Trigger.GetComponent<StoryTrigger>();
        storyTrigger.haveMission = haveMission;
        Continue();
    }


    // B:給與Command匡的物件文字名稱
    public override string GetSummary()
    {
        if (gameObject != null)
        {
            return "NPC任務" + haveMission;
        }
        return "nothing";
    }

    // C:設定Inspecter右下角顏
    public override Color GetButtonColor()
    {
        return new Color32(111, 222, 0, 221);
    }
}

/*
//A:[CommandInfo("選單位置", "名稱", "簡介")]
[CommandInfo("Pratiti", "GetDessert", "轉場")]
public class GetDessert : Command
{
    public string dessertName;
    public int num;
    // 開始時執行 
    public override void OnEnter()
    {
        //string[] desserts = { "藍莓" , "巧克力", "堅果" , "巧克力蛋糕" , "快樂草" , "拴繩" , "牛奶" , "雞蛋", "黃油" };
        Continue();
    }

    // B:給與Command匡的物件文字名稱
    public override string GetSummary()
    {
        if (gameObject != null)
        {
            return "進入戰鬥";
        }
        return "nothing";
    }

    // C:設定Inspecter右下角顏
    public override Color GetButtonColor()
    {
        return new Color32(111, 222, 0, 221);
    }
}

// 將甜點數與fungus連動
//A:[CommandInfo("選單位置", "名稱", "簡介")]
[CommandInfo("Pratiti", "InitialDessert", "將甜點數與fungus連動")]
public class InitialDessert : Command
{
    private string[] desserts = { "巧克力", "牛奶", "雞蛋" };
    // private string[] desserts = {  "巧克力", "藍莓", "堅果", "牛奶", "雞蛋", "黃油" };
    // 開始時執行 
    public override void OnEnter()
    {
        foreach ( string dessert in desserts)
        {
            SystemController.Instance.SetFungusDessert(dessert);
        }
        Continue();
    }

    // B:給與Command匡的物件文字名稱
    public override string GetSummary()
    {
        if (gameObject != null)
        {
            return "將甜點數與fungus連動";
        }
        return "nothing";
    }

    // C:設定Inspecter右下角顏
    public override Color GetButtonColor()
    {
        return new Color32(111, 222, 0, 221);
    }
}




*/
