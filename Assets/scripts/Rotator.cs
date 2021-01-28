using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [System.Serializable]
    public struct InvalidRange {
        public float min, max;

        public bool IsOnRange(float vl) {
            return vl >= min && vl <= max;
        }
    }

    public Vector3 degrees;
    public float speed;
    public Space relativeTo;

    public bool preserveX, preserveY, preserveZ;

    public bool resetDegreesAfterRotation;

    public InvalidRange invalidRangeX, invalidRangeY, invalidRangeZ;
    public bool useInvalidRangeX, useInvalidRangeY, useInvalidRangeZ;

    Transform tr;

    void Awake() {
        tr = GetComponent<Transform>();
    }

    void Update() {
        Vector3 rotBefore = tr.eulerAngles;

        tr.Rotate(degrees * speed * Time.deltaTime, relativeTo);

        if (resetDegreesAfterRotation)
            degrees = Vector3.zero;

        bool preserve_x = preserveX || (useInvalidRangeX ? invalidRangeX.IsOnRange(tr.eulerAngles.x) : false);
        bool preserve_y = preserveY || (useInvalidRangeY ? invalidRangeY.IsOnRange(tr.eulerAngles.y) : false);
        bool preserve_z = preserveZ || (useInvalidRangeZ ? invalidRangeZ.IsOnRange(tr.eulerAngles.z) : false);

        tr.eulerAngles = new Vector3(
            preserve_x ? rotBefore.x : tr.eulerAngles.x,
            preserve_y ? rotBefore.y : tr.eulerAngles.y,
            preserve_z ? rotBefore.z : tr.eulerAngles.z
        );
    }
}
