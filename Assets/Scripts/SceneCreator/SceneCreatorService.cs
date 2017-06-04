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
        [Inject]
        EnemiesSpawner _enemiesSpanwer;
        [Inject]
        LevelItemMarker _itemsMarker;

        public IList<Marker> Create(LevelInfo levelInfo)
        {
            cleanUp();
            _tilesSpawner.spawn(levelInfo.tiles);
            _itemsSpawner.spawn(levelInfo.itemsOnTiles);
            _enemiesSpanwer.spawn(levelInfo.itemsOnTiles);
            IList<Marker> markers = _itemsMarker.getMarkers(levelInfo.itemsOnTiles);
            return markers;
        }
        
        public void cleanUp()
        {
            Debug.LogError("CLEANING UP NOT IMPLEMENTED");
        }

    }
}
