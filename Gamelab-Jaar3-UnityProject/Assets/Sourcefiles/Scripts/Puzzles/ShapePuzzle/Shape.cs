using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour
{
    public Canvas myCanvas;
    public GameObject block;
    public ShapePuzzle shapePuzzle;

    public Vector2 originalPos;

    public bool placed;


    void Awake()
    {
        block = gameObject;
        originalPos = GetComponent<RectTransform>().anchoredPosition;
    }


    public enum ShapeType
    {
        Rectangular,
        Triangluar,
        Square
    };

    public ShapeType shapeType;


    public void Drag()
    {
        if(!placed)
        {
            Vector2 pos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(myCanvas.transform as RectTransform, Input.mousePosition, myCanvas.worldCamera, out pos);
            block.transform.position = new Vector3(myCanvas.transform.TransformPoint(pos).x, myCanvas.transform.TransformPoint(pos).y, block.transform.position.z);
        }
        
    }

    public void Release()
    {
        if(!placed)
        {
            GetComponent<RectTransform>().anchoredPosition = originalPos;
        }
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.GetComponent<ShapePlace>() != null && !placed)
        {
            if (col.GetComponent<ShapePlace>().shapePlaceType == shapeType)
            {
                placed = true;
                shapePuzzle.Count();
            }
        }
    }
}
