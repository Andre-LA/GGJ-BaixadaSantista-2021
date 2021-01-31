using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectTrLerp : MonoBehaviour
{
    public RectTransform target;

    [System.Serializable]
    public struct AnchorDuple {
        public Vector2 min, max;
    }

    public AnchorDuple anchorsFrom, anchorsTo;
    public AnimationCurve curve;

    public float speed;
    public bool reversed;

    float t;

    void Update() {
        t = Mathf.Clamp01(t + Time.deltaTime * speed * (reversed ? -1 : 1));
        Lerp(t);
    }

    public void Lerp(float T) {
        var tmin = target.anchorMin;
        var tmax = target.anchorMax;

        tmin = Vector2.Lerp(anchorsFrom.min, anchorsTo.min, curve.Evaluate(T));
        tmax = Vector2.Lerp(anchorsFrom.max, anchorsTo.max, curve.Evaluate(T));

        target.anchorMin = tmin;
        target.anchorMax = tmax;
    }
}
