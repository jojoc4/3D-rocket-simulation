using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{

    public Camera launchpad;
    public Camera distant;
    public Camera ss_top;
    public Camera ss_bottom;
    public Camera fs_top;

    private Cam current;


    // Start is called before the first frame update
    void Start()
    {
        disableAll();
        launchpad.enabled = true;
        current = Cam.LAUNCHPAD_GLOBAL;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void switchCam(Cam c)
    {
        if (c != current)
        {
            disableAll();
            switch (c)
            {
                case Cam.LAUNCHPAD_DISTANT:
                    distant.enabled = true;
                    break;
                case Cam.LAUNCHPAD_GLOBAL:
                    launchpad.enabled = true;
                    break;
                case Cam.SS_BOTTOM:
                    ss_bottom.enabled = true;
                    break;
                case Cam.SS_TOP:
                    ss_top.enabled = true;
                    break;
                case Cam.FS_TOP:
                    fs_top.enabled = true;
                    break;
            }
        }
    }

    void disableAll()
    {
        launchpad.enabled = false;
        distant.enabled = false;
        ss_bottom.enabled = false;
        ss_bottom.enabled = false;
        fs_top.enabled = false;
    }

    public enum Cam
    {
        LAUNCHPAD_GLOBAL,
        LAUNCHPAD_DISTANT,
        SS_BOTTOM,
        SS_TOP,
        FS_TOP
    }
}
