using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private InputActionReference teleportAction;

    private TeleportationProvider teleportationProvider;

    private void Awake()
    {
        teleportAction.action.Enable();
        teleportAction.action.performed += OnChangeRoomAction;

        teleportationProvider = GetComponentInChildren<TeleportationProvider>();

        RoomsManager.Instance.RoomChangedEvent += OnRoomChanged;
    }

    private void OnChangeRoomAction(InputAction.CallbackContext _)
    {
        ChangeRoom();
    }

    private void ChangeRoom()
    {
        RoomsManager.Instance.ChangeRoom();
    }

    private void OnRoomChanged()
    {
        TeleportPlayerToOrigin();
    }

    private void TeleportPlayerToOrigin()
    {
        TeleportRequest teleportRequest = new TeleportRequest();
        teleportRequest.destinationPosition = Vector3.zero;
        teleportationProvider.QueueTeleportRequest(teleportRequest);
    }
}
