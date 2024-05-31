using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClone : MonoBehaviour
{
    [SerializeField]
    private XRPushButton original;

    private void Start()
    {
        original.ButtonHeightChangedEvent += OnButtonHeightChanged;
    }

    private void OnButtonHeightChanged(float height)
    {
        transform.localPosition = Vector3.up * height;
    }
}
