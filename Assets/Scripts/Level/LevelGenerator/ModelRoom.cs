using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelRoom{

    public static int sizeMinX;
    public static int sizeMinY;
    public static int sizeMaxX;
    public static int sizeMaxY;
    public static int doorSize = 1;


    public int index;
    public int leftX, bottomY, rightX, topY;
    public List<ModelRoom> leftNeighbours = new List<ModelRoom>();
    public List<ModelRoom> topNeighbours = new List<ModelRoom>();
    public List<ModelRoom> rightNeighbours = new List<ModelRoom>();
    public List<ModelRoom> bottomNeighbours = new List<ModelRoom>();

    List<Vector2> pointsForNeighborsLT = new List<Vector2>();
    List<Vector2> pointsForNeighborsLB = new List<Vector2>();
    List<Vector2> pointsForNeighborsRT = new List<Vector2>();
    List<Vector2> pointsForNeighborsRB = new List<Vector2>();

    public List<ModelPath> pathList = new List<ModelPath>();

    public List<ModelDoor> doors = new List<ModelDoor>();

    List<GameObject> drawObjectList = new List<GameObject>();



    public ModelRoom(int leftX, int bottomY, int rightX, int topY,int index)
    {
        this.leftX = leftX;
        this.bottomY = bottomY;
        this.rightX = rightX;
        this.topY = topY;
        this.index = index;
        pathList.Add(new ModelPath(this));

        for (int x = leftX + doorSize+1; x < rightX - doorSize-1; x++)
        {
            pointsForNeighborsLT.Add(new Vector2(x, bottomY-1));
            pointsForNeighborsRT.Add(new Vector2(x, bottomY-1));
            pointsForNeighborsLB.Add(new Vector2(x, topY+1));
            pointsForNeighborsRB.Add(new Vector2(x, topY+1));
        }
        for (int y = bottomY + doorSize+1; y < topY - doorSize-1; y++)
        {
            pointsForNeighborsLT.Add(new Vector2(rightX+1, y));
            pointsForNeighborsRT.Add(new Vector2(leftX-1, y));
            pointsForNeighborsLB.Add(new Vector2(rightX + 1, y));
            pointsForNeighborsRB.Add(new Vector2(leftX - 1, y));
        }

    }
    public ModelRoom(float leftX, float bottomY, float rightX, float topY, int index)
    {
        this.leftX = (int)leftX;
        this.bottomY = (int)bottomY;
        this.rightX = (int)rightX;
        this.topY = (int)topY;
        this.index = index;
        pathList.Add(new ModelPath(this));


        for (int x = this.leftX + doorSize+1; x < rightX - doorSize-1; x++)
        {
            pointsForNeighborsLT.Add(new Vector2(x, bottomY - 1));
            pointsForNeighborsRT.Add(new Vector2(x, bottomY - 1));
            pointsForNeighborsLB.Add(new Vector2(x, topY + 1));
            pointsForNeighborsRB.Add(new Vector2(x, topY + 1));
        }
        for (int y = this.bottomY + doorSize+1; y < topY - doorSize-1; y++)
        {
            pointsForNeighborsLT.Add(new Vector2(rightX + 1, y));
            pointsForNeighborsLB.Add(new Vector2(rightX + 1, y));
            pointsForNeighborsRT.Add(new Vector2(leftX - 1, y));
            pointsForNeighborsRB.Add(new Vector2(leftX - 1, y));
        }
    }

    public static ModelRoom createModelRoom(float leftX, float bottomY, float rightX, float topY, List<ModelRoom> ModelModelRoomList)
    {
        foreach (ModelRoom ModelRoom in ModelModelRoomList)
        {
            if (ModelRoom.Colision(leftX, bottomY, rightX, topY))
                return null;
        }
        ModelRoom newModelModelRoom = new ModelRoom(leftX, bottomY, rightX, topY, ModelModelRoomList.Count);
        ModelModelRoomList.Add(newModelModelRoom);
        return newModelModelRoom;
    }

    public static ModelRoom createModelRoom(List<ModelRoom> ModelModelRoomList)
    {
        Vector2 ModelRoomSize = randomSize(sizeMinX,sizeMaxX,sizeMinY,sizeMaxY);
        if (ModelModelRoomList.Count==0)
        {
            return createModelRoom(0,0, ModelRoomSize.x, ModelRoomSize.y, ModelModelRoomList);
        }

        ModelRoom newModelModelRoom;
        foreach (ModelRoom item in ModelModelRoomList)
        {
            newModelModelRoom = item.createNeighbor(ModelRoomSize, ModelModelRoomList);
            if (newModelModelRoom != null)
                return newModelModelRoom;
        }
        return null;
    }

    public ModelRoom createNeighbor(Vector2 ModelRoomSize, List<ModelRoom> ModelModelRoomList)
    {
        
        List<Vector2> copyPointsForNeighborsLT = pointsForNeighborsLT;
        List<Vector2> copyPointsForNeighborsLB = pointsForNeighborsLB;
        List<Vector2> copyPointsForNeighborsRT = pointsForNeighborsRT;
        List<Vector2> copyPointsForNeighborsRB = pointsForNeighborsRB;
        int count = copyPointsForNeighborsLT.Count + copyPointsForNeighborsLB.Count + copyPointsForNeighborsRT.Count + copyPointsForNeighborsRB.Count;
        while (count!=0)
        {
            int rnd = Random.Range(0, count-1);
            ModelRoom newModelModelRoom;
            if (rnd < copyPointsForNeighborsLT.Count)
            {
                newModelModelRoom = createModelRoom(copyPointsForNeighborsLT[rnd].x, copyPointsForNeighborsLT[rnd].y-ModelRoomSize.y, copyPointsForNeighborsLT[rnd].x+ModelRoomSize.x, copyPointsForNeighborsLT[rnd].y, ModelModelRoomList);
                if (newModelModelRoom != null)
                    return newModelModelRoom;
                copyPointsForNeighborsLT.RemoveAt(rnd);
                count = copyPointsForNeighborsLT.Count + copyPointsForNeighborsLB.Count + copyPointsForNeighborsRT.Count + copyPointsForNeighborsRB.Count;
                continue;

            }

            rnd -= copyPointsForNeighborsLT.Count;
            if (rnd < copyPointsForNeighborsLB.Count)
            {
                newModelModelRoom = createModelRoom(copyPointsForNeighborsLB[rnd].x, copyPointsForNeighborsLB[rnd].y, copyPointsForNeighborsLB[rnd].x+ ModelRoomSize.x, copyPointsForNeighborsLB[rnd].y+ ModelRoomSize.y, ModelModelRoomList);
                if (newModelModelRoom != null)
                    return newModelModelRoom;
                copyPointsForNeighborsLB.RemoveAt(rnd);
                count = copyPointsForNeighborsLT.Count + copyPointsForNeighborsLB.Count + copyPointsForNeighborsRT.Count + copyPointsForNeighborsRB.Count;
                continue;
            }

            rnd -= copyPointsForNeighborsLB.Count;
            if (rnd < copyPointsForNeighborsRT.Count)
            {
                newModelModelRoom = createModelRoom(copyPointsForNeighborsRT[rnd].x- ModelRoomSize.x, copyPointsForNeighborsRT[rnd].y- ModelRoomSize.y, copyPointsForNeighborsRT[rnd].x, copyPointsForNeighborsRT[rnd].y, ModelModelRoomList);
                if (newModelModelRoom != null)
                    return newModelModelRoom;
                copyPointsForNeighborsRT.RemoveAt(rnd);
                count = copyPointsForNeighborsLT.Count + copyPointsForNeighborsLB.Count + copyPointsForNeighborsRT.Count + copyPointsForNeighborsRB.Count;
                continue;
            }

            rnd -= copyPointsForNeighborsRT.Count;
            if (rnd < copyPointsForNeighborsRB.Count)
            {
                newModelModelRoom = createModelRoom(copyPointsForNeighborsRB[rnd].x- ModelRoomSize.x, copyPointsForNeighborsRB[rnd].y, copyPointsForNeighborsRB[rnd].x, copyPointsForNeighborsRB[rnd].y+ ModelRoomSize.y, ModelModelRoomList);
                if (newModelModelRoom != null)
                    return newModelModelRoom;
                copyPointsForNeighborsRB.RemoveAt(rnd);
                count = copyPointsForNeighborsLT.Count + copyPointsForNeighborsLB.Count + copyPointsForNeighborsRT.Count + copyPointsForNeighborsRB.Count;
                continue;
            }
        }
        //clearPointsForNeighbors();   //jednak niepotrzebne
        return null;
    }

    void clearPointsForNeighbors(List<ModelRoom> ModelModelRoomList)
    {
        List<Vector2> pointsForNeighborsLT = new List<Vector2>();
        List<Vector2> pointsForNeighborsLB = new List<Vector2>();
        List<Vector2> pointsForNeighborsRT = new List<Vector2>();
        List<Vector2> pointsForNeighborsRB = new List<Vector2>();
        for (int i = 0; i < pointsForNeighborsLT.Count; i++)
        {
            foreach (ModelRoom ModelRoom in ModelModelRoomList)
            {
                if (ModelRoom.Colision(pointsForNeighborsLT[i].x, pointsForNeighborsLT[i].y - sizeMinY, pointsForNeighborsLT[i].x + sizeMinX, pointsForNeighborsLT[i].y))
                {
                    pointsForNeighborsLT.RemoveAt(i);
                    i--;
                    break;
                }
            }

        }

        for (int i = 0; i < pointsForNeighborsLB.Count; i++)
        {
            foreach (ModelRoom ModelRoom in ModelModelRoomList)
            {
                if (ModelRoom.Colision(pointsForNeighborsLB[i].x, pointsForNeighborsLB[i].y, pointsForNeighborsLB[i].x + sizeMinX, pointsForNeighborsLB[i].y + sizeMinY))
                {
                    pointsForNeighborsLT.RemoveAt(i);
                    i--;
                    break;
                }
            }

        }

        for (int i = 0; i < pointsForNeighborsRT.Count; i++)
        {
            foreach (ModelRoom ModelRoom in ModelModelRoomList)
            {
                if (ModelRoom.Colision(pointsForNeighborsRT[i].x - sizeMinX, pointsForNeighborsRT[i].y - sizeMinY, pointsForNeighborsRT[i].x, pointsForNeighborsRT[i].y))
                {
                    pointsForNeighborsLT.RemoveAt(i);
                    i--;
                    break;
                }
            }

        }

        for (int i = 0; i < pointsForNeighborsRB.Count; i++)
        {
            foreach (ModelRoom ModelRoom in ModelModelRoomList)
            {
                if (ModelRoom.Colision(pointsForNeighborsRB[i].x - sizeMinX, pointsForNeighborsRB[i].y, pointsForNeighborsRB[i].x, pointsForNeighborsRB[i].y + sizeMinY))
                {
                    pointsForNeighborsLT.RemoveAt(i);
                    i--;
                    break;
                }
            }

        }
    }

    public void findAllPath()  
    {
        for (int i = 0; i < pathList.Count; i++)
        {
            foreach (ModelRoom ModelRoom in pathList[i].target.leftNeighbours)
            {
                addPath(ModelRoom, pathList[i].target, pathList[i].ModelRoomsBetween);
            }
            foreach (ModelRoom ModelRoom in pathList[i].target.rightNeighbours)
            {
                addPath(ModelRoom, pathList[i].target, pathList[i].ModelRoomsBetween);
            }
            foreach (ModelRoom ModelRoom in pathList[i].target.topNeighbours)
            {
                addPath(ModelRoom, pathList[i].target, pathList[i].ModelRoomsBetween);
            }
            foreach (ModelRoom ModelRoom in pathList[i].target.bottomNeighbours)
            {
                addPath(ModelRoom, pathList[i].target, pathList[i].ModelRoomsBetween);
            }
        }
    }

    void addPath(ModelRoom ModelRoom, ModelRoom ModelRoomBetween=null, List<ModelRoom> listModelRoomsBetween=null)
    {
        if (ModelRoomBetween == this)
            ModelRoomBetween = null;
        List<ModelRoom> list;
        for (int i = 0; i < pathList.Count; i++)           
        {
            if (pathList[i].target == ModelRoom)
            {
                int length = 0;
                if (ModelRoomBetween != null)
                    length++;
                if (listModelRoomsBetween != null)
                    length += listModelRoomsBetween.Count;
                    if (pathList[i].length() > length)
                {
                    list = new List<ModelRoom>();
                    if (listModelRoomsBetween != null)
                        foreach (ModelRoom item in listModelRoomsBetween)
                        {
                            list.Add(item);
                        }
                    if (ModelRoomBetween != null)
                        list.Add(ModelRoomBetween);
                    
                    pathList[i].ModelRoomsBetween = list;
                }
                return;
            }
        }

        list = new List<ModelRoom>();
        if (listModelRoomsBetween != null)
            foreach (ModelRoom item in listModelRoomsBetween)
            {
                list.Add(item);
            }
        if (ModelRoomBetween != null)
            list.Add(ModelRoomBetween);

        pathList.Add(new ModelPath(ModelRoom,list));

    }

    public void findNeighbours(List<ModelRoom> ModelModelRoomList)
    {
        foreach (ModelRoom ModelRoom in ModelModelRoomList)
        {
            if(ModelRoom.Colision(leftX + doorSize + 1, bottomY - 1, rightX - doorSize - 1, bottomY - 1))
            {
                addBottomNeighbours(ModelRoom);
            }
            if (ModelRoom.Colision(leftX + doorSize + 1, topY + 1, rightX - doorSize - 1, topY + 1))
            {
                addTopNeighbours(ModelRoom);
            }


            if (ModelRoom.Colision(leftX -1, bottomY + doorSize + 1, leftX - 1, topY - doorSize - 1))
            {
                addLeftNeighbours(ModelRoom);
            }
            if (ModelRoom.Colision(rightX + 1, bottomY + doorSize + 1, rightX + 1, topY - doorSize - 1))
            {
                addRightNeighbours(ModelRoom);
            }
        }

    }
    
    public void ClearNeighbours()
        {


        }

    public void addLeftNeighbours(ModelRoom ModelRoom)
    {
        if (!leftNeighbours.Contains(ModelRoom))
        {
            leftNeighbours.Add(ModelRoom);
        }
    }
    public void addBottomNeighbours(ModelRoom ModelRoom)
    {
        if (!bottomNeighbours.Contains(ModelRoom))
            bottomNeighbours.Add(ModelRoom);
    }
    public void addTopNeighbours(ModelRoom ModelRoom)
    {
        if (!topNeighbours.Contains(ModelRoom))
            topNeighbours.Add(ModelRoom);
    }
    public void addRightNeighbours(ModelRoom ModelRoom)
    {
        if (!rightNeighbours.Contains(ModelRoom))
            rightNeighbours.Add(ModelRoom);
    }

    public bool isNeighbour(ModelRoom room)
    {
        if (rightNeighbours.Contains(room))
            return true;
        if (topNeighbours.Contains(room))
            return true;
        if (bottomNeighbours.Contains(room))
            return true;
        if (leftNeighbours.Contains(room))
            return true;
        return false;
    }
    public void removeNeighbour(ModelRoom room)
    {
        if (rightNeighbours.Contains(room))
            rightNeighbours.Remove(room);
        if (topNeighbours.Contains(room))
            topNeighbours.Remove(room);
        if (bottomNeighbours.Contains(room))
            bottomNeighbours.Remove(room);
        if (leftNeighbours.Contains(room))
            leftNeighbours.Remove(room);
    }


    public bool Colision(ModelRoom ModelRoom)
    {
        bool collisionX = false;
        bool collisionY = false;

        if (ModelRoom.leftX < leftX)
        {
            if (ModelRoom.rightX >= leftX)
            {
                collisionX = true;
            }
        }
        else if (ModelRoom.leftX <= rightX)
        {
            collisionX = true;
        }


        if (ModelRoom.bottomY < bottomY)
        {
            if (ModelRoom.topY >= bottomY)
            {
                collisionY = true;
            }
        }
        else if (ModelRoom.bottomY <= topY)
        {
            collisionY = true;
        }
        if (collisionX && collisionY)
        {
            return true;
        }
        return false;

    }
    public bool Colision(float leftX, float bottomY, float rightX, float topY)
    {
        bool collisionX = false;
        bool collisionY = false;

        if (leftX < this.leftX)
        {
            if (rightX >= this.leftX)
            {
                collisionX = true;
            }
        }
        else if (leftX <= this.rightX)
        {
            collisionX = true;
        }


        if (bottomY < this.bottomY)
        {
            if (topY >= this.bottomY)
            {
                collisionY = true;
            }
        }
        else if (bottomY <= this.topY)
        {
            collisionY = true;
        }
        if (collisionX && collisionY)
        {
            return true;
        }
        return false;

    }
    public void createDoorsToNeighbours()
    {
        foreach (ModelRoom room in topNeighbours)
        {
            if(!doorExist(room))
                createDoors(room);
        }
        foreach (ModelRoom room in bottomNeighbours)
        {
            if (!doorExist(room))
                createDoors(room);
        }
        foreach (ModelRoom room in rightNeighbours)
        {
            if (!doorExist(room))
                createDoors(room);
        }
        foreach (ModelRoom room in leftNeighbours)
        {
            if (!doorExist(room))
                createDoors(room);
        }
    }

    public void createDoors(ModelRoom destination)
    {
        ModelDoor door = new ModelDoor(this);
        ModelDoor door1 = new ModelDoor(destination, door);
        door.setDestination(door1);

        if (topNeighbours.Contains(destination))
        {
            int startRangePosition = Mathf.Max(this.leftX, destination.leftX)+1;
            int endRangePosition = Mathf.Min(this.rightX, destination.rightX)- doorSize;
            if (startRangePosition > endRangePosition)
            {
                Debug.Log("1Error: Drzwi sie nie zmiescioly " + index + " dop " + destination.index);
                return;
            }
            int startPosition = startRangePosition + Random.Range(0, endRangePosition - startRangePosition);
            door.setPosition(new Vector2(startPosition,topY),new Vector2(startPosition+doorSize-1, topY));
            door1.setPosition(new Vector2(startPosition, topY+1), new Vector2(startPosition + doorSize-1, topY+1));
            this.doors.Add(door);
            destination.doors.Add(door1);
            return;
        }


        if (bottomNeighbours.Contains(destination))
        {

            int startRangePosition = Mathf.Max(this.leftX, destination.leftX) + 1;
            int endRangePosition = Mathf.Min(this.rightX, destination.rightX) - doorSize;
            if (startRangePosition > endRangePosition)
            {
                Debug.Log("2Error: Drzwi sie nie zmiescioly " + index + " dop " + destination.index);
                return;
            }
            int startPosition = startRangePosition + Random.Range(0, endRangePosition - startRangePosition);
            door.setPosition(new Vector2(startPosition, bottomY), new Vector2(startPosition + doorSize-1, bottomY));
            door1.setPosition(new Vector2(startPosition, bottomY -1), new Vector2(startPosition + doorSize-1, bottomY - 1));
            this.doors.Add(door);
            destination.doors.Add(door1);
            return;
        }

        if (rightNeighbours.Contains(destination))
        {

            int startRangePosition = Mathf.Max(this.bottomY, destination.bottomY) + 1;
            int endRangePosition = Mathf.Min(this.topY, destination.topY) - doorSize;
            if (startRangePosition > endRangePosition)
            {
                Debug.Log("3Error: Drzwi sie nie zmiescioly " + index + " dop " + destination.index);
                return;
            }
            int startPosition = startRangePosition + Random.Range(0, endRangePosition - startRangePosition);
            door.setPosition(new Vector2(rightX, startPosition), new Vector2(rightX, startPosition + doorSize-1));
            door1.setPosition(new Vector2(rightX + 1, startPosition), new Vector2(rightX + 1, startPosition + doorSize-1));
            this.doors.Add(door);
            destination.doors.Add(door1);
            return;
        }


        if (leftNeighbours.Contains(destination))
        {

            int startRangePosition = Mathf.Max(this.bottomY, destination.bottomY) + 1;
            int endRangePosition = Mathf.Min(this.topY, destination.topY) - doorSize;
            if (startRangePosition > endRangePosition)
            {
                Debug.Log("4Error: Drzwi sie nie zmiescioly " + index + " dop " + destination.index);
                return;
            }
            int startPosition = startRangePosition + Random.Range(0, endRangePosition - startRangePosition);
            door.setPosition(new Vector2(leftX, startPosition), new Vector2(leftX, startPosition + doorSize-1));
            door1.setPosition(new Vector2(leftX - 1, startPosition), new Vector2(leftX - 1, startPosition + doorSize-1));
            this.doors.Add(door);
            destination.doors.Add(door1);
            return;
        }
        Debug.Log("cos jest nie tak");
    }

    bool doorExist(ModelRoom room)
    {
        foreach (ModelDoor door in doors)
        {
            if (door.getDestinationRoom()==room)
            {
                return true;
            }
        }
        return false;
    }

    

        static Vector2 randomSize(int leftX, int rightX, int bottomY, int topY)
    {
        Vector2 size;
        float radius;
        do
        {
            radius = (rightX - leftX / 2);
            size.x = (int)(NextGaussianDouble() * radius + leftX + radius);
        } while (size.x > rightX || size.x < leftX);
        do
        {
            radius = (topY - bottomY / 2);
            size.y = (int)(NextGaussianDouble() * radius + bottomY + radius);
        } while (size.y > topY || size.y < bottomY);
        return size;

    }

    static float NextGaussianDouble()
    {
        float u, v, S;

        do
        {
            u = 2.0f * Random.value - 1.0f;
            v = 2.0f * Random.value - 1.0f;
            S = u * u + v * v;
        }
        while (S >= 1.0);

        float fac = Mathf.Sqrt(-2.0f * Mathf.Log(S) / S);
        return u * fac;
    }
}













