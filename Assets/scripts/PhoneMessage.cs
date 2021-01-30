using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhoneMessage : MonoBehaviour
{
    public static PhoneMessage Instance;

    public CfgMessage msgPrefab;
    public RectTransform messagePannel;
    public RectTrLerp rectLerp;

    void Awake() {
        if (Instance != null)
            Debug.LogWarning("Instance is being overwritten!!");
        Instance = this;
    }

    void Update() {
        if (GameInput.Instance.toggleSmartphone)
            rectLerp.reversed = !rectLerp.reversed;
    }

    public void AddMessage(CfgMessage.MessageOwner owner, string message, int extraLines) {
        var newMsg = Instantiate<CfgMessage>(
            msgPrefab,
            Vector3.zero,
            Quaternion.identity,
            messagePannel
        );

        newMsg.Configure(owner, message, extraLines);
    }

    public void Sing() {
        Debug.Log("SING");
    }

    [ContextMenu("Test add msg")]
    public void TestAddMessage() {
        AddMessage(CfgMessage.MessageOwner.Player, "player message yay", 0);
        AddMessage(CfgMessage.MessageOwner.Friend, "Huh, what? Anyway, please find where is the green trash can", 2);
    }
}
