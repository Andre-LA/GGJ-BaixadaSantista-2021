using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance;

    public float vertical, horizontal,
                 mouseX, mouseY;

    public bool interact, escape;
    public bool toggleSmartphone;

    void Awake() {
        if (Instance != null)
            Debug.LogWarning("There is multiple GameInput's!");

        Instance = this;
    }

    void Update() {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");

        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        interact = Input.GetButtonDown("Submit");
        escape = Input.GetKeyDown(KeyCode.Escape);

        toggleSmartphone = Input.GetKeyDown(KeyCode.Space);
    }
}
