using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISaveable
{
    object CaptureState(); // 存檔
    void RestoreState(object state); // 讀檔進來
}
