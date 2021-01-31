using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTo : MonoBehaviour
{
    public bool autorun;
    public Vector3 alvoVec3;
    public Transform alvoTr;

    public enum TargetType { useVector3, useTransform }

    public TargetType targetType;

    public bool ignoreX, ignoreY, ignoreZ;

    Transform tr;

    void Awake() {
        tr = GetComponent<Transform>();
    }

    void Update() {
        if (autorun)
            AutoLook();
    }

    void AutoLook() {
        Look();
    }

    public void Look() {
        var alvo = alvoVec3;

        if (targetType == TargetType.useTransform)
            alvo = alvoTr.position;

        var rotBefore = tr.eulerAngles;

        tr.LookAt(alvo);

        var rot = tr.eulerAngles;

        if (ignoreX)
            rot.x = rotBefore.x;
        if (ignoreY)
            rot.y = rotBefore.y;
        if (ignoreZ)
            rot.z = rotBefore.z;

        tr.eulerAngles = rot;
    }
}
