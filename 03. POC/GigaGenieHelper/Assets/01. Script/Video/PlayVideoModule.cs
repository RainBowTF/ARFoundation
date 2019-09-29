using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayVideoModule
{
    protected PlayVideoTransparent _player;
    protected bool _autoStart = false;      // 동영상 자동시작 옵션
    protected PlayVideoTransparent.PLAYER_VIDEO_TYPE _type;

    public PlayVideoModule(PlayVideoTransparent player)
    {
        _player = player;
    }

    // 초기화
    virtual public void Init()
    {
        // 자동실행 옵션이 설정되어 있으면 초기화와 동시에 비디오를 실행한다
        if (_player.GetPlayOnStart())
        {
            StartVideo();
        }
    }

    // 비디오를 실행 시킨다
    virtual public void StartVideo()
    {

    }

    // 비디오를 실행 시킨다
    virtual public IEnumerator PlayVideoResource()
    {
        return null;
    }

    // 비디오의 위치를 결정한다
    virtual public void AdjustVideoLocation()
    {

    }

    virtual public void AdjustVideoLocation(Transform transform)
    {

    }

    public PlayVideoTransparent.PLAYER_VIDEO_TYPE GetModuleType()
    {
        return _type;
    }
}
