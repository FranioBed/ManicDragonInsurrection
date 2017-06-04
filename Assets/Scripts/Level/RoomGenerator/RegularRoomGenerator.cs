using Zenject;
using UnityEngine;
using Assets.Scripts.Level.LevelDTO;
using System;
using System.Collections.Generic;
using Assets.Scripts.Util;

public class RegularRoomGenerator : IIRoomGenerator {

    [Inject]
    SettingsInstaller.RoomSettings _settings;

    public void generate(int seed, IEnumerable<RoomMetaData> roomList,
        ref MetaTileEnum[,] metaTile, ref ItemOnTileEnum[,] itemsOnTiles)
    {
        System.Random rng = new System.Random(seed);

        Debug.Log("Using RegularRoomGenerator");
        fillWithWalls(ref metaTile);
        fillWithNulls(ref itemsOnTiles);
        foreach (RoomMetaData room in roomList)
        {
            carveRoom(ref metaTile, room, rng);
            insertDoors(ref metaTile, room);
            if (_settings.useFancyLayouts)
                applyFancyLayout();
            if (room.hasStart)
                placeStart(ref metaTile, ref itemsOnTiles, room, rng);
            if (room.hasExit)
                placeExit(ref metaTile, ref itemsOnTiles, room, rng);
            for (int i = 0; i < room.enemyCount; i++)
                placeEnemy(ref metaTile, ref itemsOnTiles, room, rng);
            for (int i = 0; i < room.chestCount; i++)
                placeChest(ref metaTile, ref itemsOnTiles, room, rng);
        }
        Debug.Log("Rooms generated successfully");
    }

    private void fillWithWalls(ref MetaTileEnum[,] metaTile)
    {
        for (int m = 0; m < metaTile.GetLength(0); m++)
            for (int n = 0; n < metaTile.GetLength(1); n++)
                metaTile[m, n] = MetaTileEnum.WALL;
    }

    private void fillWithNulls(ref ItemOnTileEnum[,] itemsOnTiles)
    {
        for (int m = 0; m < itemsOnTiles.GetLength(0); m++)
            for (int n = 0; n < itemsOnTiles.GetLength(1); n++)
                itemsOnTiles[m, n] = ItemOnTileEnum.NULL;
    }

    private void insertDoors(ref MetaTileEnum[,] metaTile, RoomMetaData room)
    {
        foreach (IntVector2 doorLocation in room.doorLocations)
            metaTile[room.position.x + doorLocation.x, room.position.y + doorLocation.y] = MetaTileEnum.DOOR;
    }

    private void carveRoom(ref MetaTileEnum[,] metaTile, RoomMetaData room, System.Random rng)
    {
        MetaTileEnum floorTile = getFloorTile(rng);
        for (int m = room.position.x + 1; m < room.position.x + room.size.x - 1; m++)
            for (int n = room.position.y + 1; n < room.position.y + room.size.y - 1; n++)
                //we leave one empty tile in each direction for walls
                metaTile[m, n] = floorTile;
    }

    private void placeExit(ref MetaTileEnum[,] metaTile, ref ItemOnTileEnum[,] itemsOnTiles, 
        RoomMetaData room, System.Random rng)
    {
        placeItemOnFloor(ref metaTile, ref itemsOnTiles, room, ItemOnTileEnum.EXIT, rng);
    }

    private void placeStart(ref MetaTileEnum[,] metaTile, ref ItemOnTileEnum[,] itemsOnTiles,
        RoomMetaData room, System.Random rng)
    {
        placeItemOnFloor(ref metaTile, ref itemsOnTiles, room, ItemOnTileEnum.STARTPOS, rng);
    }

    private void placeEnemy(ref MetaTileEnum[,] metaTile, ref ItemOnTileEnum[,] itemsOnTiles, RoomMetaData room, System.Random rng)
    {
        ItemOnTileEnum enemyToPlace;
        switch (rng.Next()%3) {
            case 0:
                enemyToPlace = ItemOnTileEnum.ENEMY_1;
                break;
            case 1:
                enemyToPlace = ItemOnTileEnum.ENEMY_2;
                break;
            default:
                enemyToPlace = ItemOnTileEnum.ENEMY_3;
                break;
        }
        placeItemOnFloor(ref metaTile, ref itemsOnTiles, room, enemyToPlace, rng);
    }

    private void placeChest(ref MetaTileEnum[,] metaTile, ref ItemOnTileEnum[,] itemsOnTiles, RoomMetaData room, System.Random rng)
    {
        placeItemOnFloor(ref metaTile, ref itemsOnTiles, room, ItemOnTileEnum.CHEST, rng);
    }

    private void placeItemOnFloor(ref MetaTileEnum[,] metaTile, ref ItemOnTileEnum[,] itemsOnTiles, 
        RoomMetaData room, ItemOnTileEnum whatToPlace, System.Random rng)
    {
        IntVector2 attempt;
        do
        {
            attempt = new IntVector2(
             rng.Next(room.position.x + 1, room.position.x + room.size.x - 2),
             rng.Next(room.position.y + 1, room.position.y + room.size.y - 2));
        }
        while (!(isFloor(metaTile[attempt.x, attempt.y]) && itemsOnTiles[attempt.x, attempt.y].Equals(ItemOnTileEnum.NULL)));
        itemsOnTiles[attempt.x, attempt.y] = whatToPlace;
    }

    private void applyFancyLayout()
    {
        //TODO:
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
