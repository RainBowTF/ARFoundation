using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class PlayVideoTransparent : MonoBehaviour
{
    // VideoModule
    [SerializeField] bool _playerOnStart = false;
    public enum PLAYER_VIDEO_TYPE {UI, OBJECT, OBJECT_TRACKING}    // 비디오 플레이 타입
    [SerializeField] PLAYER_VIDEO_TYPE _type = PLAYER_VIDEO_TYPE.OBJECT;     // 기본값은 오브젝트 방식
    PlayVideoModule _playVideoModule;                           // 비디오 플레이 모듈

    //
    VideoPlayer _player;
    Material _material;
    VideoController _controller;

    //
    Camera _camera;
    Quaternion _rotationOrigin;

    //
    [SerializeField] string videoName = "";

    private void Awake()
    {
        // 비디오 실행 타입에 따라 오브젝트를 실행 모듈을 지정한다
        if(_type == PLAYER_VIDEO_TYPE.UI)
        { 
            _playVideoModule = new PlayVideoUIModule(this);
        }
        else
        {
            _type = PLAYER_VIDEO_TYPE.OBJECT;
            _playVideoModule = new PlayVideoObjectModule(this);
        }
    }

    void Start()
    {
        _player = this.GetComponent<VideoPlayer>();
        _material = this.GetComponent<Renderer>().sharedMaterial;   // 일반 Object용
        _camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        _rotationOrigin = transform.rotation;
        _player.playOnAwake = false;

        // 각 type별로 Init 하는 방식이 다르다
        _playVideoModule.Init();
    }

    void Update()
    {

    }

    // 비디오를 정지 시킨다
    public void StopVideo()
    {
        _player.Stop();
    }

    public void StartVideo()
    {
        _playVideoModule.StartVideo();
    }

    public IEnumerator PlayVideoResource()
    {
        return _playVideoModule.PlayVideoResource();
    }

    public void AdjustVideoLocation(Transform transform)
    {
        _playVideoModule.AdjustVideoLocation(transform);
    }

    public void SetVideoResource(string str)
    {
        _player = this.GetComponent<VideoPlayer>();
        try
        {
            _player.Stop();
        }
        catch
        {
            // do nothing
        }
        VideoClip clip = Resources.Load<VideoClip>(str);
        _player.clip = clip;
    }

    // 비디오를 보여줄 Material을 결정
    public void SetMaterial(Material material)
    {
        _material = material;
    }

    public Material GetMaterial()
    {
        return _material;
    }

    // 비디오 실행
    public bool GetPlayOnStart()
    {
        return _playerOnStart;
    }

    // 오브젝트 로드와 동시에 실행할 것인지 결정
    public void SetPlayOnStart(bool value)
    {
        if( value )
        {
            _playerOnStart = true;
        }
        else
        {
            _playerOnStart = false;
        }
    }

    public VideoPlayer GetPlayer()
    {
        return _player;
    }

    public Camera GetCamera()
    {
        return _camera;
    }

    public VideoController GetVideoController()
    {
        return _controller;
    }
}
