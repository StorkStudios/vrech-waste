using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EventSpawner : MonoBehaviour
{
    [SerializeField]
    private AnimationCurve maxEventDurationInTime;
    [SerializeField]
    private AnimationCurve minEventDurationInTime;
    [SerializeField]
    private AnimationCurve eventResourceSpeedInTime;
    [SerializeField]
    private AnimationCurve resourceSpeedInTime;
    [SerializeField]
    private float startUpDuration;

    private ResourceManager resourceManager;
    private LoseManager loseManager;

    private float time = 0;
    private float eventEndTimestamp = 0;

    private Resource currentEventResource;

    private bool started = false;

    private void Start()
    {
        resourceManager = ResourceManager.Instance;
        loseManager = LoseManager.Instance;
    }

    private void Update()
    {
        if (loseManager.Lost)
        {
            return;
        }

        time += Time.deltaTime;

        started = time > startUpDuration;

        if (started && time > eventEndTimestamp)
        {
            GenerateNewEvent();
        }

        float speed = resourceSpeedInTime.EvaluateUnclamped(time);
        float eventSpeed = eventResourceSpeedInTime.EvaluateUnclamped(time);
        
        foreach (var resource in resourceManager.Resources)
        {
            if (resource == currentEventResource)
            {
                resource.SetChangeSpeed(eventSpeed);
            }
            else
            {
                resource.SetChangeSpeed(speed);
            }
        }
    }

    private void GenerateNewEvent()
    {
        currentEventResource = resourceManager.GetRandomResource();

        if (Random.value < 0.5f)
        {
            currentEventResource.ReverseDiraction();
        }

        float min = minEventDurationInTime.EvaluateUnclamped(time);
        float max = maxEventDurationInTime.EvaluateUnclamped(time);

        eventEndTimestamp = time + Random.Range(min, max);
    }
}
