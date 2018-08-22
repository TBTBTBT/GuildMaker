using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogViewer : SingletonMonoBehaviour<LogViewer>
{
    private List<string> _log = new List<string>();
    [SerializeField]
    private Text _text;
    [SerializeField] private int _max = 100;
    [SerializeField] private int _row = 4;
    public void Add(string str)
    {
        _log.Insert(0,str);
        UpdateView();
    }

    void UpdateView()
    {
        while (_log.Count > _max)
        {
            _log.RemoveAt(_log.Count - 1);
        }
        if(_text == null)
        {
            return;
            
        }
        _text.text = "";
        int count = 0;
        foreach (var s in _log)
        {
            _text.text += s + "\n";
            count++;
            if (count >= _row)
            {
                break;
            }
        }

    }
}
