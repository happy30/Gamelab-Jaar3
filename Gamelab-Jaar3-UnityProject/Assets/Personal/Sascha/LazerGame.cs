using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerGame : MonoBehaviour
{

    public enum Direction {North, South, West, East}
    public Direction rightDirection;
    public Direction currentDirection;
    public GameObject lineRenderer;

    void Update ()
    {
        CheckDirection();
    }

    private void OnMouseDown()
    {
        if (currentDirection == Direction.North)
            currentDirection = Direction.East;
        else if (currentDirection == Direction.East)
            currentDirection = Direction.South;
        else if (currentDirection == Direction.South)
            currentDirection = Direction.West;
        else if (currentDirection == Direction.West)
            currentDirection = Direction.North;
    }
    
    void CheckDirection()
    {
        if(currentDirection == rightDirection)
        {
            GameObject clone;
            clone = Instantiate(lineRenderer, transform.position, transform.rotation);
            clone.transform.SetParent(transform);
            
        }
        else GetComponent<LineRenderer>().enabled = false;
    }
}