using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomsManager : Singleton<RoomsManager>
{
    [SerializeField]
    private Room firstRoom;
    [SerializeField]
    private Room secondRoom;

    private bool firstRoomActive = true;

    private void Start()
    {
        firstRoom.Activate();
    }

    public void ChangeRoom()
    {
        if (firstRoomActive)
        {
            firstRoom.Deactivate();
            secondRoom.Activate();
            firstRoomActive = false;
        }
        else
        {
            secondRoom.Deactivate();
            firstRoom.Activate();
            firstRoomActive = true;
        }
    }
}
