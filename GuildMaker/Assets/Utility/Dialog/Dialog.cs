using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Dialog : MonoBehaviour
{
    protected DialogSettings _settings;

    public virtual void Open(DialogSettings settings)
    {
        _settings = settings;
    }
    public virtual void Close() {}

}

public class DialogSettings
{
    public string Title { get; set; }
    public string Message { get; set; }
    public Sprite Icon { get; set; }
    public string Ok { get; set; }
    public string Cancel { get; set; }
}