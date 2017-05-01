using Assets.Scripts.Level.LevelDTO;
using Assets.Scripts.Util;
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
    //[Inject]
    //IDMITilesGenerator tilesGenerator;
    [Inject]
    DMISettingsInstaller.LevelSettings _levelSettings;


    internal TileEnum[,] generate(int seed)
    {
        Debug.Log("Using seed:" + seed);
        IntVector2 size = setLevelSize(seed);

        IEnumerable<RoomMetaData> roomList;
        Debug.Log("Generating level...");
        roomList = levelGenerator.generate(seed, size);

        MetaTileEnum[,] metaTiles = new MetaTileEnum[size.x, size.y];
        Debug.Log("Generating rooms...");
        roomGenerator.generate(seed, roomList, ref metaTiles);

        TileEnum[,] finalTiles = new TileEnum[size.x, size.y];
        //TODO:
        //finalTiles = tilesGenerator.generate(metaTiles);

        return finalTiles;
    }

    private IntVector2 setLevelSize(int seed)
    {
        double mean = (_levelSettings.minLevelSize + _levelSettings.maxLevelSize) / 2;
        float sizeX = (float) GaussianGenerator.getGaussian(mean, _levelSettings.levelSizeVariance, seed);
        float sizeY = (float) GaussianGenerator.getGaussian(mean, _levelSettings.levelSizeVariance, seed);
        int finalSizeX = (int) Math.Round(Mathf.Clamp(sizeX, _levelSettings.minLevelSize, _levelSettings.maxLevelSize));
        int finalSizeY = (int) Math.Round(Mathf.Clamp(sizeY, _levelSettings.minLevelSize, _levelSettings.maxLevelSize));
        return new IntVector2(finalSizeX, finalSizeY);
    }
}
