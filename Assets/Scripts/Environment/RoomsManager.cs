using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class RoomsManager : Singleton<RoomsManager>
{
    [Header("Refrences")]
    [SerializeField]
    private Room firstRoom;
    [SerializeField]
    private Room secondRoom;
    [SerializeField]
    private Volume fadeVolume;

    [Header("Config")]
    [SerializeField]
    private float fadeDuration;

    public event Action RoomChangedEvent;

    private bool firstRoomActive = true;

    private void Start()
    {
        firstRoom.Activate();
    }

    public void ChangeRoom()
    {
        StartCoroutine(ChangeRoomCoroutine());
    }

    private IEnumerator ChangeRoomCoroutine()
    {
        float fadeOutEndTimestamp = Time.time + fadeDuration;
        while (Time.time < fadeOutEndTimestamp)
        {
            fadeVolume.weight = 1 - (fadeOutEndTimestamp - Time.time) / fadeDuration;
            yield return null;
        }
        fadeVolume.weight = 1;
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
        RoomChangedEvent?.Invoke();
        float fadeInEndTimestamp = Time.time + fadeDuration;
        while (Time.time < fadeInEndTimestamp)
        {
            fadeVolume.weight = (fadeInEndTimestamp - Time.time) / fadeDuration;
            yield return null;
        }
        fadeVolume.weight = 0;
    }
}
