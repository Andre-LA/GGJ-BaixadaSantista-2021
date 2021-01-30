using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    public float interactMinDistance;
    public LayerMask interactMask;
    public LayerMask entranceMask;

    Camera cam;

    void Awake() {
        cam = FindObjectOfType<Camera>();
    }

    void Update() {
        if (GameInput.Instance.interact) {
            Ray r = GetMousePointerRay();
            bool interacts = DoesInteract(r, interactMask);
            bool enters = DoesInteract(r, entranceMask);

            Debug.DrawLine(
                r.origin,
                r.origin + r.direction * interactMinDistance,
                (interacts || enters) ? Color.green : Color.red
            );

            if (interacts) {
                Debug.Log("Interagiu!!");
            } else if (enters){
                GameStages.Instance.EnterTrain(transform.position);
            }
        }
    }


    bool DoesInteract(Ray ray, LayerMask mask) {
        return Physics.Raycast(ray, interactMinDistance, mask);
    }

    Ray GetMousePointerRay() {
        return cam.ScreenPointToRay(Input.mousePosition);
    }
}
