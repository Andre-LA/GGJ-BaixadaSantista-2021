using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CfgCatraca : MonoBehaviour
{
    public Texture locked, unlocked;
    public MeshRenderer meshRend;

    public void SetLock(bool isLocked) {
        meshRend.material.mainTexture = isLocked ? locked : unlocked;
    }
}
