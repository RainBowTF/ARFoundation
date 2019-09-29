using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class GenieController : MonoBehaviour
{
    GameObject _origin;
    GameObject _camera;
    ARRaycastManager _raycastManager;
    List<ARRaycastHit> _hits;
    Vector2 _touchPosition;
    Dictionary<string, GameObject> _dictCheck;

    private void Awake()
    {
        _dictCheck = new Dictionary<string, GameObject>();
        _dictCheck.Add("LanCable", this.gameObject.transform.Find("checkLan").gameObject);
        _dictCheck.Add("HDMICable", this.gameObject.transform.Find("checkHDMI").gameObject);
        _dictCheck.Add("PowerCable", this.gameObject.transform.Find("checkPower").gameObject);
        _dictCheck.Add("PowerOn", this.gameObject.transform.Find("checkPowerEnter").gameObject);

        foreach(GameObject obj in _dictCheck.Values)
        {
            obj.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _origin = GameObject.Find("AR Session Origin");
        _camera = GameObject.FindGameObjectWithTag("MainCamera");
        _raycastManager = _origin.GetComponent<ARRaycastManager>();
        _hits = new List<ARRaycastHit>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GetTouchPosition(out Vector2 touchPosiiton))
            return;

        if ( _raycastManager.Raycast(touchPosiiton, _hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon) )
        {
            if (_touchPosition != null)
            {
                Quaternion rotation = transform.rotation;
                float x = touchPosiiton.x - _touchPosition.x;

                if ( Mathf.Abs(x) > 90.0f)
                {
                    // do nothing
                }
                else if(x < 0)
                {
                    rotation = rotation * Quaternion.Euler(0, -x * Time.deltaTime * 10f, 0);
                }
                else if( x > 0)
                {
                    rotation = rotation * Quaternion.Euler(0, -x * Time.deltaTime * 10.0f, 0);
                }

                transform.rotation = rotation;
            }
        }

        // 이후값과 비교하기위해 이전값을 저장해 놓는다
        _touchPosition = touchPosiiton;
    }

    bool GetTouchPosition(out Vector2 touchPosition)
    {
        if (Input.GetMouseButton(0))
        {
            var mousePosition = Input.mousePosition;
            touchPosition = new Vector2(mousePosition.x, mousePosition.y);
            return true;
        }

        touchPosition = default;
        return false;
    }

    // 입력된 위치와 현재 앵글이 일치하는지 확인한다
    // FRONT : 앞
    // BACK : 뒤
    public bool CheckAngle(string angleType)
    {
        Vector3 originFoward = _camera.transform.forward; originFoward.y = 0;
        Vector3 genieForward = transform.forward; genieForward.y = 0;
        float angle = Vector3.Angle(genieForward, originFoward);

        //DataCenter.GetInstance().AddDebugString("angle : " + angle);

        if (angle >= 130 && angle <= 150 )
        {
            if( angleType.Equals("BACK") )
            {
                return true;
            }
        }
        else if(angle >= 350 && angle <= 10)
        {
            if(angleType.Equals("FRONT"))
            {
                return true;
            }
        }
        return false;
    }

    public bool CheckAngleTest(string angleType)
    {
        
        Vector3 rotation = Vector3.RotateTowards(transform.forward, GameObject.FindGameObjectWithTag("SubCamera").transform.forward, 10, 0);
        Debug.Log(rotation.y);

        if (transform.rotation.eulerAngles.y >= 165 && transform.rotation.eulerAngles.y <= 195)
        {
            if (angleType.Equals("BACK"))
            {
                return true;
            }
        }
        else if (transform.rotation.eulerAngles.y >= -15 && transform.rotation.eulerAngles.y <= 15)
        {
            if (angleType.Equals("FRONT"))
            {
                return true;
            }
        }
        return false;
    }

    // 체크 아이콘을 활성화 시킨다
    public void ActivateCheck(string str)
    {
        foreach (GameObject obj in _dictCheck.Values)
        {
            obj.SetActive(false);
        }

        try
        {
            _dictCheck[str].SetActive(true);
        }
        catch
        {
            // do nothing
        }
    }

    // 체크 아이콘을 비활성화 시킨다
    public void InactivateCheck()
    {
        foreach (GameObject obj in _dictCheck.Values)
        {
            obj.SetActive(false);
        }
    }
}
