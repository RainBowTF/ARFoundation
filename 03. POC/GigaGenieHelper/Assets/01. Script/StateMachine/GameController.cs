using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class GameController : MonoBehaviour
{
    StateMachine _machine;
    GameObject _arSessionOrigin;

    // Start is called before the first frame update
    void Start()
    {
        // AR Disable
        _arSessionOrigin = GameObject.Find("AR Session Origin");
        _arSessionOrigin.SetActive(false);

        // 스테이트 머신 설정
        StateMachine machine = new StateMachine("GameController", this.gameObject);

        State state = new StateFirst("FirstScene", this.gameObject); // 초기 앱 상태
        machine.AddState("FirstScene", state);

        state = new StateFindGenie("FindGenie", this.gameObject); // 지니 탐색 상태
        machine.AddState("FindGenie", state);

        state = new StateLanCable("LanCable", this.gameObject); // 1. 랜선꼽기
        machine.AddState("LanCable", state);

        state = new StateHDMICable("HDMICable", this.gameObject); // 2. HDMI 케이블 꼽기
        machine.AddState("HDMICable", state);

        state = new StatePowerCable("PowerCable", this.gameObject); // 3. 파워 케이블 꼽기
        machine.AddState("PowerCable", state);

        state = new StatePowerOn("PowerOn", this.gameObject); // 4. 파워 온 
        machine.AddState("PowerOn", state);

        state = new StateSelfIdentify("SelfIdentify", this.gameObject); // 5. 본인인증
        machine.AddState("SelfIdentify", state);

        state = new StateRemoteControl("RemoteControl", this.gameObject); // 6. 리모컨 설정
        machine.AddState("RemoteControl", state);

        state = new StateEnd("End", this.gameObject); // 7. 종료
        machine.AddState("End", state);

        state = new State("Nothing", this.gameObject); // 8. 무반응상태
        machine.AddState("Nothing", state);

        DataCenter.GetInstance().AddStateMachine(machine);
        _machine = machine;
    }

    // Update is called once per frame
    void Update()
    {
        if(_machine.Next() == false)
        {
            _machine.ForceState("FirstScene");
        }
    }

    public void EnableArSessionOrigin(bool isEnable)
    {
        if(isEnable)
        {
            GameObject.FindGameObjectWithTag("SubCamera").SetActive(false);
        }
        else
        {
            GameObject.FindGameObjectWithTag("SubCamera").SetActive(true);
        }
        _arSessionOrigin.SetActive(isEnable);
    }

    public void ReplayVideo()
    {
        PlayVideoTransparent _player = GameObject.FindGameObjectWithTag("Video").GetComponent<PlayVideoTransparent>();

        // 기존 비디오를 정지시킨다
        _player.StopVideo();

        // 비디오를 실행시킨다
        _player.StartVideo();
    }

    public void EndState()
    {
        DataCenter.GetInstance().GetStateMachine("GameController").EndState();
    }
}
