using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private InputActionReference teleportAction;

    private CharacterControllerDriver characterControllerDriver;
    private TeleportationProvider teleportationProvider;

    private void Awake()
    {
        teleportAction.action.performed += OnTeleportAction;

        characterControllerDriver = GetComponent<CharacterControllerDriver>();
        teleportationProvider = GetComponentInChildren<TeleportationProvider>();
    }

    private void OnTeleportAction(InputAction.CallbackContext _)
    {
        TeleportPlayer();
    }

    private void TeleportPlayer()
    {
        RoomsManager.Instance.ChangeRoom();
        TeleportRequest teleportRequest = new TeleportRequest();
        teleportRequest.destinationPosition = Vector3.zero;
        teleportationProvider.QueueTeleportRequest(teleportRequest);
    }
}
