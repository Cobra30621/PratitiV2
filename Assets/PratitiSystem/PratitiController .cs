using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TrainSystem;

public class PratitiController
{
    public Pratiti toControl = new Pratiti();
    public bool randomInitilaize = true;
    public int sexDetermination, characteristic, IV;
    public PratitiBrand pratitiBrand;
    public PratitiData mPratitiData = GameObject.Find("GameData").GetComponent<PratitiData>();

    public int pressure;
    public float hp, atk, def, mAtk, mDef, agi;
    public float maxHp, maxAtk, maxDef, maxMAtk, maxMDef, maxAgi;
    public bool sex;

    public Pratiti RandomData(PratitiBrand pratitiBrand)
    {
        int randomSexDetermination;
        int randomCharacteristic;
        int randomIV;

        randomIV = Random.Range(1, 101);//決定個體值
        randomCharacteristic = Random.Range(1, 22);//決定性格
        randomSexDetermination = Random.Range(1, 101);//決定性別,true爲雄性
        PratitiInitialize(500, pratitiBrand, randomSexDetermination, randomCharacteristic, randomIV);

        return toControl;
    }

    public Pratiti RandomData(PratitiBrand pratitiBrand, int level)
    {
        int randomSexDetermination;
        int randomCharacteristic;
        int randomIV;

        randomIV = Random.Range(1, 101);//決定個體值
        randomCharacteristic = Random.Range(1, 22);//決定性格
        randomSexDetermination = Random.Range(1, 101);//決定性別,true爲雄性
        PratitiInitialize(500, pratitiBrand, randomSexDetermination, randomCharacteristic, randomIV, level);

        return toControl;
    }
    public void PratitiInitialize(int baseStatistic, PratitiBrand pratitiBrand, int sexDetermination, int characteristic, int IV)
    {
        if (sexDetermination < 0)
            sexDetermination = Random.Range(1, 101);
        if (characteristic < 0)
            characteristic = Random.Range(1, 22);
        if (IV < 0)
            IV = Random.Range(1, 101);

        toControl.pratitiName = pratitiBrand.pratitiName.ToString();
        toControl.baseStatistic = baseStatistic;
        toControl.pratitiBrand = pratitiBrand;
        Debug.Log(pratitiBrand.pratitiName.ToString());
        toControl.sexDetermination = sexDetermination;
        toControl.characteristic = characteristic;
        toControl.IV = IV;
        toControl.pressure = 0;
        if (sexDetermination <= pratitiBrand.sexCoefficient)
        {
            toControl.sex = true;
        }
        else
        {
            toControl.sex = false;
        }
        //計算最大能力值；性格修正；基礎能力值
        toControl.maxHp = pratitiBrand.hpBuff * baseStatistic * (IV / 5 + 90) / 100;
        toControl.maxAtk = pratitiBrand.atkBuff * baseStatistic * (IV / 5 + 90) / 100;
        toControl.maxDef = pratitiBrand.defBuff * baseStatistic * (IV / 5 + 90) / 100;
        toControl.maxMAtk = pratitiBrand.matkBuff * baseStatistic * (IV / 5 + 90) / 100;
        toControl.maxMDef = pratitiBrand.mdefBuff * baseStatistic * (IV / 5 + 90) / 100;
        toControl.maxAgi = pratitiBrand.agiBuff * baseStatistic * (IV / 5 + 90) / 100;
        if (characteristic < 5)
        {    //性格值1~4 => atkup
            toControl.maxAtk = toControl.maxAtk * 11 / 10;
            switch (characteristic / 4)
            {
                case 1:
                    toControl.maxDef = toControl.maxDef * 9 / 10;
                    break;
                case 2:
                    toControl.maxMAtk = toControl.maxMAtk * 9 / 10;
                    break;
                case 3:
                    toControl.maxMDef = toControl.maxMDef * 9 / 10;
                    break;
                case 4:
                    toControl.maxAgi = toControl.maxAgi * 9 / 10;
                    break;
            }
        }
        else if (characteristic < 9)
        {  //性格值5~8 => defup；以此類推；性格值==21，則不增減
            toControl.maxDef = toControl.maxDef * 11 / 10;
            switch (characteristic % 4)
            {
                case 1:
                    toControl.maxAtk = toControl.maxAtk * 9 / 10;
                    break;
                case 2:
                    toControl.maxMAtk = toControl.maxMAtk * 9 / 10;
                    break;
                case 3:
                    toControl.maxMDef = toControl.maxMDef * 9 / 10;
                    break;
                case 4:
                    toControl.maxAgi = toControl.maxAgi * 9 / 10;
                    break;
            }
        }
        else if (characteristic < 13)
        {
            toControl.maxMAtk = toControl.maxMAtk * 11 / 10;
            switch (characteristic % 4)
            {
                case 1:
                    toControl.maxAtk = toControl.maxAtk * 9 / 10;
                    break;
                case 2:
                    toControl.maxDef = toControl.maxDef * 9 / 10;
                    break;
                case 3:
                    toControl.maxMDef = toControl.maxMDef * 9 / 10;
                    break;
                case 4:
                    toControl.maxAgi = toControl.maxAgi * 9 / 10;
                    break;
            }
        }
        else if (characteristic < 17)
        {
            toControl.maxMDef = toControl.maxMDef * 11 / 10;
            switch (characteristic % 4)
            {
                case 1:
                    toControl.maxAtk = toControl.maxAtk * 9 / 10;
                    break;
                case 2:
                    toControl.maxDef = toControl.maxDef * 9 / 10;
                    break;
                case 3:
                    toControl.maxMAtk = toControl.maxMAtk * 9 / 10;
                    break;
                case 4:
                    toControl.maxAgi = toControl.maxAgi * 9 / 10;
                    break;
            }
        }
        else if (characteristic < 21)
        {     //性格值21不進行修正
            toControl.maxAgi = toControl.maxAgi * 11 / 10;
            switch (characteristic % 4)
            {
                case 1:
                    toControl.maxAtk = toControl.maxAtk * 9 / 10;
                    break;
                case 2:
                    toControl.maxDef = toControl.maxDef * 9 / 10;
                    break;
                case 3:
                    toControl.maxMAtk = toControl.maxMAtk * 9 / 10;
                    break;
                case 4:
                    toControl.maxMDef = toControl.maxMDef * 9 / 10;
                    break;
            }
        }
        toControl.hp = toControl.maxHp / 20;
        toControl.atk = toControl.maxAtk / 20;
        toControl.def = toControl.maxDef / 20;
        toControl.mAtk = toControl.maxMAtk / 20;
        toControl.mDef = toControl.maxMDef / 20;
        toControl.agi = toControl.maxAgi / 20;
        toControl.index = ++Pratiti.counter;
    }
    

