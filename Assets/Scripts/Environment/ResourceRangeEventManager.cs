using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ResourceRangeEventManager : MonoBehaviour
{
    [Serializable]
    private class ResourceThresholdEvent
    {
        [SerializeField]
        private float valueFrom;
        [SerializeField]
        private UnityEvent thresholdEvent;

        public float ValueFrom => valueFrom;
        public UnityEvent ThresholdEvent => thresholdEvent;
    }

    [SerializeField]
    private List<ResourceThresholdEvent> events;
    [SerializeField]
    private Resource resource;

    private ResourceThresholdEvent lastCalled = null;

    private void Start()
    {
        ResourceManager.Instance.ResourceChange += OnResourceChanged;
    }

    private void OnResourceChanged(Resource resource)
    {
        if (resource != this.resource)
        {
            return;
        }
        ResourceThresholdEvent eventToCall = events[0];
        foreach (ResourceThresholdEvent e in events)
        {
            if (e.ValueFrom <= resource.Value)
            {
                eventToCall = e;
            }
            else
            {
                break;
            }
        }
        if (eventToCall != lastCalled)
        {
            eventToCall.ThresholdEvent.Invoke();
            lastCalled = eventToCall;
        }
    }
}
