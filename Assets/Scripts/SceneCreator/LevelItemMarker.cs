using System.Collections.Generic;
using Assets.Scripts.Level.LevelDTO;
using Assets.Scripts.Util;

namespace Assets.Scripts.SceneCreator
{

    class LevelItemMarker
    {
        private static HashSet<ItemOnTileEnum> allowedMarkers =
            new HashSet<ItemOnTileEnum> { ItemOnTileEnum.STARTPOS, ItemOnTileEnum.ENEMY_1, ItemOnTileEnum.ENEMY_2, ItemOnTileEnum.ENEMY_3 };

        public List<Marker> getMarkers(ItemOnTileEnum[,] itemsOnTiles)
        {
            List<Marker> markers = new List<Marker>();
            for (int m = 0; m < itemsOnTiles.GetLength(0); m++)
                for (int n = 0; n < itemsOnTiles.GetLength(1); n++)
                    if (allowedMarkers.Contains(itemsOnTiles[m, n]))
                        markers.Add(new Marker { position = new IntVector2(m, n), itemType = itemsOnTiles[m, n] });
            return markers;
        }
    }
}
