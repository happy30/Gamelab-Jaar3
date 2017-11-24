//by the internets

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateItem : MonoBehaviour
{

    private float _sensitivity;
    private Vector3 _mouseReference;
    private Vector3 _mouseOffset;
    private Vector3 _rotation;
    private bool _isRotating;

    public GameObject selectSprite;

    void Start()
    {

        _sensitivity = 250f;
        _rotation = Vector3.zero;
    }


    void Update()
    {
        if(_isRotating)
        {
            float rotX = Input.GetAxis("Mouse X") * _sensitivity * Mathf.Deg2Rad;
            float rotY = Input.GetAxis("Mouse Y") * _sensitivity * Mathf.Deg2Rad;

            transform.Rotate(Vector3.up, -rotX);
            transform.Rotate(Vector3.right, rotY);
        }

    }

    /*
    void Update()
    {
        if (_isRotating)
        {
            // offset
            _mouseOffset = (Input.mousePosition - _mouseReference);
            // apply rotation
            //_rotation.y = -(_mouseOffset.x + _mouseOffset.y) * _sensitivity;
            _rotation.y = -(_mouseOffset.x) * _sensitivity;
            _rotation.x = -(_mouseOffset.y) * _sensitivity;
            // rotate
            //transform.Rotate(_rotation);
            transform.eulerAngles += _rotation;
            // store mouse
            _mouseReference = Input.mousePosition;
        }
    }

    */
    void OnMouseDown()
    {
        // rotating flag
        _isRotating = true;

        // store mouse
        _mouseReference = Input.mousePosition;
    }

    void OnMouseUp()
    {
        // rotating flag
        _isRotating = false;
    }

    
}

