using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowInitializer : MonoBehaviour
{
    public CharacterMovement shadowPrefab;
    public Transform parentGroup;

    public Vector2 areaMin, areaMax;

    bool generated;

    void EraseShadows() {
        Transform newGroup = new GameObject(parentGroup.name).transform;
        newGroup.SetParent(transform);
        newGroup.position = parentGroup.position;
        Destroy(parentGroup.gameObject);
        parentGroup = newGroup;
    }

    public void GenerateShadows(int shadowQuantity) {
        if (generated)
            EraseShadows();

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

        generated = true;
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
