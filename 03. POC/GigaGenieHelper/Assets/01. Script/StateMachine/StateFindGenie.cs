using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class StateFindGenie : State
{
    GameController _gameController;

    public StateFindGenie(string stateId, GameObject gameObject) : base(stateId, gameObject)
    {
        _sceneNumber = 0;
        _maxSceneNumber = 1;
        _gameController = gameObject.GetComponent<GameController>();
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
                _gameController.EnableArSessionOrigin(true);
                _isWorking = true;
                break;
        }
    }

    public override string GetNextState()
    {
        // 작동이 모두 끝났으면 다음 스테이트를 리턴한다
        if (_isEnd)
        {
            ARPlaneManager planeManager = GameObject.Find("AR Session Origin").GetComponent<ARPlaneManager>();

            // 바닥 표시를 비활성화 시킨다
            foreach (ARPlane plane in planeManager.trackables)
            {
                plane.transform.gameObject.SetActive(false);
            }

            GameObject.Find("AR Session Origin").GetComponent<ARPlaneManager>().enabled = false;
            GameObject.Find("AR Session Origin").GetComponent<ObjectOnPlane>().enabled = false;
            return "LanCable";
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
