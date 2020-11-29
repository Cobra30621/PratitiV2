using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleControl : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public static IEnumerator TimeControl(float time_speed, float duration)
    {
        Time.timeScale = time_speed;
        yield return new WaitForSecondsRealtime(duration);
        //duration = 10;
        //for(int times = 0;times<duration;times++)
        //{
        //    yield return null;
        //}
        Time.timeScale = 1;
    }
}
