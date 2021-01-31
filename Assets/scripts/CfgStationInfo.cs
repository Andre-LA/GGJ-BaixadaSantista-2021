using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CfgStationInfo : MonoBehaviour
{
    public Texture[] prim, sec;
    public Color[] colors;
    public MeshRenderer meshRendererPrim, meshRendererSec;

    public void SetSign(bool isPrimary, int idx) {
        if (isPrimary) {
            meshRendererPrim.material.mainTexture = prim[idx];
            meshRendererPrim.material.color = colors[idx];
        }
        else {
            meshRendererSec.material.mainTexture = sec[idx];
            meshRendererSec.material.color = colors[idx];
        }
    }
}
