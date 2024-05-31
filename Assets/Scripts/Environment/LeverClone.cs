using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverClone : MonoBehaviour
{
    [SerializeField]
    private XRLever original;

    private XRLever lever;

    private void Start()
    {
        lever = GetComponent<XRLever>();
        original.LeverValueChangeEvent.AddListener(OnOriginalLeverValueChanged);
    }

    private void OnOriginalLeverValueChanged(float value)
    {
        lever.value = value;
    }
}
