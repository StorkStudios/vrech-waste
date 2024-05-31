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
        OnKnobValueChange(knob.value);
    }

    private void OnKnobValueChange(float value)
    {
        negativeResource.AddToAdditionalSpeed(gameObject, ((1 - value) * 2 - 1) * speed);
        positiveResource.AddToAdditionalSpeed(gameObject, (value * 2 - 1) * speed);
    }
}
