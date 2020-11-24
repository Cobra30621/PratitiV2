using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class PratitiBattleGrowth
{
    private int pricePonit = 0;
    private int gate = 20;
    //

    public void GainGeneralPoints(Pratiti mine, Pratiti enemy)
    {
        mine.generalPoints += 30;

        gate = 20;
        if (UnityEngine.Random.Range(1, 101) < gate)
        {
            mine.generalPoints += 50;
        }
    }

    public void WinRoller()
    {

    }
}
