using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NameBattleSystem;

public class TutorialControl : MonoBehaviour
{
    public GameObject NipliseObj;
    public GameObject tutorialBlock;
    public GameObject dialogBox;
    public GameObject dialogBoxChildObj;
    public GameObject textObj;
    public GameObject stopTalkingTriangle;
    public bool nextDialog;
    public bool isTalking;
    public bool isTextFollowChild;
    public Animator playerAnimator;
    public Animator NipliseAnimator;
    public Animator dialogBoxAnimator;
    public int tutorialState;
    public Text text;
    public float letterPause;
    public int dialogPause;
    public string dialog00;
    public string dialog01;
    public string dialog02;
    public string dialog03;
    public string dialog04;
    public string dialog05;
    public string dialog06;
    public string dialog07;
    public string dialog08;
    public string dialog09;
    public string dialog10;
    public string dialog11;
    public string dialog12;
    public string dialog13;
    public string dialog14;
    public string dialog15;
    public string dialog16;
    public string dialog17;
    public string dialog18;
    void Start()
    {
        nextDialog = false;
        isTextFollowChild = false;
        letterPause = 0.02f;
        dialogPause = 1;
        dialog00 = "哈哈~";
        dialog01 = "讓我們開始帕拉緹緹的戰鬥教學吧~";
        dialog02 = "首先我們來教最基本的普通攻擊~\r\n請按下鍵盤上的左方向鍵~";
        dialog03 = "哇~~~嗚嗚嗚!!!\r\n多麼精湛的一擊!!!";
        dialog04 = "好的~\r\n你應該有注意到這邊...";
        dialog05 = "\r\n我頭上這個!";
        dialog06 = "這就是敵人的狀態條~\r\n而另一邊則是你可愛的帕拉緹緹的~";
        dialog07 = "那個紅色的東東叫做體力值\r\n帕拉緹緹被攻擊的話可是會減少體力值的!";
        dialog08 = "帕拉緹緹的體力值歸零時可是會昏倒的!!!\r\n這點要多多注意喔!";
        dialog09 = "接著看看這邊...";
        dialog10 = "\r\n黃色的這個!";
        dialog11 = "這個是能量!\r\n當能量值滿了的時候帕拉緹緹就可以使出有別於平常的強大攻擊!";
        dialog12 = "就是敵人的狀態條~\r\n而左邊的就是你可愛的帕拉緹緹的~";
        tutorialState = 0;
        tutorialBlock.SetActive(true);
        StartCoroutine(tutorialStateChange());
        StartCoroutine(dialogControl());
    }
    void Update()
    {
        dialogBox.transform.position = new Vector3(NipliseObj.transform.position.x + 3.4f, NipliseObj.transform.position.y + 1.2671f);
        if(tutorialState == 0)
        {
            BattleSystem.motionStop = true;
        }
        if(Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Return))
        {
            nextDialog = true;
        }
        else
        {
            nextDialog = false;
        }
        textObj.transform.position = new Vector3(dialogBoxChildObj.transform.position.x * 107 + 963.4805f, dialogBoxChildObj.transform.position.y * 107 + 531.9f);
        stopTalkingTriangle.transform.position = new Vector3(dialogBoxChildObj.transform.position.x - 1.676f, dialogBoxChildObj.transform.position.y - 2.0029f);
       
    }
    public IEnumerator tutorialStateChange()
    {
        yield return new WaitUntil(() => NipliseObj.transform.position.y > 1);
        yield return new WaitUntil(() => NipliseObj.transform.position.y <= 0.733f);
        isTalking = true;
        tutorialState = 1;
        yield return new WaitUntil(() => nextDialog && isTalking == false);
        tutorialState = 2;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.LeftArrow));
        tutorialState = 3;
        StartCoroutine(PlayerFirstAttack());
        yield return new WaitUntil(() => playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Hit Complete"));
        isTalking = true;
        tutorialState = 4;
        StartCoroutine(NipliseGotCrazy());
        yield return new WaitUntil(() => nextDialog && isTalking == false);
        tutorialState = 5;
        StartCoroutine(NipliseWalktoRightBar());
        yield return new WaitUntil(() => NipliseAnimator.GetCurrentAnimatorStateInfo(0).IsName("Niplise Jump One RightBar"));
        isTalking = true;
        tutorialState = 6;
        yield return new WaitUntil(() => nextDialog && isTalking == false);
        isTalking = true;
        tutorialState = 7;
        yield return new WaitUntil(() => nextDialog && isTalking == false);
        isTalking = true;
        tutorialState = 8;
        yield return new WaitUntil(() => nextDialog && isTalking == false);
        isTalking = true;
        tutorialState = 9;
        yield return new WaitUntil(() => nextDialog && isTalking == false);
        tutorialState = 10;
        StartCoroutine(NipliseRightBarToLeftBar());
        yield return new WaitUntil(() => NipliseAnimator.GetCurrentAnimatorStateInfo(0).IsName("Niplise Jump One LeftBar"));
        isTalking = true;
        tutorialState = 11;
        yield return new WaitUntil(() => nextDialog && isTalking == false);
        isTalking = true;
        tutorialState = 12;

    }
    IEnumerator dialogControl()
    {
        yield return new WaitUntil(() => tutorialState == 1);
        StartCoroutine(ContinueTalk(dialog00, dialog01));
        yield return new WaitUntil(() => tutorialState == 2);
        StartCoroutine(NewTalk(dialog02));
        yield return new WaitUntil(() => tutorialState == 4);
        StartCoroutine(NewTalk(dialog03));
        yield return new WaitUntil(() => tutorialState == 5);
        StartCoroutine(NewTalk(dialog04));
        yield return new WaitUntil(() => tutorialState == 6);
        StartCoroutine(ContinueTalk(dialog04, dialog05));
        yield return new WaitUntil(() => tutorialState == 7);
        StartCoroutine(NewTalk(dialog06));
        yield return new WaitUntil(() => tutorialState == 8);
        StartCoroutine(NewTalk(dialog07));
        yield return new WaitUntil(() => tutorialState == 9);
        StartCoroutine(NewTalk(dialog08));
        yield return new WaitUntil(() => tutorialState == 10);
        StartCoroutine(NewTalk(dialog09));
        yield return new WaitUntil(() => tutorialState == 11);
        StartCoroutine(ContinueTalk(dialog09, dialog10));
        yield return new WaitUntil(() => tutorialState == 12);
        StartCoroutine(NewTalk(dialog11));
    }
    IEnumerator NewTalk(string str)
    {
        stopTalkingTriangle.SetActive(false);
        text.text = null;
        foreach (var word in str)
        {
            text.text += word;
            yield return new WaitForSeconds(letterPause);
            if(nextDialog)
            {
                break;
            }
        }
        text.text = str;
        isTalking = false;
        stopTalkingTriangle.SetActive(true);
    }
    IEnumerator ContinueTalk(string str0, string str1)
    {
        stopTalkingTriangle.SetActive(false);
        foreach (var word in str1)
        {
            text.text += word;
            yield return new WaitForSeconds(letterPause);
            if (nextDialog)
            {
                break;
            }
        }
        text.text = str0+str1;
        isTalking = false;
        stopTalkingTriangle.SetActive(true);
    }
    public IEnumerator PlayerFirstAttack()
    {
        BattleSystem.ability01.hitRate = 1000;
        print("玩家使用攻擊");
        BattleSystem.player.hitRecoveryTime = BattleSystem.player.attackStartHitRecoveryTime + BattleSystem.player.attackCompleteHitRecoveryTime;
        BattleSystem.player.hitRecoveryTimeMax = BattleSystem.player.hitRecoveryTime;
        BattleSystem.player.isHitRecovery = true;
        playerAnimator.SetTrigger("Attack");
        yield return null;
    }
    public IEnumerator NipliseGotCrazy()
    {
        NipliseAnimator.SetTrigger("Jump");
        dialogBoxAnimator.SetTrigger("Wiggle");
        yield return null;
    }
    public IEnumerator NipliseWalktoRightBar()
    {
        NipliseAnimator.SetTrigger("WalkRightBar");
        dialogBoxAnimator.SetTrigger("WalkRightBar");
        yield return null;
    }
    public IEnumerator NipliseRightBarToLeftBar()
    {
        NipliseAnimator.SetTrigger("RightToLeft");
        dialogBoxAnimator.SetTrigger("RightToLeft");
        yield return null;
    }
}