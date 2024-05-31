using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverResourceChanger : MonoBehaviour
{
    [Serializable]
    private class LeverResourceEffect
    {
        [SerializeField]
        private Resource resource;
        [SerializeField]
        private float effect;

        public Resource Resource => resource;
        public float Effect => effect;
    }

    [SerializeField]
    private List<LeverResourceEffect> leverResourceEffects;

    private void Start()
    {
        XRLever lever = GetComponent<XRLever>();
        lever.LeverValueChangeEvent.AddListener(OnLeverValueChanged);
    }

    private void OnLeverValueChanged(float value)
    {
        foreach (var effect in leverResourceEffects)
        {
            effect.Resource.SetAdditionalSpeed(value * effect.Effect);
        }
    }
}
