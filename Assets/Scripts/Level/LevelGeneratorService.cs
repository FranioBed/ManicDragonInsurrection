using Assets.Scripts.Level.LevelDTO;
using Assets.Scripts.Level.TilesTranslator;
using Assets.Scripts.Util;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LevelGeneratorService {

    [Inject]
    ILevelGenerator _levelGenerator;
    [Inject]
    IIRoomGenerator _roomGenerator;
    [Inject]
    ITilesTranslator _tilesGenerator;
    [Inject]
    SettingsInstaller.LevelSettings _levelSettings;

    public LevelInfo generate(int seed)
    {
        LevelInfo levelInfo = new LevelInfo();

        Debug.Log("Using seed:" + seed);
        IntVector2 size = setLevelSize(seed);

        IEnumerable<RoomMetaData> roomList;
        Debug.Log("Generating level...");
        roomList = _levelGenerator.generate(seed, ref size);

        MetaTileEnum[,] metaTiles = new MetaTileEnum[size.x, size.y];
        ItemOnTileEnum[,] itemsOnTiles = new ItemOnTileEnum[size.x, size.y];
        Debug.Log("Generating rooms...");
        _roomGenerator.generate(seed, roomList, ref metaTiles, ref itemsOnTiles);

        Debug.Log("Translating metatiles to final tiles...");
        TileEnum[,] finalTiles = _tilesGenerator.translate(metaTiles);

        levelInfo.tiles = finalTiles;
        levelInfo.itemsOnTiles = itemsOnTiles;
        return levelInfo;
    }

    private IntVector2 setLevelSize(int seed)
    {
        System.Random rand = new System.Random(seed);
        double mean = (_levelSettings.minLevelSize + _levelSettings.maxLevelSize) / 2;
        float sizeX = (float) GaussianGenerator.getGaussian(mean, _levelSettings.levelSizeVariance, rand);
        float sizeY = (float) GaussianGenerator.getGaussian(mean, _levelSettings.levelSizeVariance, rand);
        int finalSizeX = (int) Math.Round(Mathf.Clamp(sizeX, _levelSettings.minLevelSize, _levelSettings.maxLevelSize));
        int finalSizeY = (int) Math.Round(Mathf.Clamp(sizeY, _levelSettings.minLevelSize, _levelSettings.maxLevelSize));
        return new IntVector2(finalSizeX, finalSizeY);
    }
}