    //用於控制帕拉緹緹初始強度
    public void PratitiInitialize(int baseStatistic, PratitiBrand pratitiBrand, int sexDetermination, int characteristic, int IV, int level)
    {
        toControl.pratitiName = pratitiBrand.pratitiName.ToString();
        toControl.baseStatistic = baseStatistic;
        toControl.pratitiBrand = pratitiBrand;
        Debug.Log(pratitiBrand.pratitiName.ToString());
        toControl.sexDetermination = sexDetermination;
        toControl.characteristic = characteristic;
        toControl.IV = IV;
        toControl.pressure = 0;
        if (sexDetermination <= pratitiBrand.sexCoefficient)
        {
            toControl.sex = true;
        }
        else
        {
            toControl.sex = false;
        }
        //計算最大能力值；性格修正；基礎能力值
        toControl.maxHp = pratitiBrand.hpBuff * baseStatistic * (IV / 5 + 90) / 100;
        toControl.maxAtk = pratitiBrand.atkBuff * baseStatistic * (IV / 5 + 90) / 100;
        toControl.maxDef = pratitiBrand.defBuff * baseStatistic * (IV / 5 + 90) / 100;
        toControl.maxMAtk = pratitiBrand.matkBuff * baseStatistic * (IV / 5 + 90) / 100;
        toControl.maxMDef = pratitiBrand.mdefBuff * baseStatistic * (IV / 5 + 90) / 100;
        toControl.maxAgi = pratitiBrand.agiBuff * baseStatistic * (IV / 5 + 90) / 100;
        if (characteristic < 5)
        {    //性格值1~4 => atkup
            toControl.maxAtk = toControl.maxAtk * 11 / 10;
            switch (characteristic / 4)
            {
                case 1:
                    toControl.maxDef = toControl.maxDef * 9 / 10;
                    break;
                case 2:
                    toControl.maxMAtk = toControl.maxMAtk * 9 / 10;
                    break;
                case 3:
                    toControl.maxMDef = toControl.maxMDef * 9 / 10;
                    break;
                case 4:
                    toControl.maxAgi = toControl.maxAgi * 9 / 10;
                    break;
            }
        }
        else if (characteristic < 9)
        {  //性格值5~8 => defup；以此類推；性格值==21，則不增減
            toControl.maxDef = toControl.maxDef * 11 / 10;
            switch (characteristic % 4)
            {
                case 1:
                    toControl.maxAtk = toControl.maxAtk * 9 / 10;
                    break;
                case 2:
                    toControl.maxMAtk = toControl.maxMAtk * 9 / 10;
                    break;
                case 3:
                    toControl.maxMDef = toControl.maxMDef * 9 / 10;
                    break;
                case 4:
                    toControl.maxAgi = toControl.maxAgi * 9 / 10;
                    break;
            }
        }
        else if (characteristic < 13)
        {
            toControl.maxMAtk = toControl.maxMAtk * 11 / 10;
            switch (characteristic % 4)
            {
                case 1:
                    toControl.maxAtk = toControl.maxAtk * 9 / 10;
                    break;
                case 2:
                    toControl.maxDef = toControl.maxDef * 9 / 10;
                    break;
                case 3:
                    toControl.maxMDef = toControl.maxMDef * 9 / 10;
                    break;
                case 4:
                    toControl.maxAgi = toControl.maxAgi * 9 / 10;
                    break;
            }
        }
        else if (characteristic < 17)
        {
            toControl.maxMDef = toControl.maxMDef * 11 / 10;
            switch (characteristic % 4)
            {
                case 1:
                    toControl.maxAtk = toControl.maxAtk * 9 / 10;
                    break;
                case 2:
                    toControl.maxDef = toControl.maxDef * 9 / 10;
                    break;
                case 3:
                    toControl.maxMAtk = toControl.maxMAtk * 9 / 10;
                    break;
                case 4:
                    toControl.maxAgi = toControl.maxAgi * 9 / 10;
                    break;
            }
        }
        else if (characteristic < 21)
        {     //性格值21不進行修正
            toControl.maxAgi = toControl.maxAgi * 11 / 10;
            switch (characteristic % 4)
            {
                case 1:
                    toControl.maxAtk = toControl.maxAtk * 9 / 10;
                    break;
                case 2:
                    toControl.maxDef = toControl.maxDef * 9 / 10;
                    break;
                case 3:
                    toControl.maxMAtk = toControl.maxMAtk * 9 / 10;
                    break;
                case 4:
                    toControl.maxMDef = toControl.maxMDef * 9 / 10;
                    break;
            }
        }
        toControl.hp = toControl.maxHp * (1 + 19 * level / 100) / 20;
        toControl.atk = toControl.maxAtk * (1 + 19 * level / 100) / 20;
        toControl.def = toControl.maxDef * (1 + 19 * level / 100) / 20;
        toControl.mAtk = toControl.maxMAtk * (1 + 19 * level / 100) / 20;
        toControl.mDef = toControl.maxMDef * (1 + 19 * level / 100) / 20;
        toControl.agi = toControl.maxAgi * (1 + 19 * level / 100) / 20;
        toControl.index = ++Pratiti.counter;
    }
}
