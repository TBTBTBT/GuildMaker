using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : SingletonMonoBehaviour<DialogManager> {
    public enum State
    {
        Init,
        Wait,
        Open,
        Close
    }

    private Statemachine<State> _statemachine;
    [SerializeField] private GameObject _dialogPrefab;
    public Dialog AppDialog { get; set; }
    public State Current => _statemachine.GetCurrentState();
    protected override void Awake()
    {
        base.Awake();
        _statemachine = new Statemachine<State>();
        _statemachine.Init(this);
    }

    void Update()
    {
        _statemachine.Update();
        
    }
    IEnumerator Init()
    {
        InstantiateDialog();
        _statemachine.Next(State.Wait);
        yield return null;
    }


    IEnumerator Close()
    {
        _statemachine.Next(State.Wait);
        yield return null;
    }
    public void Open(DialogSettings settings)
    {
        AppDialog.Open(settings);
        _statemachine.Next(State.Open);
    }

    public void Close(bool immidiate)
    {
        AppDialog.Close();
        _statemachine.Next(State.Close);
    }
    void InstantiateDialog()
    {
        var dialog = Instantiate(_dialogPrefab,this.transform).GetComponent<Dialog>();
        AppDialog = dialog;
    }
}
