using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ObjectOnPlane : MonoBehaviour
{
    [Tooltip("터치 위치에 올릴 프리팹")]
    [SerializeField] GameObject _placedPrefab;
    private GameObject spawnedObject;

    ARRaycastManager _raycastManager;
    static List<ARRaycastHit> _hits = new List<ARRaycastHit>();

    private void Awake()
    {
        _raycastManager = GetComponent<ARRaycastManager>();
    }

    bool GetTouchPosition(out Vector2 touchPosition)
    {
        if(Input.GetMouseButton(0))
        {
            var mousePosition = Input.mousePosition;
            touchPosition = new Vector2(mousePosition.x, mousePosition.y);
            return true;
        }

        touchPosition = default;
        return false;
    }

    private void Update()
    {
        if (!GetTouchPosition(out Vector2 touchPosiiton))
            return;

        // 레이케스트를 날린다
        if( _raycastManager.Raycast(touchPosiiton, _hits, TrackableType.PlaneWithinPolygon) )
        {
            // 선택한 위치
            var hitPosition = _hits[0].pose;

            if( spawnedObject  == null )
            {
                spawnedObject = Instantiate(_placedPrefab, hitPosition.position, hitPosition.rotation);
            }
            else
            {
                spawnedObject.transform.position = hitPosition.position;
            }

            // 스테이트 머신에 위치가 입력된걸 알린다
            DataCenter.GetInstance().GetStateMachine("GameController").AddSceneNumber();
        }
    }
}
