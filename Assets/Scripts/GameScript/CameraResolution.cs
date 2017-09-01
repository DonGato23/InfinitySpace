using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraResolution : MonoBehaviour {

    private void Awake()
    {
        Screen.SetResolution(320, 480, true);
    }
}
