using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBackground : MonoBehaviour {

    MeshRenderer mr;
    Material mat;
    Vector2 offset;

    

    private void Start()
    {
        mr = GetComponent<MeshRenderer>();
        mat = mr.material;
        offset = mat.mainTextureOffset;
    }

    // Update is called once per frame
    void FixedUpdate () {
        offset.y -= Time.deltaTime/1;
        mat.mainTextureOffset = offset;
	}

}
