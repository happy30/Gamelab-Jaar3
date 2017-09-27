//Made by Alieke
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RushHourBlocks : MonoBehaviour {
    private float xBeginPos, yBeginPos;
    public bool horizontalBlock;
    public Vector2 blockPosition;
    public Vector2 lastPosition;

    RushHour rushHour;

	void Start () {
        blockPosition = new Vector2(transform.position.x, transform.position.y);
        lastPosition = blockPosition;
        xBeginPos = transform.localPosition.x;
        yBeginPos = transform.localPosition.y;
        rushHour = GameObject.Find("RushHour").GetComponent<RushHour>();
	}
	
	void OnMouseDrag () {
        Vector3 v3 = Input.mousePosition;
        v3.z = 10;
        v3 = Camera.main.ScreenToWorldPoint(v3);
        if (horizontalBlock)
        {
            for(int x = 0; x < rushHour.gridWidth; x++)
            {
                for(int y = 0; y < rushHour.gridHeight; y++)
                {
                    transform.localPosition = new Vector3(Mathf.Clamp(v3.x, 0, rushHour.gridWidth), Mathf.Clamp(v3.y, yBeginPos, yBeginPos), v3.z);
                    rushHour.grid[(int)transform.localEulerAngles.x, (int)transform.localPosition.y] = true;
                    lastPosition = blockPosition;
                    blockPosition = new Vector2(transform.position.x, transform.position.y);
                    if (rushHour.grid[(int)blockPosition.x, (int)blockPosition.y])
                    {
                        transform.position = lastPosition;
                    }
                }
            }
        } else
        {
            transform.localPosition = new Vector3(Mathf.Clamp(v3.x, xBeginPos, xBeginPos), Mathf.Clamp(v3.y, 0, rushHour.gridHeight) , v3.z);
        }
	}

    void OnMouseExit()
    {
        transform.localPosition = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), 0);
    }
}
