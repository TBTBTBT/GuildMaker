using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class MstQuestData
{
    [Serializable]
    public class DataFormat
    {
        public int Rank;
        public List<Reward> Reward;
        public int Charge;//契約金
        public int Week; //掲載期間
        public string Name;//クエスト名
        //public List<Monster> Target;//モンスターの種類と数 
    }
    public class Reward{
        public int Id;
        public int Num;
    }
    public List<DataFormat> Data = new List<DataFormat>();


}