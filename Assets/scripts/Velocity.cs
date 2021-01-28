using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Velocity : MonoBehaviour
{
    public float speed;
    public Vector3 direction;
    public Space relativeTo;
    public bool preserveX, preserveY, preserveZ;

    Transform tr;

    void Awake() {
        tr = GetComponent<Transform>();
    }

    void FixedUpdate() {
        Vector3 posBefore = tr.position;

        tr.Translate(direction * speed * Time.deltaTime, relativeTo);

        tr.position = new Vector3(
            preserveX ? posBefore.x : tr.position.x,
            preserveY ? posBefore.y : tr.position.y,
            preserveZ ? posBefore.z : tr.position.z
        );
    }
}
