using Assets.Scripts.Level.LevelDTO;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DMIDummyLevelGenerator : IDMILevelGenerator
{

    //mock data for room generator
    public IEnumerable<RoomMetaData> generate(int seed)
    {
        IList<RoomMetaData> rooms = new List<RoomMetaData>();

        RoomMetaData room1 = new RoomMetaData {
            position = new Vector2(2, 2),
            size = new Vector2(5, 6),
            doorLocations = new List<Vector2>
            {
                new Vector2(5, 4)
            },
            hasExit = false
        };

        RoomMetaData room2 = new RoomMetaData
        {
            position = new Vector2(7, 4),
            size = new Vector2(6, 8),
            doorLocations = new List<Vector2>
            {
                new Vector2(0, 2)
            },
            hasExit = true
        };

        rooms.Add(room1);
        rooms.Add(room2);
        return rooms;
    }
}
