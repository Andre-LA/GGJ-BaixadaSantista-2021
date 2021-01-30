using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Velocity velocity;
    public Rotator rotator;
    public RaycastAnchor anchor;

    public float walkEffect, walkEffectVel;
    float anchorOffsetStart;
    float walkEffectT;


    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        anchorOffsetStart = anchor.offset.y;
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

        float dirMag = velocity.direction.magnitude;

        if (dirMag > 1)
            velocity.direction.Normalize();

        dirMag = velocity.direction.magnitude;

        if (dirMag > 0.1f) {
            walkEffectT += dirMag * walkEffectVel * Time.deltaTime;
        }

        float sin = Mathf.Sin(walkEffectT);

        anchor.offset.y = anchorOffsetStart + sin * walkEffect;

        rotator.degrees.y =  GameInput.Instance.mouseX;
        rotator.degrees.x = -GameInput.Instance.mouseY;
    }

    void OnTriggerEnter(Collider col) {
        if (col.CompareTag("ExitTrigger")) {
            GameStages.Instance.GameExit();
        }
    }
}
