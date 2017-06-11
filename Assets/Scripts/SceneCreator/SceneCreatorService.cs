using System;
using Assets.Scripts.Level.LevelDTO;
using UnityEngine;
using Zenject;
using System.Collections.Generic;
using System.Linq;

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

        public LevelGameObjectsHolder Create(LevelGameObjectsHolder oldHolder, LevelInfo levelInfo)
        {
            cleanUp(oldHolder);
            LevelGameObjectsHolder holder = create(levelInfo);
            return holder;
        }

        private LevelGameObjectsHolder create(LevelInfo levelInfo)
        {
            LevelGameObjectsHolder holder = new LevelGameObjectsHolder();
            holder.tiles = _tilesSpawner.spawn(levelInfo.tiles);
            holder.items = _itemsSpawner.spawn(levelInfo.itemsOnTiles);
            holder.enemies = _enemiesSpanwer.spawn(levelInfo.itemsOnTiles);
            List<Marker> markers = _itemsMarker.getMarkers(levelInfo.itemsOnTiles);
            holder.startPos = markers.Where<Marker>(m => ItemOnTileEnum.STARTPOS.Equals(m.itemType)).First();
            return holder;
        }

        private void cleanUp(LevelGameObjectsHolder oldHolder)
        {
            removeGameObjects(oldHolder.tiles);
            removeGameObjects(oldHolder.items);
            removeGameObjects(oldHolder.enemies);
        }

        private void removeGameObjects(IList<GameObject> toRemoveList)
        {
            foreach (GameObject toRemove in toRemoveList)
            {
                GameObject.Destroy(toRemove);
            }
        }

    }
}
