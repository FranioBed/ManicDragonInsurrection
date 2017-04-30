using Assets.Scripts.Level.LevelDTO;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LevelGeneratorService {

    [Inject]
    IDMILevelGenerator levelGenerator;
    [Inject]
    IDMIRoomGenerator roomGenerator;


    internal void generate(int seed)
    {
        IEnumerable<RoomMetaData> roomList;
        Debug.Log("Generating level...");
        roomList = levelGenerator.generate(seed);
        Debug.Log("Generating rooms...");
        roomGenerator.generate(seed, roomList);
    }
}
