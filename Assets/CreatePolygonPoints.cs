using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePolygonPoints : MonoBehaviour
{
    private Vector2[] newPoints;
    private float rectangleDefaultSize = 15f;
    // Start is called before the first frame update
    void Start()
    {
        newPoints = new Vector2[] {
            new Vector2(-1 * rectangleDefaultSize, -1 * rectangleDefaultSize), 
            new Vector2( 1 * rectangleDefaultSize, -1 * rectangleDefaultSize),  
            new Vector2( 1 * rectangleDefaultSize,  1 * rectangleDefaultSize),   
            new Vector2(-1 * rectangleDefaultSize,  1 * rectangleDefaultSize)     
        };
        PolygonCollider2D polygonCollider = GetComponent<PolygonCollider2D>();
        if (newPoints != null && newPoints.Length > 0)
        {
            polygonCollider.points = newPoints;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
