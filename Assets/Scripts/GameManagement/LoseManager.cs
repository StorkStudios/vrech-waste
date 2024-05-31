using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseManager : Singleton<LoseManager>
{
    public event System.Action LooseEvent;

    public bool Lost
    {
        get => lost;
        private set
        {
            if (lost != value && value == true)
            {
                LooseEvent?.Invoke();
                lost = value;
            }
        }
    }

    [SerializeField]
    private GameObject gameOverCanvas;
    [SerializeField]
    private GameObject environment;

    [SerializeField]
    [ReadOnly]
    private bool lost = false;

    private void Start()
    {
        ResourceManager.Instance.ResourceReachedBound += OnLoose;
    }

    private void OnLoose(Resource _)
    {
        Lost = true;
        environment.SetActive(false);
        gameOverCanvas.SetActive(true);
    }
}
