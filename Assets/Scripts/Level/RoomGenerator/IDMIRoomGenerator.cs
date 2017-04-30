using System.Collections.Generic;
using Assets.Scripts.Level.LevelDTO;

public interface IDMIRoomGenerator
{
    void generate(int seed, IEnumerable<RoomMetaData> roomList);
}
