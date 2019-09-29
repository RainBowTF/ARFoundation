using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    GameObject _configPanel;

    // Start is called before the first frame update
    void Start()
    {
        _configPanel = GameObject.Find("ConfigPanel");
        DisactivateConfigure();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateConfigure()
    {
        _configPanel.SetActive(true);
    }

    public void DisactivateConfigure()
    {
        _configPanel.SetActive(false);
    }
}
