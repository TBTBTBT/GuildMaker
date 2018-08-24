using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ユーザーが変更できないデータ
/// </summary>
public class MasterDataManager : SingletonMonoBehaviour<MasterDataManager>
{
    
    private JsonDatabase _master = new JsonDatabase();
    public JsonDatabase Get => _master;
    protected override void Awake()
    {
        base.Awake();
        _master.Init();
    }

    public static T Load<T>(string path) where T:new()
    {
        return JsonAccessor.ReadFile<T>(path);
    }
}

public class JsonDatabase
{
    public QuestData Quest { get; set; }

    public void Init()
    {
        Quest = JsonAccessor.ReadFile<QuestData>("Master/mst_quest.json");
    }
#if UNITY_EDITOR
    public void Save()
    {
        JsonAccessor.SaveFile("Master/mst_quest.json",Quest);
    }
#endif

}
[Serializable]
public class QuestData
{
    [Serializable]
    public class DataFormat
    {
        public int id;
    }

    public List<DataFormat> Data = new List<DataFormat>();


}