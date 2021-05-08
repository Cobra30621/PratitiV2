using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class UIManager : MonoBehaviour
{
    // UI
    [SerializeField] private GameObject GO_pratitiStickerUI;
    private PratitiStickerUI _pratitiStickerUI;

    [SerializeField] private GameObject GO_StickerSelectedUI;
    private StickerSelectedUI _stickerSelectedUI;


    [SerializeField] private GameObject GO_pratitiBattleUI;
    private PratitiBattleUI _pratitiBattleUI;


    // 模糊
    [SerializeField] private GameObject GO_blurPanel;

    // 顯示按鍵文字
    [SerializeField] private GameObject GO_TeachingPanel;
    // [SerializeField] private  GO_TeachingPanel;


    // 按鍵
    [SerializeField]
    KeyCode keyCode_OpenPratitiUI;
    [SerializeField]
    KeyCode keyCode_ClosePratitiUI;
    [SerializeField]
    KeyCode keyCode_OpenPratitiBattleUI;



    // Start is called before the first frame update
    void Start()
    {
        InitUI();
    }

    // Update is called once per frame
    void Update()
    {
        InputProcess();
    }

    public void InitUI(){
        _pratitiStickerUI = GO_pratitiStickerUI.GetComponent<PratitiStickerUI>();
        _stickerSelectedUI = GO_StickerSelectedUI.GetComponent<StickerSelectedUI>();
        // _BagUI = GO_BagUI.GetComponent<BagUI>();
        _pratitiBattleUI = GO_pratitiBattleUI.GetComponent<PratitiBattleUI>();
    }

    public void InputProcess(){
        if(Input.GetKeyDown(keyCode_OpenPratitiUI))
            _pratitiStickerUI.Open();
        if(Input.GetKeyDown(keyCode_ClosePratitiUI))
            _pratitiStickerUI.Close();

        
        if(Input.GetKeyDown(keyCode_OpenPratitiBattleUI))
            _pratitiBattleUI.Open();
        if(Input.GetKeyDown(keyCode_ClosePratitiUI))
            _pratitiBattleUI.Close();
    }

    public void SetBlurPanel(bool bo){
        GO_blurPanel.SetActive(bo);
    }
}
