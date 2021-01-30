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

    public bool[] puzzleStates = new bool[4];
    public int currentPuzzle;

    bool inTransition;

    void Awake() {
        if (Instance != null)
            Debug.LogWarning("Instance is being overwritten!!");
        Instance = this;
    }

#region Routes
    // RED
    public void Red_1() {
        stationLine1.SetSubwaySettings(SubwayStation.LineColor.Red, 1, 20);
        stationLine2.SetSubwaySettings(SubwayStation.LineColor.Yellow, 1, 20);
    }

    public void Red_2() {
        stationLine1.SetSubwaySettings(SubwayStation.LineColor.Red, 2, 20);
        stationLine2.SetSubwaySettings(SubwayStation.LineColor.Blue, 3, 20);
    }

    public void Red_3() {
        stationLine1.SetSubwaySettings(SubwayStation.LineColor.Red, 3, 20);
        stationLine2.SetSubwaySettings(SubwayStation.LineColor.Blue, 4, 20);
    }

    public void Red_4() {
        stationLine1.SetSubwaySettings(SubwayStation.LineColor.Red, 4, 20);
        stationLine2.SetSubwaySettings(SubwayStation.LineColor.Yellow, 3, 20);
    }

    // BLUE
    public void Blue_1() {
        stationLine1.SetSubwaySettings(SubwayStation.LineColor.Blue, 1, 20);
        stationLine2.SetSubwaySettings(SubwayStation.LineColor.Yellow, 2, 20);
    }

    public void Blue_2() {
        stationLine1.SetSubwaySettings(SubwayStation.LineColor.Blue, 2, 20);
        stationLine2.SetSubwaySettings(SubwayStation.LineColor.Yellow, 4, 20);
    }

    public void Blue_3() {
        Red_2();
    }

    public void Blue_4() {
        Red_3();
    }

    // BLUE
    public void Yellow_1() {
        Red_1();
    }

    public void Yellow_2() {
        Blue_1();
    }

    public void Yellow_3() {
        Red_4();
    }

    public void Yellow_4() {
        Blue_2();
    }
#endregion

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
            case 2: Red_3(); break;
            case 3: Red_4(); break;
            case 4: Red_1(); break;
        }
    }

    void GoNextBlue(int lineIndex) {
        Debug.Log("GoNextRed: " + lineIndex.ToString());

        switch(lineIndex) {
            case 1: Blue_2(); break;
            case 2: Blue_3(); break;
            case 3: Blue_4(); break;
            case 4: Blue_1(); break;
        }
    }

    void GoNextYellow(int lineIndex) {
        Debug.Log("GoNextRed: " + lineIndex.ToString());

        switch(lineIndex) {
            case 1: Yellow_2(); break;
            case 2: Yellow_3(); break;
            case 3: Yellow_4(); break;
            case 4: Yellow_1(); break;
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

    public void Interacted(RaycastHit hit) {
        var puzzle = hit.transform.GetComponent<Puzzle>();
        if (puzzle != null) {
            puzzleStates[puzzle.puzzleIndex] = true;
            PhoneMessage.Instance.Sing();
        }
    }

    public void PuzzleSolved(int i) {
        puzzleStates[i] = true;
    }

    public bool CanExit() {
        for (int i = 0; i < puzzleStates.Length; i++) {
            if (!puzzleStates[i])
                return false;
        }
        return true;
    }

    public void GameExit() {
        Debug.Log("Jogo terminou!");
    }
}
