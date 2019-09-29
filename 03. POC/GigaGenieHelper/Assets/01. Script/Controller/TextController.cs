using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour
{
    [Tooltip("컨트롤할 텍스트 오브젝트 명칭")]
    [SerializeField] string _controlTextObjectName = "HelpTEext";
    Text _helpText;
    float _curTime = 0;
    float _fadeInTime = 0;
    float _fadeOutTime = 0;
    bool _isWork = false;
    Color _originColor = Color.white;

    private void Start()
    {
        _helpText = GameObject.Find(_controlTextObjectName).GetComponent<Text>();
    }

    private void Update()
    {
        // 작동여부 설정
        if(_isWork)
        {
            // 페이드 인 시간체크
            if( _curTime <= _fadeInTime )
            {
                float alpha = _helpText.color.a + (1 / _fadeInTime) * Time.deltaTime;
                _helpText.color = new Color(_helpText.color.r, _helpText.color.g, _helpText.color.b, alpha);
            }
            // 페이드 아웃 시간체크
            else if( _curTime <= _fadeInTime + _fadeOutTime)
            {
                float alpha = _helpText.color.a - (1 / _fadeOutTime) * Time.deltaTime;
                _helpText.color = new Color(_helpText.color.r, _helpText.color.g, _helpText.color.b, alpha);
            }
            // 종료
            else
            {
                _isWork = false;
                DataCenter.GetInstance().GetStateMachine("GameController").AddSceneNumber();
            }

            _curTime += Time.deltaTime;
        }
    }

    // 보여줄 텍스트를  넣는다
    // text : 텍스트
    // fadeInTime : 페이드 인이 발생하는 duration
    // fadeOutTime : 페이드 아웃이 발생하는 duration
    public void SetText(string text, float fadeInTime, float fadeOutTimne, Color color)
    {
        _helpText.color = color;
        _helpText.color = new Color(_helpText.color.r, _helpText.color.g, _helpText.color.b, 0);
        _helpText.text = text;
        _fadeInTime = fadeInTime;
        _fadeOutTime = fadeOutTimne;
        _isWork = true;
        _curTime = 0;
    }
}
