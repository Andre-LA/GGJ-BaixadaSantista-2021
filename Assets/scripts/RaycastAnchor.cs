using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastAnchor : MonoBehaviour
{
    public LayerMask mask;
    public Vector3 direction;
    public float distance;
    public bool distanceIsInfinite;
    public bool anchorX, anchorY, anchorZ;
    public Vector3 offset;

    Transform tr;

    void Awake() {
        tr = GetComponent<Transform>();
    }

    void Update() {
        RaycastHit hit;
        if (Physics.Raycast(
            tr.position,
            direction,
            out hit,
            distanceIsInfinite ? Mathf.Infinity : distance,
            mask,
            QueryTriggerInteraction.Ignore
        )) {
            tr.position = new Vector3(
                anchorX ? hit.point.x + offset.x : tr.position.x,
                anchorY ? hit.point.y + offset.y : tr.position.y,
                anchorZ ? hit.point.z + offset.z : tr.position.z
            );
        }
    }
}
