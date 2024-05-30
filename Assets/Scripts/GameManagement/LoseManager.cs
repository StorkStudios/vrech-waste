using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseManager : Singleton<LoseManager>
{
    public event System.Action OnLoose;

    public bool Lost
    {
        get => lost;
        private set
        {
            if (lost != value && value == true)
            {
                OnLoose?.Invoke();
                lost = value;
            }
        }
    }
    private bool lost = false;

    private void Start()
    {
        ResourceManager.Instance.ResourceReachedBound += (_) => Lost = true;
    }
}
