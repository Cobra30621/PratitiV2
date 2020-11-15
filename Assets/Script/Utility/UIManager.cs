using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // UI
    [SerializeField] private GameObject GO_pratitiUI;
    private PratitiUI _pratitiUI;

    [SerializeField] private GameObject GO_StickerSelectedUI;
    private StickerSelectedUI _stickerSelectedUI;

    // 按鍵
    [SerializeField]
    KeyCode keyCode_OpenPratitiUI;
    [SerializeField]
    KeyCode keyCode_ClosePratitiUI;



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
        _pratitiUI = GO_pratitiUI.GetComponent<PratitiUI>();
        _stickerSelectedUI = GO_StickerSelectedUI.GetComponent<StickerSelectedUI>();
    }

    public void InputProcess(){
        if(Input.GetKeyDown(keyCode_OpenPratitiUI))
            _pratitiUI.Open();
        if(Input.GetKeyDown(keyCode_ClosePratitiUI))
            _pratitiUI.Close();
        
    }
}
