using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class AnimationCurveExtensions
{
    public static float EvaluateUnclamped(this AnimationCurve curve, float time)
    {
        Keyframe last = curve.keys.Last();
        float res = last.value;
        if (time <= last.time)
        {
            res = curve.Evaluate(time);
        }
        return res;
    }
}
