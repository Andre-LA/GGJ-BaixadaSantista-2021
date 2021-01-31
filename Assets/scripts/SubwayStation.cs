using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubwayStation : MonoBehaviour
{
    public MeshRenderer lineColorMat1, lineColorMat2;
    public ShadowInitializer shadowInitializer;
    public Transform redEnviroments, blueEnviroments, yellowEnviroments;
    public Transform refTeleport;

    public CfgStationInfo stationInfo, otherStationInfo;

    public enum LineColor {
        Red = 0, Blue = 1, Yellow = 2
    }

    LineColor stationColor;
    int stationIndex;

    void SetLineColor(LineColor lineColor) {
        Color color = Color.white;

        switch (lineColor) {
            case LineColor.Red   : color = Color.red; break;
            case LineColor.Blue  : color = Color.blue; break;
            case LineColor.Yellow: color = Color.yellow; break;
        }

        lineColorMat1.material.color = color;
        lineColorMat2.material.color = color;
        stationColor = lineColor;
    }

    void AdjustShadowDensity(int shadowDensity) {
        shadowInitializer.GenerateShadows(shadowDensity);
    }

    void DeactivateEnviroments(Transform env) {
        for (int i = 0; i < env.childCount; ++i)
            env.GetChild(i).gameObject.SetActive(false);
    }

    public void SetSubwaySettings(
        LineColor lineColor,
        int lineIndex,
        int shadowDensity
    ) {
        Debug.Log("!!Subway Settings!! " + string.Concat(
            lineColor.ToString(), "_", lineIndex.ToString()
        ));

        stationIndex = lineIndex;
        shadowDensity = Random.Range(shadowDensity - 10, shadowDensity + 10);

        DeactivateEnviroments(redEnviroments);
        DeactivateEnviroments(blueEnviroments);
        DeactivateEnviroments(yellowEnviroments);

        Transform env = null;
        switch (lineColor) {
            case LineColor.Red   : env = redEnviroments.GetChild(stationIndex - 1); break;
            case LineColor.Blue  : env = blueEnviroments.GetChild(stationIndex - 1); break;
            case LineColor.Yellow: env = yellowEnviroments.GetChild(stationIndex - 1); break;
            default:
                env = transform;
                Debug.LogWarning("unknown LineColor");
                break;
        }
        env.gameObject.SetActive(true);

        SetLineColor(lineColor);
        AdjustShadowDensity(shadowDensity);

        stationInfo.SetSign(true, stationIndex - 1, (int)lineColor);
        otherStationInfo.SetSign(false, stationIndex - 1, (int)lineColor);
    }

    public LineColor GetLineColor() {
        return stationColor;
    }

    public int GetLineIndex() {
        return stationIndex;
    }
}
