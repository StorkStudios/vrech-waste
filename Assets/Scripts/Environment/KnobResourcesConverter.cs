using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnobResourcesConverter : MonoBehaviour
{
    [SerializeField]
    private Resource positiveResource;
    [SerializeField]
    private Resource negativeResource;
    [SerializeField]
    private float speed;

    private XRKnob knob;

    private void Start()
    {
        knob = GetComponent<XRKnob>();
        knob.onValueChange.AddListener(OnKnobValueChange);
    }

    private void OnKnobValueChange(float value)
    {
        positiveResource.SetAdditionalSpeed((value * 2 - 1) * speed);
        negativeResource.SetAdditionalSpeed(((1 - value) * 2 - 1) * speed);
    }
}
