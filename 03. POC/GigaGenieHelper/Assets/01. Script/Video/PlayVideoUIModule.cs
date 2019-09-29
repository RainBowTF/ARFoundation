using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayVideoUIModule : PlayVideoModule
{
    public PlayVideoUIModule(PlayVideoTransparent player) : base(player)
    {
        _type = PlayVideoTransparent.PLAYER_VIDEO_TYPE.UI;
    }

    public override void Init()
    {
        base.Init();

        // 매터리얼을 설정한다
        _player.SetMaterial(_player.GetComponent<RawImage>().material);
    }

    // 비디오를 실행한다
    public override void StartVideo()
    {
        base.StartVideo();
        _player.StartCoroutine("PlayVideoResource");
    }

    public override IEnumerator PlayVideoResource()
    {
        DataCenter.GetInstance().SetVideoOriginSize(new Vector3(1.0f, 1.0f, 1.0f));
        DataCenter.GetInstance().SetVideoSize(1.0f);

        _player.GetPlayer().Prepare();
        WaitForSeconds waitTime = new WaitForSeconds(1.0f);

        // 동영상 준비
        while (!_player.GetPlayer().isPrepared)
        {
            yield return waitTime;
        }

        // 로우 이미지의 텍스쳐를 비디오의 텍스쳐로 바꿔준다
        _player.GetComponent<RawImage>().texture = _player.GetPlayer().texture;
        // 동영상 실행
        _player.GetPlayer().Play();

        // 동영상 실행 중...
        while (_player.GetPlayer().isPlaying)
        {
            //_player.GetMaterial().SetFloat("_Transparency", _player.GetVideoController().GetTransparent());
            AdjustVideoLocation();
            yield return null;
        }
    }

    public override void AdjustVideoLocation()
    {
        // 비디오 위치 설정
        //Vector3 position = Vector3.zero;
        //position.Set(_player.GetVideoController().GetHorizontalLoc() * 1000.0f, 0, 0);
        //_player.transform.localPosition = position;

        // 비디오 사이즈 조정
        //_player.transform.localScale = _player.GetVideoController().GetSize();
    }
}
