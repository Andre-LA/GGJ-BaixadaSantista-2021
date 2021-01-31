using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    public float interactMinDistance;
    public LayerMask interactMask;
    public LayerMask entranceMask;

    public Transform camTr;

    void Update() {
        if (GameInput.Instance.interact) {
            RaycastHit hitInteract;
            Ray r = GetMousePointerRay();
            bool interacts = DoesInteract(r, interactMask, out hitInteract);
            bool enters = DoesInteract(r, entranceMask);

            Debug.DrawLine(
                r.origin,
                r.origin + r.direction * interactMinDistance,
                (interacts || enters) ? Color.green : Color.red,
                0.1f
            );


            if (interacts) {
                GameStages.Instance.Interacted(hitInteract);
            } else if (enters){
                GameStages.Instance.EnterTrain(transform.position);
            }
        }
    }


    bool DoesInteract(Ray ray, LayerMask mask, out RaycastHit hit) {
        return Physics.Raycast(ray, out hit, interactMinDistance, mask);
    }

    bool DoesInteract(Ray ray, LayerMask mask) {
        return Physics.Raycast(ray, interactMinDistance, mask);
    }

    Ray GetMousePointerRay() {
        return new Ray(camTr.position, camTr.forward);
    }
}
