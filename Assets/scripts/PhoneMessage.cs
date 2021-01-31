using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhoneMessage : MonoBehaviour
{
    public static PhoneMessage Instance;

    public CfgMessage msgPrefab;
    public RectTransform messagePannel;
    public RectTrLerp rectLerp, bellLerpActiveFx, bellLerpInactiveFx;

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
            if (currentMessage != MessageId.None) {
                bellLerpInactiveFx.Reset();
                bellLerpInactiveFx.enabled = true;
                bellLerpActiveFx.enabled = false;
                Message();
            }
        }
    }

    public void AddMessage(CfgMessage.MessageOwner owner, string message, int extraLines) {
        var newMsg = Instantiate<CfgMessage>(
            msgPrefab,
            Vector3.zero,
            Quaternion.identity,
            messagePannel
        );

        newMsg.Configure(owner, message, extraLines + 2);
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

            bellLerpActiveFx.Reset();
            bellLerpInactiveFx.enabled = false;
            bellLerpActiveFx.enabled = true;
        } else
            Debug.Log("Repeated message avoided");
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

        yield return new WaitForSeconds(0.5f);
        AddMessage(CfgMessage.MessageOwner.Friend, "Vai ser facinho chegar aqui", 0);
        yield return new WaitForSeconds(1.8f);
        AddMessage(CfgMessage.MessageOwner.Friend, "Eu só não me lembro exatamente dos trens que eu peguei", 3);
        yield return new WaitForSeconds(1.6f);
        AddMessage(CfgMessage.MessageOwner.Friend, "Mas sei como te explicar", 0);
        yield return new WaitForSeconds(1.7f);
        AddMessage(CfgMessage.MessageOwner.Friend, "Lembro que no primeiro trem havia uma máquina de refrigerante quebrada na plataforma", 6);
        yield return new WaitForSeconds(1.4f);
        AddMessage(CfgMessage.MessageOwner.Player, "Entendi. Vou procurar", 0);
        yield return new WaitForSeconds(0.2f);

        canClose = true;
        currentMessage = MessageId.None;
    }

    IEnumerator Co_Message2() {
        canClose = false;

        yield return new WaitForSeconds(0.5f);
        AddMessage(CfgMessage.MessageOwner.Friend, "Perfeito", 0);
        yield return new WaitForSeconds(1f);
        AddMessage(CfgMessage.MessageOwner.Friend, "Agora, lembro de entrar no trem quando encontrei aquela TV grande, na plataforma, com a nova série da Motflex, sei lá", 8);
        yield return new WaitForSeconds(2f);
        AddMessage(CfgMessage.MessageOwner.Friend, "Você está sempre assistindo na TV", 0);
        yield return new WaitForSeconds(1f);
        AddMessage(CfgMessage.MessageOwner.Friend, "Mas não me lembro se eu peguei outra linha, ou segui em frente para chegar até a TV", 4);
        yield return new WaitForSeconds(2f);
        AddMessage(CfgMessage.MessageOwner.Player, "Motflex??", 0);
        yield return new WaitForSeconds(0.2f);

        canClose = true;
        currentMessage = MessageId.None;
    }

    IEnumerator Co_Message3() {
        canClose = false;

        yield return new WaitForSeconds(0.5f);
        AddMessage(CfgMessage.MessageOwner.Friend, "Otimo querida", 0);
        yield return new WaitForSeconds(1.8f);
        AddMessage(CfgMessage.MessageOwner.Friend, "Só que agora as coisas podem ficar complicadas, pois eu peguei no sono nessa linha", 6);
        yield return new WaitForSeconds(1.6f);
        AddMessage(CfgMessage.MessageOwner.Friend, "Talvez eu tenha até dado a volta", 0);
        yield return new WaitForSeconds(1.7f);
        AddMessage(CfgMessage.MessageOwner.Friend, "Mas não se preocupe, você deve sentir um fedor horroroso de uma das lixeiras", 5);
        yield return new WaitForSeconds(1.5f);
        AddMessage(CfgMessage.MessageOwner.Friend, "Parece que não trocam ela há muito tempo", 1);
        yield return new WaitForSeconds(1.4f);
        AddMessage(CfgMessage.MessageOwner.Friend, "Assim que eu passei perto, entrei direto para dentro do trem", 3);
        yield return new WaitForSeconds(1.4f);
        AddMessage(CfgMessage.MessageOwner.Player, "Ainda não limparam essa lixeira?", 0);
        yield return new WaitForSeconds(0.2f);

        canClose = true;
        currentMessage = MessageId.None;
    }

    IEnumerator Co_Message4() {
        canClose = false;

        yield return new WaitForSeconds(0.5f);
        AddMessage(CfgMessage.MessageOwner.Friend, "Agora vá para a plataforma que tem a placa de inauguração da estação, e entre no trem", 6);
        yield return new WaitForSeconds(1.8f);
        AddMessage(CfgMessage.MessageOwner.Friend, "Não será difícil de achar. Essa placa me fez lembrar a primeira vez que andei de metrô", 6);
        yield return new WaitForSeconds(1.6f);
        AddMessage(CfgMessage.MessageOwner.Player, "Se não me engano essa foi a primeira linha do metro feita", 3);
        yield return new WaitForSeconds(0.2f);

        canClose = true;
        currentMessage = MessageId.None;
    }

    IEnumerator Co_Message5() {
        canClose = false;

        yield return new WaitForSeconds(0.5f);
        AddMessage(CfgMessage.MessageOwner.Friend, "Me perdoe querida", 0);
        yield return new WaitForSeconds(1.8f);
        AddMessage(CfgMessage.MessageOwner.Friend, "Preciso trocar esses meus óculos, mas eu consegui ajuda", 4);
        yield return new WaitForSeconds(1.9f);
        AddMessage(CfgMessage.MessageOwner.Friend, "Você só precisa vir até o ponto 4 da linha azul", 2);
        yield return new WaitForSeconds(1.7f);
        AddMessage(CfgMessage.MessageOwner.Friend, "Eu devia ter procurado ajuda antes, minha cabeça já não é mais a mesma", 5);
        yield return new WaitForSeconds(1.9f);
        AddMessage(CfgMessage.MessageOwner.Friend, "Estou te esperando", 0);
        yield return new WaitForSeconds(2f);
        AddMessage(CfgMessage.MessageOwner.Player, "Não se preocupe vovó. Estou indo", 0);
        yield return new WaitForSeconds(0.2f);

        canClose = true;
        currentMessage = MessageId.None;
    }

    IEnumerator Co_PuzzleFeedback1() {
        canClose = false;

        yield return new WaitForSeconds(0.8f);
        AddMessage(CfgMessage.MessageOwner.Player, "Ok!", 0);
        yield return new WaitForSeconds(1.5f);
        AddMessage(CfgMessage.MessageOwner.Player, "Nossa essa máquina está horrível", 0);
        yield return new WaitForSeconds(1.8f);
        AddMessage(CfgMessage.MessageOwner.Player, "Mas já vou entrar no trem da linha amarela", 2);
        yield return new WaitForSeconds(0.2f);

        canClose = true;
        currentMessage = MessageId.None;
    }

    IEnumerator Co_PuzzleFeedback2() {
        canClose = false;

        yield return new WaitForSeconds(0.8f);
        AddMessage(CfgMessage.MessageOwner.Player, "Ok, essa foi difícil!", 0);
        yield return new WaitForSeconds(1.5f);
        AddMessage(CfgMessage.MessageOwner.Player, "Encontrei a propaganda da série", 2);
        yield return new WaitForSeconds(1f);
        AddMessage(CfgMessage.MessageOwner.Player, "E é Notflix hahaha", 0);
        yield return new WaitForSeconds(1.3f);
        AddMessage(CfgMessage.MessageOwner.Player, "Estou entrando na linha azul então", 0);
        yield return new WaitForSeconds(0.2f);

        canClose = true;
        currentMessage = MessageId.None;
    }

    IEnumerator Co_PuzzleFeedback3() {
        canClose = false;

        yield return new WaitForSeconds(0.8f);
        AddMessage(CfgMessage.MessageOwner.Player, "Apenas queria dizer que sinto inveja da sua habilidade de dormir em qualquer lugar haha", 7);
        yield return new WaitForSeconds(1.5f);
        AddMessage(CfgMessage.MessageOwner.Player, "Entrando no trem da linha amarela já!", 3);
        yield return new WaitForSeconds(0.2f);

        canClose = true;
        currentMessage = MessageId.None;
    }

    IEnumerator Co_PuzzleFeedback4() {
        canClose = false;

        yield return new WaitForSeconds(0.8f);
        AddMessage(CfgMessage.MessageOwner.Player, "Estou entrando na linha vermelha, mas desse jeito vou voltar de onde eu vim", 4);
        yield return new WaitForSeconds(0.2f);

        canClose = true;
        currentMessage = MessageId.None;
    }
}
