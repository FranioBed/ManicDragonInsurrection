using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room {
    
    public int index;

    public int minX, minY, maxX, maxY;
    public List<Room> leftNeighbours = new List<Room>();
    public List<Room> topNeighbours = new List<Room>();
    public List<Room> rightNeighbours = new List<Room>();
    public List<Room> bottomNeighbours = new List<Room>();

    public List<Path> pathList = new List<Path>();

    List<GameObject> drawObjectList = new List<GameObject>();

    public Room(int minX, int minY, int maxX, int maxY)
    {
        this.minX = minX;
        this.minY = minY;
        this.maxX = maxX;
        this.maxY = maxY;
        pathList.Add(new Path(this));
    }
    public Room(float minX, float minY, float maxX, float maxY)
    {
        this.minX = (int)minX;
        this.minY = (int)minY;
        this.maxX = (int)maxX;
        this.maxY = (int)maxY;
        pathList.Add(new Path(this));
    }



    public void findAllPath()  
    {
        for (int i = 0; i < pathList.Count; i++)
        {
            foreach (Room room in pathList[i].target.leftNeighbours)
            {
                addPath(room, pathList[i].target, pathList[i].roomsBetween);
            }
            foreach (Room room in pathList[i].target.rightNeighbours)
            {
                addPath(room, pathList[i].target, pathList[i].roomsBetween);
            }
            foreach (Room room in pathList[i].target.topNeighbours)
            {
                addPath(room, pathList[i].target, pathList[i].roomsBetween);
            }
            foreach (Room room in pathList[i].target.bottomNeighbours)
            {
                addPath(room, pathList[i].target, pathList[i].roomsBetween);
            }
        }
    }

    void addPath(Room room, Room roomBetween=null, List<Room> listRoomsBetween=null)
    {
        if (roomBetween == this)
            roomBetween = null;
        List<Room> list;
        for (int i = 0; i < pathList.Count; i++)           
        {
            if (pathList[i].target == room)
            {
                int length = 0;
                if (roomBetween != null)
                    length++;
                if (listRoomsBetween != null)
                    length += listRoomsBetween.Count;
                    if (pathList[i].length() > length)
                {
                    list = new List<Room>();
                    if (listRoomsBetween != null)
                        foreach (Room item in listRoomsBetween)
                        {
                            list.Add(item);
                        }
                    if (roomBetween != null)
                        list.Add(roomBetween);
                    
                    pathList[i].roomsBetween = list;
                }
                return;
            }
        }

        list = new List<Room>();
        if (listRoomsBetween != null)
            foreach (Room item in listRoomsBetween)
            {
                list.Add(item);
            }
        if (roomBetween != null)
            list.Add(roomBetween);

        pathList.Add(new Path(room,list));

    }

    public void addLeftNeighbours(Room room)
    {
        if (!leftNeighbours.Contains(room))
        {
            leftNeighbours.Add(room);
        }
    }
    public void addBottomNeighbours(Room room)
    {
        if (!bottomNeighbours.Contains(room))
            bottomNeighbours.Add(room);
    }
    public void addTopNeighbours(Room room)
    {
        if (!topNeighbours.Contains(room))
            topNeighbours.Add(room);
    }
    public void addRightNeighbours(Room room)
    {
        if (!rightNeighbours.Contains(room))
            rightNeighbours.Add(room);
    }

    public int isNeighbour(Room room)
    {
        if (rightNeighbours.Contains(room))
            return 1;
        if (topNeighbours.Contains(room))
            return 3;
        if (bottomNeighbours.Contains(room))
            return 4;
        if (leftNeighbours.Contains(room))
            return 2;
        return 0;
    }

    public bool pointInRoom(Vector2 point)
    {
        if (minX <= point.x && point.x <= maxX)
            if (minY <= point.y && point.y <= maxY)
                return true;
        return false;
    }


    public bool roomColision(Room room)
    {
        bool collisionX = false;
        bool collisionY = false;

        if (room.minX < minX)
        {
            if (room.maxX >= minX)
            {
                collisionX = true;
            }
        }
        else if (room.minX <= maxX)
        {
            collisionX = true;
        }


        if (room.minY < minY)
        {
            if (room.maxY >= minY)
            {
                collisionY = true;
            }
        }
        else if (room.minY <= maxY)
        {
            collisionY = true;
        }
        if (collisionX && collisionY)
        {
            return true;
        }
        return false;

    }
    public bool roomColision(float minX, float minY, float maxX, float maxY)
    {
        bool collisionX = false;
        bool collisionY = false;

        if (minX < this.minX)
        {
            if (maxX >= this.minX)
            {
                collisionX = true;
            }
        }
        else if (minX <= this.maxX)
        {
            collisionX = true;
        }


        if (minY < this.minY)
        {
            if (maxY >= this.minY)
            {
                collisionY = true;
            }
        }
        else if (minY <= this.maxY)
        {
            collisionY = true;
        }
        if (collisionX && collisionY)
        {
            return true;
        }
        return false;

    }
}

public class Path
{
    public List<Room> roomsBetween;
    public Room target;
    public Path(Room room, List<Room> list=null)
    {
        this.target = room;
        if (list != null)
        {
            roomsBetween = list;
        }
        else
        {
            roomsBetween = new List<Room>();
        }
    }
    public int length()
    {
        if (roomsBetween != null)
            return roomsBetween.Count + 1;
        else return 1;
    }

}