using Zenject;
using UnityEngine;
using Assets.Scripts.Level.LevelDTO;
using System;
using System.Collections.Generic;
using Assets.Scripts.Util;

public class DMIRegularRoomGenerator : IDMIRoomGenerator {

    [Inject]
    DMISettingsInstaller.RoomSettings _settings;

    public void generate(int seed, IEnumerable<RoomMetaData> roomList, ref MetaTileEnum[,] metaTile)
    {
        System.Random rng = new System.Random(seed);

        Debug.Log("Using RegularRoomGenerator");
        fillWithWalls(ref metaTile);
        foreach (RoomMetaData room in roomList)
        {
            carveRoom(ref metaTile, room, rng);
            insertDoors(ref metaTile, room);
            if (_settings.useFancyLayouts)
                applyFancyLayout();
            if (room.hasStart)
                placeStart(ref metaTile, room, rng);
            if (room.hasExit)
                placeExit(ref metaTile, room, rng);
            //TODO:
            //if (room.enemiesCount > 0)
            //if (room.lootChests > 0)
            //etc.
        }
        Debug.Log("Rooms generated successfully");
    }

    private void fillWithWalls(ref MetaTileEnum[,] metaTile)
    {
        for (int m = 0; m < metaTile.GetLength(0); m++)
        {
            for (int n = 0; n < metaTile.GetLength(1); n++)
            {
                metaTile[m, n] = MetaTileEnum.WALL;
            }
        }
    }

    private void insertDoors(ref MetaTileEnum[,] metaTile, RoomMetaData room)
    {
        foreach (IntVector2 doorLocation in room.doorLocations)
        {
            metaTile[room.position.x + doorLocation.x, room.position.y + doorLocation.y] = MetaTileEnum.DOOR;
        }
    }

    private void carveRoom(ref MetaTileEnum[,] metaTile, RoomMetaData room, System.Random rng)
    {
        MetaTileEnum floorTile = getFloorTile(rng);
        for (int m = room.position.x + 1; m < room.position.x + room.size.x; m++)
        {
            for (int n = room.position.y + 1; n < room.position.y + room.size.y; n++)
            {
                //we leave one empty tile in each direction for walls
                metaTile[m, n] = floorTile;
            }
        }
    }

    private void placeExit(ref MetaTileEnum[,] metaTile, RoomMetaData room, System.Random rng)
    {
        placeItemOnFloor(ref metaTile, room, MetaTileEnum.EXIT, rng);
    }

    private void placeStart(ref MetaTileEnum[,] metaTile, RoomMetaData room, System.Random rng)
    {
        placeItemOnFloor(ref metaTile, room, MetaTileEnum.START, rng);
    }

    private void placeItemOnFloor(ref MetaTileEnum[,] metaTile, RoomMetaData room, MetaTileEnum whatToPlace, System.Random rng)
    {
        IntVector2 attempt;
        do
        {
            attempt = new IntVector2(
             rng.Next(room.position.x + 1, room.position.x + room.size.x - 1),
             rng.Next(room.position.y + 1, room.position.y + room.size.y - 1));
        }
        while (!isFloor(metaTile[attempt.x, attempt.y]));
        metaTile[attempt.x, attempt.y] = whatToPlace;
    }



    private void applyFancyLayout()
    {
        throw new NotImplementedException();
    }

    private bool isFloor(MetaTileEnum tile)
    {
        return tile.Equals(MetaTileEnum.FLOOR_1) ||
            tile.Equals(MetaTileEnum.FLOOR_2) ||
            tile.Equals(MetaTileEnum.FLOOR_3);
    }

    private MetaTileEnum getFloorTile(System.Random rng)
    {
        switch (rng.Next(1, 3))
        {
            case 1:
                return MetaTileEnum.FLOOR_1;
            case 2:
                return MetaTileEnum.FLOOR_2;
            default:
                return MetaTileEnum.FLOOR_3;
        }
    }
}
