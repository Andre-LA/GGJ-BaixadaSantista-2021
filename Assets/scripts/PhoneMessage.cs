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

    public enum MessageId {
        None = -1,
        Message1 = 1,
        Message2 = 2,
        Message3 = 3,
        Message4 = 4,
        Message5 = 5,

        PuzzleFeedback1 = 11,
        PuzzleFeedback2 = 12,
        PuzzleFeedback3 = 13,
        PuzzleFeedback4 = 14,
        PuzzleFeedback5 = 15
    }

    // oh no, some wild a state variables appears!
    MessageId currentMessage = MessageId.None;
    bool[] messagesSend = new bool[5];
    bool[] puzzlesFeedbackSend = new bool[5];

    bool canClose = true;

    void Awake() {
        if (Instance != null)
            Debug.LogWarning("Instance is being overwritten!!");
        Instance = this;
    }

    void Update() {
        if (GameInput.Instance.toggleSmartphone && canClose) {
            rectLerp.reversed = !rectLerp.reversed;
            if (currentMessage != MessageId.None)
                Message();
        }
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

    public void PrepareMessage(bool isMessage, int puzzleIndex) {
        bool alreadySend = isMessage ? messagesSend[puzzleIndex] : puzzlesFeedbackSend[puzzleIndex];

        if (!alreadySend) {
            currentMessage = (MessageId)(puzzleIndex + (isMessage ? 1 : 11));

            if (isMessage)
                messagesSend[puzzleIndex] = true;
            else
                puzzlesFeedbackSend[puzzleIndex] = true;

            Debug.Log("Preparing " + (isMessage ? "Message" : "PuzzleFeedback") + " on puzzle #" + puzzleIndex.ToString() + " -> " + currentMessage.ToString());
        } else
            Debug.Log("Repeated message avoided");
    }

    public void Sing() {
        Debug.Log("SING");
    }

    void Message() {
        switch (currentMessage) {
            case MessageId.Message1: StartCoroutine(Co_Message1()); break;
            case MessageId.Message2: StartCoroutine(Co_Message2()); break;
            case MessageId.Message3: StartCoroutine(Co_Message3()); break;
            case MessageId.Message4: StartCoroutine(Co_Message4()); break;
            case MessageId.Message5: StartCoroutine(Co_Message5()); break;

            case MessageId.PuzzleFeedback1: StartCoroutine(Co_PuzzleFeedback1()); break;
            case MessageId.PuzzleFeedback2: StartCoroutine(Co_PuzzleFeedback2()); break;
            case MessageId.PuzzleFeedback3: StartCoroutine(Co_PuzzleFeedback3()); break;
            case MessageId.PuzzleFeedback4: StartCoroutine(Co_PuzzleFeedback4()); break;
        }
    }

    IEnumerator Co_Message1() {
        canClose = false;

        yield return new WaitForSeconds(0.3f);
        AddMessage(CfgMessage.MessageOwner.Player, "Message1", 0);
        yield return new WaitForSeconds(0.5f);
        AddMessage(CfgMessage.MessageOwner.Friend, "YEAH!", 0);
        yield return new WaitForSeconds(0.2f);

        canClose = true;
        currentMessage = MessageId.None;
    }

    IEnumerator Co_Message2() {
        canClose = false;

        yield return new WaitForSeconds(0.3f);
        AddMessage(CfgMessage.MessageOwner.Player, "Message2", 0);
        yield return new WaitForSeconds(0.5f);
        AddMessage(CfgMessage.MessageOwner.Friend, "YEAH!", 0);
        yield return new WaitForSeconds(0.2f);

        canClose = true;
        currentMessage = MessageId.None;
    }

    IEnumerator Co_Message3() {
        canClose = false;

        yield return new WaitForSeconds(0.3f);
        AddMessage(CfgMessage.MessageOwner.Player, "Message3", 0);
        yield return new WaitForSeconds(0.5f);
        AddMessage(CfgMessage.MessageOwner.Friend, "YEAH!", 0);
        yield return new WaitForSeconds(0.2f);

        canClose = true;
        currentMessage = MessageId.None;
    }

    IEnumerator Co_Message4() {
        canClose = false;

        yield return new WaitForSeconds(0.3f);
        AddMessage(CfgMessage.MessageOwner.Player, "Message4", 0);
        yield return new WaitForSeconds(0.5f);
        AddMessage(CfgMessage.MessageOwner.Friend, "YEAH!", 0);
        yield return new WaitForSeconds(0.2f);

        canClose = true;
        currentMessage = MessageId.None;
    }

    IEnumerator Co_Message5() {
        canClose = false;

        yield return new WaitForSeconds(0.3f);
        AddMessage(CfgMessage.MessageOwner.Player, "Message5", 0);
        yield return new WaitForSeconds(0.5f);
        AddMessage(CfgMessage.MessageOwner.Friend, "YEAH!", 0);
        yield return new WaitForSeconds(0.2f);

        canClose = true;
        currentMessage = MessageId.None;
    }

    IEnumerator Co_PuzzleFeedback1() {
        canClose = false;

        yield return new WaitForSeconds(0.3f);
        AddMessage(CfgMessage.MessageOwner.Player, "PuzzleFeedback1", 0);
        yield return new WaitForSeconds(0.5f);
        AddMessage(CfgMessage.MessageOwner.Friend, "YEAH!", 0);
        yield return new WaitForSeconds(0.2f);

        canClose = true;
        currentMessage = MessageId.None;
    }

    IEnumerator Co_PuzzleFeedback2() {
        canClose = false;

        yield return new WaitForSeconds(0.3f);
        AddMessage(CfgMessage.MessageOwner.Player, "PuzzleFeedback2", 0);
        yield return new WaitForSeconds(0.5f);
        AddMessage(CfgMessage.MessageOwner.Friend, "YEAH!", 0);
        yield return new WaitForSeconds(0.2f);

        canClose = true;
        currentMessage = MessageId.None;
    }

    IEnumerator Co_PuzzleFeedback3() {
        canClose = false;

        yield return new WaitForSeconds(0.3f);
        AddMessage(CfgMessage.MessageOwner.Player, "PuzzleFeedback3", 0);
        yield return new WaitForSeconds(0.5f);
        AddMessage(CfgMessage.MessageOwner.Friend, "YEAH!", 0);
        yield return new WaitForSeconds(0.2f);

        canClose = true;
        currentMessage = MessageId.None;
    }

    IEnumerator Co_PuzzleFeedback4() {
        canClose = false;

        yield return new WaitForSeconds(0.3f);
        AddMessage(CfgMessage.MessageOwner.Player, "PuzzleFeedback4", 0);
        yield return new WaitForSeconds(0.5f);
        AddMessage(CfgMessage.MessageOwner.Friend, "YEAH!", 0);
        yield return new WaitForSeconds(0.2f);

        canClose = true;
        currentMessage = MessageId.None;
    }
}
