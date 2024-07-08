using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public static class GoogleSheetManager
{
    public static List<UnitData> unitDatas = new List<UnitData>();

    // 링크 뒤 edit ~ 부분을 빼고 export?format=tsv 추가하기
    // origin url https://docs.google.com/spreadsheets/d/1Z_awcApNODEqu---j7VFGDjjuyU5cl7IMinMXKHPII/edit?gid=0#gid=0
    private const string unitDataURL = "https://docs.google.com/spreadsheets/d/1Z_awcApNODEqu---j7VFGDjjuyU5cl7IMinMXKHPII/export?format=tsv"; 
    public static IEnumerator Loader()
    {
        UnityWebRequest www = UnityWebRequest.Get(unitDataURL);
        yield return www.SendWebRequest();

        string data = www.downloadHandler.text;
        Debug.Log(data);

        ParseUnitData(data);
    }

    
    private static void ParseUnitData(string data)
    {
        string[] lines = data.Split('\n');
        for (int i = 1; i < lines.Length; i++) // first line is data type
        {
            string[] fields = lines[i].Split('\t');
            if (fields.Length >= 13) // enough field checking 
            {
                UnitData unitData = new UnitData()
                {
                    Index = int.Parse(fields[0]),
                    Name = fields[1],
                    Grade = int.Parse(fields[2]),
                    HP = float.Parse(fields[3]),
                    MP = float.Parse(fields[4]),
                    AttackPower = float.Parse(fields[5]),
                    Defense = float.Parse(fields[6]),
                    AttackSpeed = float.Parse(fields[7]),
                    MoveSpeed = float.Parse(fields[8]),
                    Class = int.Parse(fields[9]),
                    AttackRange = float.Parse(fields[10]),
                    Cost = int.Parse(fields[11]),
                    KRName = fields[12],
                    DesText = fields[13],
                };
                unitDatas.Add(unitData);
            }
        }
    }
}

[System.Serializable]
public class UnitData
{
    public int Index;
    public string Name;
    public int Grade;
    public float HP;
    public float MP;
    public float AttackPower;
    public float Defense;
    public float AttackSpeed;
    public float MoveSpeed;
    public int Class;
    public float AttackRange;
    public int Cost;
    public string KRName;
    public string DesText;
}

[System.Serializable]
public class CommanderData
{

}