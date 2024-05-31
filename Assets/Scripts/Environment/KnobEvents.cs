using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KnobEvents : MonoBehaviour
{
    [SerializeField]
    private UnityEvent MaxValueReachedEvent;
    [SerializeField]
    private UnityEvent MinValueReachedEvent;
    [SerializeField]
    private UnityEvent NormalValueReachedEvent;
    [SerializeField]
    private UnityEvent CentralValueReachedEvent;

    [SerializeField]
    private RangeBoundariesFloat centralValueRange;

    private void Start()
    {
        XRKnob knob = GetComponent<XRKnob>();
        knob.onValueChange.AddListener(OnKnobValueChanged);
    }

    private void OnKnobValueChanged(float value)
    {
        if (centralValueRange.IsBetween(value))
        {
            CentralValueReachedEvent.Invoke();
            return;
        }
        if (value == 1)
        {
            MaxValueReachedEvent.Invoke();
            return;
        }
        if (value == 0)
        {
            MinValueReachedEvent.Invoke();
            return;
        }
        NormalValueReachedEvent.Invoke();
    }
}
