using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    public float interactMinDistance;
    public LayerMask interactMask;

    Camera cam;

    void Awake() {
        cam = FindObjectOfType<Camera>();
    }

    void Update() {
        if (GameInput.Instance.interact) {
            Ray r = GetMousePointerRay();
            bool interacts = DoesInteract(r);

            Debug.DrawLine(
                r.origin,
                r.origin + r.direction * interactMinDistance,
                interacts ? Color.green : Color.red
            );

            if (interacts) {
                Debug.Log("Interagiu!!");
            }
        }
    }


    bool DoesInteract(Ray ray) {
        return Physics.Raycast(ray, interactMinDistance, interactMask);
    }

    Ray GetMousePointerRay() {
        return cam.ScreenPointToRay(Input.mousePosition);
    }
}
