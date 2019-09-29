using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 비디오 설정을 처리하는 객체
public class VideoController : MonoBehaviour
{
    DataCenter _dataCenter;
    Slider _transparentSlider;
    Slider _sizeSlider;
    Slider _distanceSlider;
    Slider _horizontalSlider;

    private void Awake()
    {
        /*
        _transparentSlider = GameObject.Find("TransparentSlider").GetComponent<Slider>();
        _sizeSlider = GameObject.Find("SizeSlider").GetComponent<Slider>();
        _distanceSlider = GameObject.Find("DistanceSlider").GetComponent<Slider>();
        _horizontalSlider = GameObject.Find("HorizontalSlider").GetComponent<Slider>();
        _dataCenter = DataCenter.GetInstance();
        */
    }

    private void Start()
    {
    }

    public void ChangeTransparent()
    {
        _dataCenter.SetTransparent(_transparentSlider.value);
    }

    public float GetTransparent()
    {
        return _dataCenter._transparentValue;
    }

    public float GetDistance()
    {
        return _dataCenter._videoDistance;
    }

    public void SetDistance(float value)
    {
        _dataCenter._videoDistance = value;
    }

    public void ChangeDistance()
    {
        _dataCenter.SetVideoDistance(_distanceSlider.value);
    }

    public void ChangeSize()
    {
        _dataCenter.SetVideoSize(_sizeSlider.value);
    }

    public void SetSize(Vector3 value)
    {
        _dataCenter._videoSize = value;
    }

    public Vector3 GetSize()
    {
        return _dataCenter._videoSize;
    }

    public float GetHorizontalLoc()
    {
        return _dataCenter._videoHorizontalLoc;
    }

    public void SetHorizontalLoc(float value)
    {
        _dataCenter._videoHorizontalLoc = value;
    }

    public void ChangeHorizontalLoc()
    {
        _dataCenter.SetVideoHorizontalLoc(_horizontalSlider.value);
    }
}
