//Made by Alieke
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RushHourBlocks : MonoBehaviour
{
    private float xBeginPos, yBeginPos;
    public bool horizontalBlock;
    public Vector2 blockPosition;
    Vector2 size;

    RushHour rushHour;

    int widthMin, widthMax;
    int heigthMin, heigthMax;

	void Start ()
    {
        blockPosition = new Vector2(transform.position.x, transform.position.y);
        xBeginPos = transform.localPosition.x;
        yBeginPos = transform.localPosition.y;
        rushHour = GameObject.Find("RushHour").GetComponent<RushHour>();
	}
	
	void OnMouseDrag ()
    {
        //Drag block
        Vector3 v3 = Input.mousePosition;
        v3.z = 10;
        v3 = Camera.main.ScreenToWorldPoint(v3);

        if (horizontalBlock)
        {
            CheckWidth(widthMin, widthMax);
            transform.position = new Vector3(Mathf.Clamp(v3.x, widthMin, widthMax), Mathf.Clamp(v3.y, yBeginPos, yBeginPos), v3.z);
        }
        else
        {
            CheckHeight();
            transform.position = new Vector3(Mathf.Clamp(v3.x, xBeginPos, xBeginPos), Mathf.Clamp(v3.y, 0, rushHour.gridHeight), v3.z);
        }
	}

    int CheckWidth(int widthMinInt, int widthMaxInt)
    {
        for (int i = 0; i < rushHour.gridWidth -1; i ++) {
            if (rushHour.grid[(int)transform.position.x + i, (int)transform.position.y] == true)
            {
                widthMaxInt = i--;
            }
        }
        return (widthMinInt);
    }

    void CheckHeight()
    {
        for (int i = 0; i < rushHour.gridHeight - 1; i++)
        {
            if (rushHour.grid[(int)transform.position.x, (int)transform.position.y + i] == true)
            {

            }
        }
    }

    void OnMouseUpAsDown()
    {
        transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), 0);
    }
}
