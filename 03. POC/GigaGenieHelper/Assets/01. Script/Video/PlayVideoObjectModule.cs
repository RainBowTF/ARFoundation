using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayVideoObjectModule : PlayVideoModule
{
    public PlayVideoObjectModule(PlayVideoTransparent player) : base(player)
    {
        _type = PlayVideoTransparent.PLAYER_VIDEO_TYPE.OBJECT;
    }

    public override void StartVideo()
    {
        base.StartVideo();
        _player.StartCoroutine("PlayVideoResource");
    }

    public override IEnumerator PlayVideoResource()
    {
        WaitForSeconds waitTime = new WaitForSeconds(1.0f);
        while(_player.GetPlayer() == null)
        {
            yield return waitTime;
        }

        _player.GetPlayer().Prepare();

        while (!_player.GetPlayer().isPrepared)
        {
            yield return waitTime;
        }

        _player.GetPlayer().Play();

        while (_player.GetPlayer().isPlaying)
        {
            _player.GetMaterial().SetFloat("_Transparency", _player.GetVideoController().GetTransparent());
            AdjustVideoLocation();
            yield return null;
        }
    }

    public override void AdjustVideoLocation()
    {
        // 비디오 위치 설정
        Vector3 cameraFrontVector = _player.GetCamera().transform.forward;
        Vector3 horizontalVector = Quaternion.Euler(0, 90, 0) * cameraFrontVector * _player.GetVideoController().GetHorizontalLoc();    // 좌우 벡터
        Vector3 videoLocation = (_player.GetCamera().transform.position + horizontalVector) + cameraFrontVector * _player.GetVideoController().GetDistance();

        _player.transform.position = videoLocation;

        // 비디오 각도 설정
        _player.transform.LookAt(videoLocation - cameraFrontVector * 10.0f);
        _player.transform.rotation *= Quaternion.Euler(180, 0, 0);

        // 비디오 사이즈 조정
        _player.transform.localScale = _player.GetVideoController().GetSize();
    }
}
