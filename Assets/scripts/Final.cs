using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Final : MonoBehaviour
{
    public Image finalImg;
    public float t, speed;
    public AnimationCurve curve;

    void Update() {
        t += Time.deltaTime * speed;
        Color cor = finalImg.color;
        cor.a = curve.Evaluate(t);
        finalImg.color = cor;
    }
}
