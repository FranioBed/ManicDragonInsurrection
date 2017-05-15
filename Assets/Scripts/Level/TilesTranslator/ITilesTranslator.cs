using Assets.Scripts.Level.LevelDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Level.TilesTranslator
{
    public interface ITilesTranslator
    {
        TileEnum[,] translate(MetaTileEnum[,] metaTiles);
    }
}
