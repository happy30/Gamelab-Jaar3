using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPuzzleController : MonoBehaviour
{
    public void Reset() { }
    public void OnSolve() { }
    public void OnFail() { }
}
