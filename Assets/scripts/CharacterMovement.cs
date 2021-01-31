using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public Velocity velocity;
    public LookAtTo lookAtTo;
    public int routeCount;

    public float minDistance;

    public Vector3[] routePoints;
    int currentCounter;

    Transform tr;

    float elapsedTime = 0;

    public bool showGizmos;

    void Awake() {
        tr = GetComponent<Transform>();
    }

    public void InitRoute(Vector2 areaMin, Vector2 areaMax) {
        routePoints = new Vector3[routeCount];

        for (int i = 0; i < routeCount; ++i) {
            float x = Random.Range(areaMin.x, areaMax.x);
            float z = Random.Range(areaMin.y, areaMax.y);

            routePoints[i] = new Vector3(x, 0, z);
        }

        AdjustLook();
    }

    void Update() {
        elapsedTime += Time.deltaTime;

        if (Vector3.Distance(tr.position, routePoints[currentCounter]) < minDistance || elapsedTime > 10f) {
            currentCounter = (currentCounter + 1) % routeCount;
            AdjustLook();
            elapsedTime = 0f;
        }
    }

    void AdjustLook() {
        lookAtTo.alvoVec3 = routePoints[currentCounter];
        lookAtTo.Look();
    }

    void OnDrawGizmos() {
        if (!showGizmos || routePoints.Length < currentCounter + 1)
            return;

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, minDistance);
        Gizmos.DrawLine(transform.position, routePoints[currentCounter]);
    }
}
