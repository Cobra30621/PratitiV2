using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

[System.Serializable]
public class PratitiData : MonoBehaviour 
{
    public List<Pratiti> pratitiData = new List<Pratiti>();

    static JsonSerializerSettings setting = new JsonSerializerSettings();
    static string PratitiDataPath;

    public static PratitiBrand feather = new PratitiBrand(PratitiBrand.PratitiName.羽毛, 1.1f, 1.1f, 1f, 1f, 1f, 1f, 60);
    public static PratitiBrand pig = new PratitiBrand(PratitiBrand.PratitiName.山豬, 1.1f, 1.1f, 1.1f, 1f, 1f, 1f, 40);

    public void Awake()
    {
        pratitiData.Clear();
        PratitiDataPath = @"Assets/PratitiData/PratitiDataJsonInfo.txt";
        //LoadPratitiData();
        
        PratitiController pC = new PratitiController();
        PratitiController pC2 = new PratitiController();

        pC.RandomData(pig);
        pratitiData.Add(pC.toControl);
        pC2.RandomData(feather);
        pratitiData.Add(pC2.toControl);

        SavePratitiData(pratitiData);   //存檔方式
        //LoadPratitiData();          //讀檔方式
        
    }

    static public void SavePratitiData(List<Pratiti> pratitiData)
    {
        //string pratitiDataJsonInfo = JsonUtility.ToJson(new SerializationPratiti<Pratiti>(pratitiData));   //將PratitiData陣列轉化爲Json形式；
        //string pratitiDataJsonInfo = JsonUtility.ToJson(pratitiData);
        string pratitiDataJsonInfo = JsonConvert.SerializeObject(pratitiData , setting);

        Regex reg = new Regex(@"(?i)\\[uU]([0-9a-f]{4})");
        pratitiDataJsonInfo = reg.Replace(pratitiDataJsonInfo, delegate (Match m) { return ((char)Convert.ToInt32(m.Groups[1].Value, 16)).ToString(); });
            
        if (!Directory.Exists(@"Assets/PratitiData/")) 
        {
        Directory.CreateDirectory(@"Assets/PratitiData/");
        }

        FileStream pratitiDataFile = new FileStream(PratitiDataPath,FileMode.Create);
        StreamWriter sw = new StreamWriter(pratitiDataFile);
        sw.Write(pratitiDataJsonInfo);  //將PratitiData的Json形式寫入PratitiDataPath路徑下的PratitiDataJsonInfo.txt中；
        sw.Close();
        Debug.Log(pratitiDataJsonInfo);
    }

    public void LoadPratitiData()
    {
        string pratitiDataJsonInfo = "";
        StreamReader sr = new StreamReader(PratitiDataPath);
        pratitiDataJsonInfo = sr.ReadToEnd();
        sr.Close();
        if (pratitiDataJsonInfo == null)
        {
            Debug.Log("null");
            return;
        }
        else
        {
            Debug.Log(pratitiDataJsonInfo);
        }

        pratitiData.Clear();
        //pratitiData = JsonUtility.FromJson<List<Pratiti>>(pratitiDataJsonInfo);
        //pratitiData = JsonUtility.FromJson<SerializationPratiti<Pratiti>>(pratitiDataJsonInfo).PratitiToList();
        pratitiData = JsonConvert.DeserializeObject<List<Pratiti>>(pratitiDataJsonInfo , setting);
    }
}






