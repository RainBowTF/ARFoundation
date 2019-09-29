using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateRemoteControl : State
{
    TextController _textCtrl;
    GenieController _genieController;

    public StateRemoteControl(string stateId, GameObject gameObject) : base(stateId, gameObject)
    {
        _sceneNumber = 0;
        _maxSceneNumber = 2;
        _textCtrl = _gameObject.GetComponent<TextController>();
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
        switch (_sceneNumber)
        {
            case 0:
                _genieController = GameObject.FindGameObjectWithTag("Genie").GetComponent<GenieController>(); // 지니 오브젝트의 컨트롤러를 가져온다
                DataCenter.GetInstance().GetVideoPanel().transform.Find("VideoText").GetComponent<Text>().text = "6. 리모컨 설정하기";

                _textCtrl.SetText("6. 리모컨의 홈메뉴 버튼과 확인 버튼을 3초간 눌러주세요",
                    2.0f,
                    2.0f,
                    Color.white
                    );
                _isWorking = true;
                break;
            case 1: // 전원케이블 결합영상을 보여준다
                _isWorking = true;
                GameObject videoPanel = DataCenter.GetInstance().GetVideoPanel();
                videoPanel.SetActive(true); // 비디오 패널을 활성화 한다

                PlayVideoTransparent _player = GameObject.FindGameObjectWithTag("Video").GetComponent<PlayVideoTransparent>();

                // 비디오 리소스를 지정한다
                try
                {
                    _player.SetVideoResource("RemoteControl");
                }
                catch (Exception e)
                {
                    DataCenter.GetInstance().AddDebugString(e.StackTrace);
                }

                // 비디오를 실행시킨다
                _player.StartVideo();

                break;
        }
    }

    public override string GetNextState()
    {
        // 작동이 모두 끝났으면 다음 스테이트를 리턴한다
        if (_isEnd)
        {
            // 기존 비디오를 inActive 시킨다
            PlayVideoTransparent _player = GameObject.FindGameObjectWithTag("Video").GetComponent<PlayVideoTransparent>();
            _player.StopVideo();

            GameObject videoPanel = DataCenter.GetInstance().GetVideoPanel();
            videoPanel.SetActive(false); // 비디오 패널을 비활성화 시킨다

            return "End";
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
