using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject videoCanvas = GameObject.Find("VideoCanvasObjTrack");

        if (videoCanvas == null)
        {
            videoCanvas = GameObject.Instantiate(Resources.Load<GameObject>("VideoCanvasObjTrack"));
        }

        videoCanvas.SetActive(true);
        PlayVideoTransparent _playVideoTransparent = videoCanvas.GetComponent<PlayVideoTransparent>();
        _playVideoTransparent.StartVideo();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
