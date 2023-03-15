using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public GameObject brickOriginal;
    void Start()
    {
        GameObject BrickWall = Instantiate(brickOriginal);
        BrickWall.transform.position = Vector3.zero; 
    }


}
