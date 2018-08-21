using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleDialog : Dialog
{
    [SerializeField] private Animator _anim;
    [SerializeField] private Text _title;
    [SerializeField] private Text _message;
    [SerializeField] private Image _icon;
    public override void Open(DialogSettings settings)
    {
        base.Open(settings);
        
        SetIcon();
        SetTitle();
        SetMessage();
        _anim?.SetTrigger("Open");
    }

    public virtual void Close()
    {

        _anim?.SetTrigger("Close");
    }

    void SetTitle()
    {

        if (SetText(_title,_settings.Title))
        {
            
        }
    }
    void SetMessage()
    {
        if (SetText(_message, _settings.Message))
        {
            //icon次第では移動
        }
    }

    bool SetText(Text text , string str)
    {
        if (str == "")
        {
            return false;
        }

        text.text = str;
        return true;
    }
    void SetIcon()
    {
        if (_settings.Icon == null)
        {
            return;
        }

        _icon.sprite = _settings.Icon;
    }


}
