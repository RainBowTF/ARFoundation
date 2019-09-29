using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 설정값을 글로벌로 임시 저장하는 객체
public class DataCenter
{
    private static DataCenter instance = new DataCenter();

    public float _transparentValue;                  // 동영상 투명도 0~1
    public float _videoDistance;                     // 카메라와 비디오 사이의 거리
    private Vector3 _videoOriginSize;
    public Vector3 _videoSize;
    public float _videoHorizontalLoc;

    public List<string> _debug;
    Text _debugText;
    int _counter = 0;

    Dictionary<string, StateMachine> _dictStateMachine;
    GameObject _videoPanel;

    private DataCenter()
    {
        _dictStateMachine = new Dictionary<string, StateMachine>();

        _transparentValue = 0.7f;
        _videoDistance = 3.0f;
        _videoOriginSize = new Vector3();
        _videoOriginSize.Set(0.128f, 0.0f, 0.072f);
        _videoSize = _videoOriginSize;
        _videoHorizontalLoc = 0.0f;

        // 비디오 패널
        _videoPanel = GameObject.Find("VideoPanel");
        _videoPanel.SetActive(false);   // 비활성화

        _debug = new List<string>();
        try
        {
            _debugText = GameObject.Find("DebugText").GetComponent<Text>();
        }
        catch
        {
            _debugText = null;
        }
    }

    public static DataCenter GetInstance()
    {
        return instance;
    }

    public void SetTransparent(float value)
    {
        _transparentValue = value;
    }

    public void SetVideoSize(float value)
    {
        _videoSize = _videoOriginSize * value;
    }

    public void SetVideoDistance(float value)
    {
        _videoDistance = value;
    }

    public void SetVideoHorizontalLoc(float value)
    {
        _videoHorizontalLoc = value;
    }

    public void SetVideoOriginSize(Vector3 value)
    {
        _videoOriginSize = value;
    }

    public Vector3 GetVideoOriginSize()
    {
        return _videoOriginSize;
    }

    public void AddDebugString(string str)
    {
        if (_debugText != null && str != null)
        {
            _counter++;
            if(_counter++ > 9)
            {
                _debugText.text = "";
                _counter = 0;
            }

            _debugText.text = _debugText.text + System.Environment.NewLine + str;
        }
    }

    private string GetDebugString(int step)
    {
        string str = "";
        for (int i = 0; i < _debug.Count; i++)
        {
            str += _debug[i];
            str += System.Environment.NewLine;
        }
        return str;
    }

    // 스테이트 머신을 추가한다
    public void AddStateMachine(StateMachine _stateMachine)
    {
        _dictStateMachine.Add(_stateMachine.GetId(), _stateMachine);
    }

    // 스테이트 머신을 가져온다
    public StateMachine GetStateMachine(string id)
    {
        return _dictStateMachine[id];
    }

    public GameObject GetVideoPanel()
    {
        return _videoPanel;
    }
}
