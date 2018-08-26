using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;
using System.IO;
/// <summary>
/// JsonUtilを用いてJsonファイルを取り扱う
/// 取り扱えない型など
/// https://docs.unity3d.com/jp/current/Manual/JSONSerialization.html
/// 
/// </summary>
public class JsonAccessor {
    /// <summary>
    /// Asset配下から読み込む
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="resourcePath"> ./Asset/ 以下のパス</param>
    /// <returns></returns>
    public static T ReadFile<T>(string path) where T : new()
    {
        var fullPath = Application.dataPath + "/" + path;
        if (!File.Exists(fullPath))
        {
            Debug.LogWarning("File not exists at "+fullPath);
            return new T();
        }

        var json = File.ReadAllText(fullPath);
        if (json.Length <= 0)
        {
            return new T();
        }
        Debug.Log("read at " + fullPath);
        return JsonUtility.FromJson<T>(json);
    }
    /// <summary>
    /// Asset配下に書き出し
    /// </summary>
    /// <param name="path"></param>
    /// <param name="obj"></param>
    /// <param name="createIfNotExist"></param>
    public static void SaveFile(string path, object obj,bool createIfNotExist = true)
    {
        var fullPath = Application.dataPath + "/" + path;
        var json = JsonUtility.ToJson(obj);
        if (!File.Exists(fullPath))
        {
            if (createIfNotExist)
            {
                File.WriteAllText(fullPath, json);
                Debug.Log("create new file at " + fullPath);
            }

            return;
        }
        Debug.Log("save at " + fullPath);
        File.WriteAllText(fullPath, json);

    }
}
