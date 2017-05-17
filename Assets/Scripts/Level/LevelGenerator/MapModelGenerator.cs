using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Level.LevelDTO;
using Assets.Scripts.Util;
using Zenject;
public class MapModelGenerator : ILevelGenerator
{
    [Inject]
    SettingsInstaller.LevelSettings _settings;
    int pathCount;
    int modelRoomCount;
    ModelRoom startRoom, endRoom;


    public IEnumerable<RoomMetaData> generate(int seed, ref IntVector2 levelSize)
    {
        pathCount = _settings.mapModelPathCount;
        modelRoomCount = _settings.mapModelRoomCount;
        ModelRoom.sizeMaxX = _settings.roomsSettings.maxRoomSizeX;
        ModelRoom.sizeMaxY = _settings.roomsSettings.maxRoomSizeY;
        ModelRoom.sizeMinX = _settings.roomsSettings.minRoomSizeX;
        ModelRoom.sizeMinY = _settings.roomsSettings.minRoomSizeY;




        Random.InitState(seed); ;
        List<ModelRoom> MapModelRoomList = generateMapModelRoomList();
        IList<RoomMetaData> rooms = new List<RoomMetaData>();

        IntVector2 maxPosition = new IntVector2(0, 0);
        IntVector2 minPosition = new IntVector2(0, 0);

        foreach (ModelRoom modelRoom in MapModelRoomList)
        {
            if (maxPosition.x < modelRoom.rightX)
                maxPosition.x = modelRoom.rightX;
            if (maxPosition.y < modelRoom.topY)
                maxPosition.y = modelRoom.topY;
            if (minPosition.x > modelRoom.leftX)
                minPosition.x = modelRoom.leftX;
            if (minPosition.y > modelRoom.bottomY)
                minPosition.y = modelRoom.bottomY;
        }

        levelSize.x = maxPosition.x - minPosition.x;
        levelSize.y = maxPosition.y - minPosition.y;

        foreach (ModelRoom modelRoom in MapModelRoomList)
        {
            RoomMetaData newRoom = new RoomMetaData();
            newRoom.position.x = modelRoom.leftX- minPosition.x;
            newRoom.position.y = modelRoom.bottomY - minPosition.y;
            newRoom.size.x = modelRoom.rightX - modelRoom.leftX+1;
            newRoom.size.y = modelRoom.topY - modelRoom.bottomY+1;

            List<IntVector2> doorList = new List<IntVector2>();
            foreach (ModelDoor door in modelRoom.doors)
            {
                doorList.Add(new IntVector2((int)door.startPosition.x - modelRoom.leftX, (int)door.startPosition.y - modelRoom.bottomY));
            }
            newRoom.doorLocations = doorList;

            if (modelRoom == startRoom)
                newRoom.hasStart = true;
            if (modelRoom == endRoom)
                newRoom.hasExit = true;
            rooms.Add(newRoom);
        }

        return rooms;
    }


    List<ModelRoom> generateMapModelRoomList()
    {
        List<ModelRoom> MapModelRoomList = new List<ModelRoom>();
        List<ModelRoom> ModelRoomList = new List<ModelRoom>();
        for (int i = 0; i < modelRoomCount; i++)
        {
            if (ModelRoom.createModelRoom(ModelRoomList) == null)
                Debug.Log("jakis blad 212");
        }
        findAllNeighbours(ModelRoomList);
        Debug.Log("1");
        do
        {
            startRoom = ModelRoomList[(int)(UnityEngine.Random.value * (ModelRoomList.Count - 1))];
            startRoom.findAllPath();
        } while (startRoom.pathList.Count < ModelRoomList.Count);


        List<ModelRoom> newModelRoomList = new List<ModelRoom>();
        List<ModelPath> pathList = startRoom.pathList;
        pathList.RemoveAt(0);
        MapModelRoomList.Add(startRoom);
        for (int i = 0; i < pathCount; i++)
        {
            if (pathList.Count == 0)
                break;


            ModelPath longestPath = pathList[0];
            foreach (ModelPath path in pathList)
            {
                if (longestPath.length() < path.length())
                    longestPath = path;
            }


            MapModelRoomList.Add(longestPath.target);

            endRoom = longestPath.target;

            foreach (ModelRoom ModelRoom in longestPath.ModelRoomsBetween)
            {
                if (!MapModelRoomList.Contains(ModelRoom))
                    MapModelRoomList.Add(ModelRoom);
            }

            for (int j = 0; j < pathList.Count; j++)
            {
                bool delete = false;
                if (pathList[j].target == longestPath.target)   //usuniecie istniejacych pokoi z list celow nowych scierzek
                {
                    pathList.RemoveAt(j);
                    j--;
                    continue;

                }
                foreach (ModelRoom ModelRoom in longestPath.ModelRoomsBetween)
                {
                    if (pathList[j].target == ModelRoom)
                    {
                        delete = true;
                        break;
                    }
                }
                if (delete)
                {
                    pathList.RemoveAt(j);
                    j--;
                    continue;
                }
                if (longestPath.target.isNeighbour(pathList[j].target))   //usuniecie sasiadow konca ciezki
                {
                    pathList.RemoveAt(j);
                    j--;
                    continue;

                }
                for (int k = longestPath.ModelRoomsBetween.Count - 1; k > longestPath.ModelRoomsBetween.Count - 4; k--)
                {
                    if (k < 0)
                        break;
                    if (longestPath.ModelRoomsBetween[k].isNeighbour(pathList[j].target))
                    {
                        delete = true;
                        break;
                    }
                }
                if (delete)
                {
                    pathList.RemoveAt(j);
                    j--;
                    continue;
                }

            }

        }
        ClearNeighbours(ModelRoomList, MapModelRoomList);
        createDoors(MapModelRoomList);
        return MapModelRoomList;
    }
    


    public void findAllNeighbours(List<ModelRoom> ModelRoomList)
    {
        for (int i = 0; i < ModelRoomList.Count; i++)
        {
            ModelRoomList[i].findNeighbours(ModelRoomList);
        }
    }

    public void ClearNeighbours(List<ModelRoom> ModelRoomList, List<ModelRoom> newModelRoomList)  //usuwa wszystkich sasiadow ktorzy nei sa w nowej lsicie a byli w starej
    {
        foreach (ModelRoom ModelRoom in ModelRoomList)
        {
            if (!newModelRoomList.Contains(ModelRoom))
            {
                foreach (ModelRoom ModelRoom1 in newModelRoomList)
                {
                    ModelRoom1.removeNeighbour(ModelRoom);
                }
            }
        }
    }

    public void createDoors(List<ModelRoom> roomList)
    {
        foreach (ModelRoom room in roomList)
        {
            room.createDoorsToNeighbours();
        }
    }
}
