using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField]
    private Transform worldOrigin;

    private Vector3 startPosition;

    private void Awake()
    {
        startPosition = transform.position;
    }

    public void Activate()
    {
        transform.position = worldOrigin.position;
    }

    public void Deactivate()
    {
        transform.position = startPosition;
    }
}
