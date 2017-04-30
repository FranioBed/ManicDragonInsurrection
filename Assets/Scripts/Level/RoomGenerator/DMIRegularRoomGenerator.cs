using Zenject;
using UnityEngine;
using Assets.Scripts.Level.LevelDTO;
using System;
using System.Collections.Generic;

public class DMIRegularRoomGenerator : IDMIRoomGenerator {

    [Inject]
    DMISettingsInstaller.RoomSettings _settings;

    public void generate(int seed, IEnumerable<RoomMetaData> roomList)
    {
        Debug.Log("works");
        Debug.Log("seed:" + seed);
    }
    //do stuff
}
