using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateEnd : State
{
    TextController _textCtrl;
    GenieController _genieController;

    public StateEnd(string stateId, GameObject gameObject) : base(stateId, gameObject)
    {
        _sceneNumber = 0;
        _maxSceneNumber = 1;
        _textCtrl = _gameObject.GetComponent<TextController>();
    }

    public override void Init()
    {
        _sceneNumber = 0;
        _maxSceneNumber = 1;
        _isEnd = false;
        _isWorking = false;
    }

    public override void Work()
    {
        switch (_sceneNumber)
        {
            case 0:
                _textCtrl.SetText("즐거운 컨텐츠 여행 되세요" + System.Environment.NewLine + "감사합니다",
                    2.0f,
                    2.0f,
                    Color.white
                    );
                _isWorking = true;
                break;
        }
    }

    public override string GetNextState()
    {
        // 작동이 모두 끝났으면 다음 스테이트를 리턴한다
        if (_isEnd)
        {
            return "Nothing";
        }
        // 작동이 모두 끝나지 않았으면 현재 스테이트를 리턴한다
        else
        {
            if (_isWorking == false)
            {
                Work();
            }
            return _id;
        }
    }
}
