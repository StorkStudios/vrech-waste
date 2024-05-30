using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : ScriptableObject
{
    const float min = 0;
    const float max = 100;

    [Range(min, max)]
    [SerializeField]
    private float value;
    [SerializeField]
    private float changeSpeed;
    [SerializeField]
    private int direction;
    [SerializeField]
    private float additionalSpeed;

    public event System.Action<Resource> ValueChanged;
    public event System.Action<Resource> ValueReachedBound;

    public float Value
    {
        get => value;
        set
        {
            this.value = value;
            if (this.value <= min || max <= this.value)
            {
                ValueReachedBound?.Invoke(this);
            }
            ValueChanged?.Invoke(this);
        }
    }

    public void UpdateResource(float deltaTime)
    {
        Value += (direction * changeSpeed + additionalSpeed) * deltaTime;
    }

    public void SetChangeSpeed(float changeSpeed)
    {
        this.changeSpeed = changeSpeed;
    }

    public void SetAdditionalSpeed(float additionalSpeed)
    {
        this.additionalSpeed = additionalSpeed;
    }

    public void AddToValue(float amount)
    {
        Value += amount;
    }

    public void ReverseDiraction()
    {
        direction *= -1;
    }
}
