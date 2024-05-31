using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Resource")]
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

    public event System.Action<Resource> ValueChanged;
    public event System.Action<Resource> ValueReachedBound;

    [SerializeField]
    [ReadOnly]
    private SerializedDictionary<GameObject, float> additionalSpeedComponents;
    [SerializeField]
    [ReadOnly]
    private float additionalSpeed = 0;

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

    public void AddToAdditionalSpeed(GameObject setter, float additionalSpeed)
    {
        additionalSpeedComponents[setter] = additionalSpeed;
        this.additionalSpeed = additionalSpeedComponents.Values.Sum();
    }

    public void AddToValue(float amount)
    {
        Value += amount;
    }

    public void ReverseDiraction()
    {
        direction *= -1;
    }

    public void CopyValues(Resource resource)
    {
        Value = resource.Value;
        changeSpeed = resource.changeSpeed;
        direction = resource.direction;
        additionalSpeed = 0;
        additionalSpeedComponents.Clear();
    }

    private void OnValidate()
    {
        if (value != Value)
        {
            Value = value;
        }
    }
}
