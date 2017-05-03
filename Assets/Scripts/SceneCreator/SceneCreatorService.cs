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

        public void Create(LevelInfo levelInfo)
        {
            cleanUp();
            instantiateTiles(levelInfo.tiles);
            instantiateItems(levelInfo.itemsOnTiles);
        }
        
        private void instantiateTiles(TileEnum[,] tiles)
        {
            _tilesSpawner.spawnMap(tiles);
        }

        private void instantiateItems(ItemOnTileEnum[,] itemsOnTiles)
        {
            Debug.LogError("INSTANTIATING TILES NOT IMPLEMENTED");
        }

        public void cleanUp()
        {
            Debug.LogError("CLEANING UP NOT IMPLEMENTED");
        }

    }
}
