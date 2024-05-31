using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRoationController : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private GameObject objectToRotate;

    [Header("Config")]
    [SerializeField]
    private RangeBoundariesFloat angleRange;

    [Header("Debug")]
    [SerializeField]
    [Range(0, 1)]
    private float value;

    private void OnValidate()
    {
        ChangeValue(value, 1);
    }

    public void ChangeValueInverted(float current, float maxVal)
    {
        ChangeValue((maxVal - current), maxVal);
    }

    public void ChangeValue(float current, float maxVal)
    {
        float t = current / maxVal;
        Vector3 angles = objectToRotate.transform.localEulerAngles;
        angles.z = angleRange.Lerp(t);
        objectToRotate.transform.localEulerAngles = angles;
    }
}
