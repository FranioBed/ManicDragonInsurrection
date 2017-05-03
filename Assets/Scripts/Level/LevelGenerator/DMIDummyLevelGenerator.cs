using Assets.Scripts.Level.LevelDTO;
using Assets.Scripts.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DMIDummyLevelGenerator : IDMILevelGenerator
{

    //please use settings in real generation, for dummy generator it can be skipped
    //[Inject]
    //DMISettingsInstaller.LevelSettings _settings;

    //mock data for room generator
    public IEnumerable<RoomMetaData> generate(int seed, IntVector2 levelSize)
    {
        if ((levelSize.x < 20) || (levelSize.y < 20)) {
            throw new NotSupportedException("Can mock only on 20x20 or greater map");
        }

        IList<RoomMetaData> rooms = new List<RoomMetaData>();

        RoomMetaData room1 = new RoomMetaData {
            position = new IntVector2(2, 2),
            size = new IntVector2(5, 6),
            doorLocations = new List<IntVector2>
            {
                new IntVector2(4, 4)
            },
            hasExit = false,
            hasStart = true
        };

        RoomMetaData room2 = new RoomMetaData
        {
            position = new IntVector2(7, 4),
            size = new IntVector2(6, 8),
            doorLocations = new List<IntVector2>
            {
                new IntVector2(0, 2),
                new IntVector2(2, 7)
            },
            hasExit = true,
            hasStart = false
        };

        RoomMetaData room3 = new RoomMetaData
        {
            position = new IntVector2(8, 12),
            size = new IntVector2(4, 4),
            doorLocations = new List<IntVector2>
            {
                new IntVector2(1, 0)
            },
            hasExit = false,
            hasStart = false
        };

        rooms.Add(room1);
        rooms.Add(room2);
        rooms.Add(room3);
        return rooms;
    }
}
