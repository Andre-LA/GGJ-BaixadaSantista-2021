using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowInitializer : MonoBehaviour
{
    public CharacterMovement shadowPrefab;
    public Transform parentGroup;
    public int shadowQuantity;

    public Vector2 areaMin, areaMax;

    void Start() {
        for (int i = 0; i < shadowQuantity; ++i) {
            float x = Random.Range(areaMin.x, areaMax.x);
            float z = Random.Range(areaMin.y, areaMax.y);

            var shadowMov = Instantiate<CharacterMovement>(
                shadowPrefab,
                new Vector3(x, 0, z),
                Quaternion.identity,
                parentGroup
            );

            shadowMov.InitRoute(areaMin, areaMax);
        }
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.yellow;

        Gizmos.DrawLine(
            new Vector3(areaMin.x, 0, areaMin.y),
            new Vector3(areaMax.x, 0, areaMin.y)
        );

        Gizmos.DrawLine(
            new Vector3(areaMax.x, 0, areaMin.y),
            new Vector3(areaMax.x, 0, areaMax.y)
        );

        Gizmos.DrawLine(
            new Vector3(areaMax.x, 0, areaMax.y),
            new Vector3(areaMin.x, 0, areaMax.y)
        );

        Gizmos.DrawLine(
            new Vector3(areaMin.x, 0, areaMax.y),
            new Vector3(areaMin.x, 0, areaMin.y)
        );
    }
}
