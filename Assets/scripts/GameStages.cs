using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStages : MonoBehaviour
{
    public static GameStages Instance;
    public Transform playerTr;
    public float ZDivision;
    public SubwayStation stationLine1, stationLine2;

    public Image transitionImg;
    public float transitionSpeed;
    public AnimationCurve transitionCurve;

    bool inTransition;

    void Awake() {
        if (Instance != null)
            Debug.LogWarning("Instance is being overwritten!!");
        Instance = this;
    }

    public void Red_2() {
        stationLine1.SetSubwaySettings(SubwayStation.LineColor.Red, 2, 20);
        stationLine2.SetSubwaySettings(SubwayStation.LineColor.Yellow, 2, 40);
    }

    public void Yellow_4() {
        stationLine1.SetSubwaySettings(SubwayStation.LineColor.Yellow, 4, 20);
        stationLine2.SetSubwaySettings(SubwayStation.LineColor.Blue, 2, 10);
    }

    public void EnterTrain(Vector3 playerPos) {
        if(!inTransition)
            StartCoroutine(Co_EnterTrain(playerPos.z));
    }

    IEnumerator Co_EnterTrain(float playerPosZ) {
        yield return StartCoroutine(Transition(true));

        var isFirstStation = IsPosOnFirstStation(playerPosZ);

        var station = isFirstStation ? stationLine1 : stationLine2;
        var otherStation = isFirstStation ? stationLine2 : stationLine1;

        var lineColor = station.GetLineColor();
        var lineIndex = station.GetLineIndex();

        switch (lineColor) {
            case SubwayStation.LineColor.Red   : GoNextRed(lineIndex); break;
            case SubwayStation.LineColor.Blue  : GoNextBlue(lineIndex); break;
            case SubwayStation.LineColor.Yellow: GoNextYellow(lineIndex); break;
        }

        playerTr.position = station.refTeleport.position;
        playerTr.rotation = station.refTeleport.rotation;

        yield return StartCoroutine(Transition(false));
    }

    IEnumerator Transition(bool hiding) {
        if (hiding)
            inTransition = true;

        Color color = transitionImg.color;
        float t = 0;

        while (t < 0.99f) {
            t += Time.deltaTime * transitionSpeed;
            t = Mathf.Clamp01(t);

            color.a = Mathf.Lerp(color.a, hiding ? 1f : 0f, transitionCurve.Evaluate(t));

            transitionImg.color = color;
            yield return new WaitForEndOfFrame();
        }

        if (!hiding)
            inTransition = false;
    }

    void GoNextRed(int lineIndex) {
        Debug.Log("GoNextRed: " + lineIndex.ToString());
        switch(lineIndex) {
            case 1: Red_2(); break;
            //case 2: Red_2(); break;
            //case 3: Red_3(); break;
            //case 4: Red_4(); break;
            //case 5: Red_5(); break;
        }
    }

    void GoNextBlue(int lineIndex) {
        Debug.Log("GoNextBlue: " + lineIndex.ToString());
        switch(lineIndex) {
            //case 1: Blue_1(); break;
            //case 2: Blue_2(); break;
            //case 3: Blue_3(); break;
            //case 4: Blue_4(); break;
            //case 5: Blue_5(); break;
        }
    }

    void GoNextYellow(int lineIndex) {
        Debug.Log("GoNextYellow: " + lineIndex.ToString());
        switch(lineIndex) {
            //case 1: Yellow_1(); break;
            case 2: Yellow_4(); break;
            //case 3: Yellow_3(); break;
            //case 4: Yellow_4(); break;
            //case 5: Yellow_5(); break;
        }
    }

    bool IsPosOnFirstStation(float zpos) {
        return zpos < ZDivision;
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.magenta;
        var divpos = transform.position;
        divpos.z = ZDivision;
        Gizmos.DrawWireSphere(divpos, 0.5f);
    }

    [ContextMenu("Teste 1!")]
    public void TESTE1() {
        GoNextRed(1);
    }
}
