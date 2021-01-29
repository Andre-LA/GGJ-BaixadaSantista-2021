using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTo : MonoBehaviour
{
    public bool autorun;
    public Vector3 alvoVec3;

    public bool ignoreX, ignoreY, ignoreZ;

    Transform tr;

    void Awake() {
        tr = GetComponent<Transform>();
    }

    void Update() {
        if (autorun)
            Look();
    }

    public void Look() {
        tr.LookAt(new Vector3(
            ignoreX ? transform.position.x : alvoVec3.x,
            ignoreY ? transform.position.y : alvoVec3.y,
            ignoreZ ? transform.position.z : alvoVec3.z
        ));
    }
}
