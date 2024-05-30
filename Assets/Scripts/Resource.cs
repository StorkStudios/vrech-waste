using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : ScriptableObjectSingleton<Resource>
{
    const float min = 0;
    const float max = 100;

    [Range(min, max)]
    [SerializeField]
    private float value;
    [SerializeField]
    private float changeSpeed;

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

    public void UpdateValue(float deltaTime)
    {
        Value += changeSpeed * deltaTime;
    }

    public void SetChangeSpeed(float changeSpeed)
    {
        this.changeSpeed = changeSpeed;
    }

    public void AddToValue(float amount)
    {
        Value += amount;
    }
}
