using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventWall : MonoBehaviour
{
    void Start()
    {
        EventWallOpen();
    }

    void Update()
    {
        EventWallOpen();
    }

    void EventWallOpen()
    {
        if (Database.instance.playerStatus.Level >= 10)
        {
            Destroy(gameObject);
        }
    }
}