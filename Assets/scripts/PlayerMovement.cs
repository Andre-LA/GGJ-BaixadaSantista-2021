using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Velocity velocity;
    public Rotator rotator;

    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update() {
        if (GameInput.Instance.escape)
            Cursor.lockState = (
                Cursor.lockState == CursorLockMode.Locked
                    ? CursorLockMode.None
                    : CursorLockMode.Locked
            );

        velocity.direction.x = GameInput.Instance.horizontal;
        velocity.direction.z = GameInput.Instance.vertical;

        if (velocity.direction.magnitude > 1)
            velocity.direction.Normalize();

        rotator.degrees.y =  GameInput.Instance.mouseX;
        rotator.degrees.x = -GameInput.Instance.mouseY;
    }
}
