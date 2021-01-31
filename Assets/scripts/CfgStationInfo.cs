using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CfgStationInfo : MonoBehaviour
{
    public Texture[] prim, sec;
    public Color[] colors;
    public MeshRenderer meshRendererPrim, meshRendererSec;

    public void SetSign(bool isPrimary, int idx, int colorIdx) {

        if (isPrimary) {
            meshRendererPrim.material.mainTexture = prim[idx];
            meshRendererPrim.material.color = colors[colorIdx];
            Debug.Log("idx1: " + idx.ToString(), gameObject);
        }
        else {
            meshRendererSec.material.mainTexture = sec[idx];
            meshRendererSec.material.color = colors[colorIdx];
            Debug.Log("idx2: " + idx.ToString(), gameObject);
        }
    }
}
