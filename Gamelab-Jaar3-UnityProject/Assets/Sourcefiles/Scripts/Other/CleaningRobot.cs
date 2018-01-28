using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CleaningRobot : MonoBehaviour

{
	public float speed;

	Vector3 direction;

	public enum Direction
	{
		Forward,
		Left,
		Right,
		Backwards,
	}

	public Direction directionToMoveTo;

    void Start()
    {
        SetDirection(Direction.Forward);
    }
	
	// Update is called once per frame
	void Update ()
	{
        transform.Translate(direction * Time.deltaTime * speed);
	}

	void SetDirection(Direction dir)
	{
        directionToMoveTo = dir;
		switch (dir)
		{
			case Direction.Forward:
				direction = Vector3.forward;
                break;
                
			case Direction.Backwards:
				direction = -Vector3.forward;
                break;

            case Direction.Left:
				direction = -Vector3.right;
                break;

            case Direction.Right:
				direction = Vector3.right;
                break;
        }
        
    }

    void OnCollisionStay(Collision col)
    {

        if(col.collider.gameObject.tag != "Player")
        {
            print("collision");
            switch (directionToMoveTo)
            {
                case Direction.Forward:
                    SetDirection(Direction.Right);
                    break;

                case Direction.Right:
                    SetDirection(Direction.Backwards);
                    break;

                case Direction.Backwards:
                    SetDirection(Direction.Left);
                    break;

                case Direction.Left:
                    SetDirection(Direction.Forward);
                    break;
            }
        }


    }

    
}
