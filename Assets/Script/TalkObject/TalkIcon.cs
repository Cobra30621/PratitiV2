using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum TalkIconType{
    Null = 0 , mission, talk , door
}

public class TalkIcon{
    public TalkIconType talkIconType;
    public GameObject talkIcon;

    public TalkIcon(TalkIconType talkIconType, GameObject talkObject){
        this.talkIconType = talkIconType;

        if(talkIconType == TalkIconType.Null){return;}
        LoadAssest();
        talkIcon.transform.SetParent( talkObject.transform);
        talkIcon.transform.position = talkObject.transform.position + new Vector3(0.2f, 1.5f, 0);
        CloseIcon();
    }

    public void LoadAssest(){
        Debug.Log("GameObject 載入資料夾的Icon");
        if (talkIconType == TalkIconType.Null){return;}
        
        IAssetFactory AssetFactory = MainFactory.GetAssetFactory();
		talkIcon = AssetFactory.LoadTalkIcon(talkIconType.ToString() + "Icon");

    } 

    public void ShowIcon(){
        if(talkIconType == TalkIconType.Null){return;}
        talkIcon.SetActive(true);
    }

    public void CloseIcon(){
        if(talkIconType == TalkIconType.Null){return;}
        talkIcon.SetActive(false);
    }

}
