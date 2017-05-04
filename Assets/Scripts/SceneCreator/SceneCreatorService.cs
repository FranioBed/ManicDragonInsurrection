using System;
using Assets.Scripts.Level.LevelDTO;
using UnityEngine;
using Zenject;
using System.Collections.Generic;

namespace Assets.Scripts.SceneCreator
{
    public class SceneCreatorService
    {
        [Inject]
        LevelTilesSpawner _tilesSpawner;
        [Inject]
        LevelItemsSpawner _itemsSpawner;

        public void Create(LevelInfo levelInfo)
        {
            cleanUp();
            instantiateTiles(levelInfo.tiles);
            instantiateItems(levelInfo.itemsOnTiles);
        }
        
        private void instantiateTiles(TileEnum[,] tiles)
        {
            _tilesSpawner.spawn(tiles);
        }

        private void instantiateItems(ItemOnTileEnum[,] itemsOnTiles)
        {
            _itemsSpawner.spawn(itemsOnTiles);
        }

        public void cleanUp()
        {
            Debug.LogError("CLEANING UP NOT IMPLEMENTED");
        }

    }
}
