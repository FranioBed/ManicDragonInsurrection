using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelDoor {
    ModelRoom room;
    public Vector2 startPosition;
    public Vector2 endPosition;
    ModelDoor destination;
	
    public ModelDoor(ModelRoom room)
    {
        this.room = room;
    }

    public ModelDoor(ModelRoom room, ModelDoor destination)
    {
        this.room = room;
        this.destination = destination;
    }


    public void setPosition(Vector2 startPosition, Vector2 endPosition)
    {
        this.startPosition = startPosition;
        this.endPosition = endPosition;

    }
    public void setDestination(ModelDoor destination)
    {
        this.destination = destination;
    }

   public ModelRoom getDestinationRoom()
    {
        return destination.room;
    }
}
