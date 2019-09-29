using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    // 상태를 저장하고 있는 딕셔너리
    Dictionary<string, State> _dictState;
    State _currentState;
    GameObject _mainObject;
    string _id;

    // Start is called before the first frame update
    public StateMachine(string id, GameObject mainObject)
    {
        _id = id;
        _mainObject = mainObject;

        // 상태목록을 생성한다
        _dictState = new Dictionary<string, State>();
    }

    public void AddState(string id, State state)
    {
        _dictState.Add(state.GetId(), state);
    }

    public bool Next()
    {
        // 현재 상태가 null이면 return false
        if (_currentState == null)
            return false;

        // 현재 상태의 다음 상태를 받는다
        string stateId = _currentState.GetNextState();

        // stateId 확인
        if( string.IsNullOrEmpty(stateId) )
        {
            return true;
        }

        // 현재 상태가 다음 상태와 다를때 Work
        if (!_currentState.GetId().Equals(stateId))
        {
            _currentState.Init();
            _currentState = _dictState[stateId];
            _currentState.Work();
        }
        return true;
    }

    public void ForceState(string stateId)
    {
        // 현재 상태가 null이면
        if (_currentState == null)
        {
            _currentState = _dictState[stateId];
            _currentState.Work();
        }
        // 현재 상태가 다음 상태와 다를때 Work
        else if (!_currentState.GetId().Equals(stateId))
        {
            _currentState = _dictState[stateId];
            _currentState.Work();
        }
    }

    public void AddSceneNumber()
    {
        _currentState.AddSceneNumber();
    }

    // 스테이트 머신의 id를 가져온다
    public string GetId()
    {
        return _id;
    }

    // 현재 상태를 종료하고 다음 상태로 넘어간다
    public void EndState()
    {
        if(_currentState != null)
        {
            _currentState.EndState();
        }
    }
}
