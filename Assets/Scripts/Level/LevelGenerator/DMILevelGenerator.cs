using Assets.Scripts.Level.LevelDTO;
using Assets.Scripts.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DMILevelGenerator : IDMILevelGenerator
{
    

    public List<Room> ModelRoomList = new List<Room>();
    List<List<Vector3>> pointsNearSides = new List<List<Vector3>>(); // punty przy brzegach pokojow, 'z' odpowiada zastrone sasiada 1 - prawa, 2 - lewa, 3 - gora, 4 - dol


    public IEnumerable<RoomMetaData> generate(int seed, IntVector2 levelSize)
    {
        UnityEngine.Random.InitState(seed);
        List<Room> roomList = GenerateMap();

        IList<RoomMetaData> rooms = new List<RoomMetaData>();

        int minimumX = 0;
        int minimumY = 0;

        foreach (Room item in roomList)
        {
            if (minimumX > item.minX)
                minimumX = item.minX;
            if (minimumY > item.minY)
                minimumY = item.minY;
        }

        foreach (Room item in roomList)
        {
            RoomMetaData room = new RoomMetaData
            {

                position = new IntVector2(item.minX- minimumX, item.minY-minimumY),
                size = new IntVector2(item.maxX-item.minX+1, item.maxY - item.minY+1),
                doorLocations = new List<IntVector2>
                {
                    new IntVector2(4, 4)
                },
                hasExit = false,
                hasStart = true
            };
            rooms.Add(room);
        }
        return rooms;
        
    }


    List<Room> GenerateMap()
    {
        generateRoomModel(5);
        Room startRoom;
        do
        {
            startRoom = ModelRoomList[(int)(UnityEngine.Random.value * (ModelRoomList.Count - 1))];
            startRoom.findAllPath();
        } while (startRoom.pathList.Count < ModelRoomList.Count);


        List<Room> newRoomList = new List<Room>();
        List<Path> pathList = startRoom.pathList;
        pathList.RemoveAt(0);
        newRoomList.Add(startRoom);
        for (int i = 0; i < 6; i++)
        {
            if (pathList.Count == 0)
                break;


            Path longestPath = pathList[0];
            foreach (Path path in pathList)
            {
                if (longestPath.length() < path.length())
                    longestPath = path;
            }


            newRoomList.Add(longestPath.target);
            foreach (Room room in longestPath.roomsBetween)
            {
                if (!newRoomList.Contains(room))
                    newRoomList.Add(room);
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
                foreach (Room room in longestPath.roomsBetween)
                {
                    if (pathList[j].target == room)
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
                if (longestPath.target.isNeighbour(pathList[j].target) != 0)   //usuniecie sasiadow konca ciezki
                {
                    pathList.RemoveAt(j);
                    j--;
                    continue;

                }
                for (int k = longestPath.roomsBetween.Count - 1; k > longestPath.roomsBetween.Count - 4; k--)
                {
                    if (k < 0)
                        break;
                    if (longestPath.roomsBetween[k].isNeighbour(pathList[j].target) != 0)
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
        return newRoomList;

    }


    public void generateRoomModel(int count)
    {
        ModelRoomList.Clear();
        pointsNearSides.Clear();

        createRoom(new Vector2(0, 0), randomSize(8, 20, 8, 20), 1, -1);
        count--;

        bool match;
        Vector2 roomSize;
        int rotation;
        for (int i = 0; i < count; i++)
        {
            roomSize = randomSize(8, 20, 8, 20);
            for (int roomId = 0; roomId < pointsNearSides.Count; roomId++)
            {
                match = false;
                for (int j = 0; j < pointsNearSides[roomId].Count; j++)
                {
                    if (matchInThisPosition(pointsNearSides[roomId][j], new Vector2(3, 3)) == 0)
                    {
                        pointsNearSides[roomId].RemoveAt(j);
                        j--;
                    }
                    else
                    {
                        rotation = matchInThisPosition(pointsNearSides[roomId][j], roomSize);
                        if (rotation != 0)
                        {
                            createRoom(pointsNearSides[roomId][j], roomSize, rotation, roomId);
                            match = true;
                            break;
                        }
                    }
                }

                if (match)
                    break;
            }

        }
        findNeighbours();
    }


    int matchInThisPosition(Vector2 position, Vector2 size)
    {
        bool matchRT = true;
        bool matchRB = true;
        bool matchLT = true;
        bool matchLB = true;
        Vector2[] predictableRT = new Vector2[2];
        Vector2[] predictableRB = new Vector2[2];
        Vector2[] predictableLT = new Vector2[2];
        Vector2[] predictableLB = new Vector2[2];

        predictableRT[0] = position;
        predictableRT[1] = position + size;

        predictableRB[0] = new Vector2(position.x, position.y - size.y);
        predictableRB[1] = new Vector2(position.x + size.x, position.y);

        predictableLT[0] = new Vector2(position.x - size.x, position.y);
        predictableLT[1] = new Vector2(position.x, position.y + size.y);

        predictableLB[0] = position - size;
        predictableLB[1] = position;

        foreach (Room room in ModelRoomList)
        {
            if (room.roomColision(predictableRT[0][0], predictableRT[0][1], predictableRT[1][0], predictableRT[1][1]))
            {
                matchRT = false;
            }
            if (room.roomColision(predictableRB[0][0], predictableRB[0][1], predictableRB[1][0], predictableRB[1][1]))
                matchRB = false;
            if (room.roomColision(predictableLT[0][0], predictableLT[0][1], predictableLT[1][0], predictableLT[1][1]))
                matchLT = false;
            if (room.roomColision(predictableLB[0][0], predictableLB[0][1], predictableLB[1][0], predictableLB[1][1]))
                matchLB = false;
        }

        if (matchRT)
            return 1;
        if (matchRB)
            return 2;
        if (matchLT)
            return 3;
        if (matchLB)
            return 4;
        return 0;
    }

    Room createRoom(Vector3 position, Vector2 size, int rotation, int neighboursId)
    {
        Room newRoom = null;
        switch (rotation)
        {
            case 1:
                {
                    newRoom = new Room(position.x, position.y, position.x + size.x, position.y + size.y);
                    break;
                }
            case 2:
                {
                    newRoom = new Room(position.x, position.y - size.y, position.x + size.x, position.y);
                    break;
                }
            case 3:
                {
                    newRoom = new Room(position.x - size.x, position.y, position.x, position.y + size.y);
                    break;
                }
            case 4:
                {
                    newRoom = new Room(position.x - size.x, position.y - size.y, position.x, position.y);
                    break;
                }
            default:
                break;
        }
        ModelRoomList.Add(newRoom);
        forNeighbours(newRoom);
        return newRoom;
    }

    void forNeighbours(Room room)  //dodaje do listy punkty przy brzegach pomieszzeenia
    {

        List<Vector3> pointList = new List<Vector3>();  // z odpowiada zastrone sasiada 1 - prawa, 2 - lewa, 3 - gora, 4 - dol
        for (int j = room.minX + 3; j < room.maxX - 3; j++)
        {
            pointList.Add(new Vector3(j, room.minY - 1, 4));
            pointList.Add(new Vector3(j, room.maxY + 1, 3));
        }

        for (int j = room.minY + 3; j < room.maxY - 3; j++)
        {
            pointList.Add(new Vector3(room.minX - 1, j, 2));
            pointList.Add(new Vector3(room.maxX + 1, j, 1));
        }


        List<Vector3> pointList2 = new List<Vector3>();
        while (pointList.Count != 0)
        {
            int randomPosition = (int)(UnityEngine.Random.value * (pointList.Count - 1));
            pointList2.Add(pointList[randomPosition]);
            pointList.RemoveAt(randomPosition);
        }

        pointsNearSides.Add(pointList2);
    }

    Vector2 randomSize(int minX, int maxX, int minY, int maxY)
    {
        Vector2 size;
        float radius;
        do
        {
            radius = (maxX - minX / 2);
            size.x = (int)(NextGaussianDouble() * radius + minX + radius);
        } while (size.x > maxX || size.x < minX);
        do
        {
            radius = (maxY - minY / 2);
            size.y = (int)(NextGaussianDouble() * radius + minY + radius);
        } while (size.y > maxY || size.y < minY);
        return size;

    }


    float NextGaussianDouble()
    {
        float u, v, S;

        do
        {
            u = 2.0f * UnityEngine.Random.value - 1.0f;
            v = 2.0f * UnityEngine.Random.value - 1.0f;
            S = u * u + v * v;
        }
        while (S >= 1.0);

        float fac = Mathf.Sqrt(-2.0f * Mathf.Log(S) / S);
        return u * fac;
    }


    void findNeighbours()
    {
        for (int i = 0; i < ModelRoomList.Count; i++)
        {
            findNeighbours(i);
        }
    }

    void findNeighbours(int roomId)
    {
        int minLength = 3;  //blokada rogow
        Room neighbour;

        for (int x = (int)ModelRoomList[roomId].minX + minLength; x <= (int)ModelRoomList[roomId].maxX - minLength; x++)
        {
            neighbour = pointInRoom(new Vector2(x, ModelRoomList[roomId].minY - 1));
            if (neighbour != null)
                ModelRoomList[roomId].addBottomNeighbours(neighbour);

            neighbour = pointInRoom(new Vector2(x, ModelRoomList[roomId].maxY + 1));
            if (neighbour != null)
                ModelRoomList[roomId].addTopNeighbours(neighbour);
        }

        for (int y = (int)ModelRoomList[roomId].minY + minLength; y <= (int)ModelRoomList[roomId].maxY - minLength; y++)
        {
            neighbour = pointInRoom(new Vector2(ModelRoomList[roomId].minX - 1, y));
            if (neighbour != null)
                ModelRoomList[roomId].addLeftNeighbours(neighbour);

            neighbour = pointInRoom(new Vector2(ModelRoomList[roomId].maxX + 1, y));
            if (neighbour != null)
                ModelRoomList[roomId].addRightNeighbours(neighbour);
        }

    }

    Room pointInRoom(Vector2 point)
    {

        foreach (Room room in ModelRoomList)
        {
            if (room.pointInRoom(point))
                return room;
        }
        return null;
    }
}
