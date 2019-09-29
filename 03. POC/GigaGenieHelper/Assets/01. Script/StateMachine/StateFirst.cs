using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateFirst : State
{
    TextController textCtrl;

    public StateFirst(string stateId, GameObject gameObject) : base(stateId, gameObject)
    {
        _sceneNumber = 0;
        _maxSceneNumber = 2;
        textCtrl = _gameObject.GetComponent<TextController>();
    }

    public override void Init()
    {
        _sceneNumber = 0;
        _maxSceneNumber = 2;
        _isEnd = false;
        _isWorking = false;
    }

    public override void Work()
    {
        switch(_sceneNumber)
        {
            case 0:
                textCtrl.SetText("안녕하세요" + System.Environment.NewLine + "기가지니를 구매해 주셔서" + System.Environment.NewLine + "감사합니다",
                    2.0f,
                    2.0f,
                    Color.black
                    );
                _isWorking = true;
                break;
            case 1:
                textCtrl.SetText("기가지니를 설치하고 싶은" + System.Environment.NewLine + "위치를 터치해주세요",
                    2.0f,
                    2.0f,
                    Color.black
                    );
                _isWorking = true;
                break;
        }
    }

    public override string GetNextState()
    {
        // 작동이 모두 끝났으면 다음 스테이트를 리턴한다
        if( _isEnd )
        {
            return "FindGenie";
        }
        // 작동이 모두 끝나지 않았으면 현재 스테이트를 리턴한다
        else
        {
            if( _isWorking == false )
            {
                Work();
            }
            return _id;
        }
    }
}
