using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    protected string _id;
    protected GameObject _gameObject;
    protected bool _isEnd;      // 스테이트 종료여부
    protected bool _isWorking;  // Scene의 종료여부 (false : 종료(다른 Scene 실행가능), true : 실행중)

    protected int _sceneNumber = 0;
    protected int _maxSceneNumber = 0;

    public State(string id, GameObject gameObject)
    {
        _id = id;
        _gameObject = gameObject;
    }

    public virtual void Init()
    {

    }

    public virtual void Work()
    {

    }

    public virtual string GetNextState()
    {
        return "";
    }

    // Scene 넘버를 더해준다
    public void AddSceneNumber()
    {
        // 종료상태가 아닐때
        if( !_isEnd )
        {
            // Scene 넘버를 + 1
            _sceneNumber++;

            // Scene의 작동이 끝났다는걸 기록한다
            _isWorking = false;

            // 마지막 Scene 일경우 _isEnd를 false로 만들어 더이상 작동하지 않도록 한다
            if (_sceneNumber >= _maxSceneNumber)
            {
                _isEnd = true;
            }
        }
    }

    public string GetId()
    {
        return _id;
    }

    public virtual void EndState()
    {
        _isEnd = true;
    }
}
