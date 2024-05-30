using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private InputActionReference teleportAction;

    private void Awake()
    {
        teleportAction.action.performed += OnTeleportAction;
    }

    private void OnTeleportAction(InputAction.CallbackContext _)
    {
        TeleportPlayer();
    }

    private void TeleportPlayer()
    {
        RoomsManager.Instance.ChangeRoom();
        Debug.Log("teleport");
    }
}
