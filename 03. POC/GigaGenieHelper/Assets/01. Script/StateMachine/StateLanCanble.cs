using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateLanCable : State
{
    TextController _textCtrl;
    GenieController _genieController;

    public StateLanCable(string stateId, GameObject gameObject) : base(stateId, gameObject)
    {
        _sceneNumber = 0;
        _maxSceneNumber = 4;
        _textCtrl = _gameObject.GetComponent<TextController>();
    }

    public override void Init()
    {
        _sceneNumber = 0;
        _maxSceneNumber = 4;
        _isEnd = false;
        _isWorking = false;
    }

    public override void Work()
    {
        switch (_sceneNumber)
        {
            case 0:
                _genieController = GameObject.FindGameObjectWithTag("Genie").GetComponent<GenieController>(); // 지니 오브젝트의 컨트롤러를 가져온다

                _textCtrl.SetText("이제 스와이프하여 뒷쪽을 봐주세요",
                    2.0f,
                    2.0f,
                    Color.white
                    );
                _isWorking = true;
                break;
            case 1: // 뒷면이 보이는지 확인한다
                if (_genieController.CheckAngle("BACK"))
                {
                    DataCenter.GetInstance().GetStateMachine("GameController").AddSceneNumber();
                }
                _isWorking = false; // 계속해서 호출되어야 되기 때문에 false로 해준다
                break;
            case 2:
                _genieController = GameObject.FindGameObjectWithTag("Genie").GetComponent<GenieController>(); // 지니 오브젝트의 컨트롤러를 가져온다

                _textCtrl.SetText("1. LAN 케이블을 LAN포트에 꼽아주세요",
                    2.0f,
                    2.0f,
                    Color.white
                    );
                _isWorking = true;
                break;
            case 3: // 랜케이블 결합영상을 보여준다
                _isWorking = true;
                GameObject videoPanel = DataCenter.GetInstance().GetVideoPanel();
                videoPanel.SetActive(true); // 비디오 패널을 활성화 한다

                PlayVideoTransparent _player = GameObject.FindGameObjectWithTag("Video").GetComponent<PlayVideoTransparent>();

                // 비디오 리소스를 지정한다
                try
                {
                    _player.SetVideoResource("LanCable");
                }catch( Exception e )
                {
                    DataCenter.GetInstance().AddDebugString(e.StackTrace);
                }

                // 비디오를 실행시킨다
                _player.StartVideo();

                // 체크박스를 활성화 시킨다
                _genieController.ActivateCheck("LanCable");

                break;
        }
    }

    public override string GetNextState()
    {
        // 작동이 모두 끝났으면 다음 스테이트를 리턴한다
        if (_isEnd)
        {
            // 체크박스를 비활성화 한다
            _genieController.InactivateCheck();

            // 기존 비디오를 inActive 시킨다
            PlayVideoTransparent _player = GameObject.FindGameObjectWithTag("Video").GetComponent<PlayVideoTransparent>();
            _player.StopVideo();

            GameObject videoPanel = DataCenter.GetInstance().GetVideoPanel();
            videoPanel.SetActive(false); // 비디오 패널을 비활성화 시킨다

            return "HDMICable";
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
