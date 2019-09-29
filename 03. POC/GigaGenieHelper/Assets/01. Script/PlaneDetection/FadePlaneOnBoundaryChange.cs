using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class FadePlaneOnBoundaryChange : MonoBehaviour
{
    const string _fadeOffAnim = "FadeOff";
    const string _fadeOnAnim = "FadeOn";
    const float _timeout = 2.0f;

    Animator _animator;
    ARPlane _plane;

    float _showTime = 0;
    bool _updatePlane = false;

    private void OnEnable()
    {
        _plane = GetComponent<ARPlane>();
        _animator = GetComponent<Animator>();

        _plane.boundaryChanged += PlaneOnBoundaryChanged;
    }

    private void OnDisable()
    {
        _plane.boundaryChanged -= PlaneOnBoundaryChanged;
    }

    private void Update()
    {
        if(_updatePlane)
        {
            _showTime -= Time.deltaTime;

            if(_showTime <= 0)
            {
                _updatePlane = false;
                _animator.SetBool(_fadeOffAnim, true);
                _animator.SetBool(_fadeOnAnim, false);
            }
        }
    }

    void PlaneOnBoundaryChanged(ARPlaneBoundaryChangedEventArgs obj)
    {
        _updatePlane = true;
        _animator.SetBool(_fadeOffAnim, false);
        _animator.SetBool(_fadeOnAnim, true);
        _showTime = _timeout;
    }
}
