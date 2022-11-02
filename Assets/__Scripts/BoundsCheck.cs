using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsCheck : MonoBehaviour
{
    [Header("Set in the Unity Inspector")]//properties of BoundsCheck class 
    public float radius = 1f;
    public bool keepOnScreen = true;

    [Header("These fields are set dynamically")]
    public bool isOnScreen = true;
    public float camWidth;
    public float camHeight;
    
    [HideInInspector]
    public bool offRight, offLeft, offUp, offDown; 

    void Start()
    {
        camHeight = Camera.main.orthographicSize;
        camWidth = camHeight * Camera.main.aspect;//setting camHeight and Width values at the start of the scene 
        
    }

    void LateUpdate()
    {
        Vector3 pos = transform.position;
        //at every frame, it assumes that the position of game object is on screen 
        isOnScreen = true;
        offRight = offLeft = offUp = offDown = false;

        if (pos.x > camWidth - radius)//if off to the right 
        {
            pos.x = camWidth - radius;
            isOnScreen = false;
            offRight = true;
        }
        if (pos.x < -camWidth + radius)//if off to the left 
        {
            pos.x = -camWidth + radius;
            isOnScreen = false;
            offLeft = true;
        }

        if (pos.y > camHeight - radius)//if off to the top 
        {
            pos.y = camHeight - radius;
            isOnScreen = false;
            offUp = true;
        }
        if (pos.y < -camHeight + radius)//if off to the bottom
        {
            pos.y = -camHeight + radius;
            isOnScreen = false;
            offDown = true;
        }

        isOnScreen = !(offRight || offLeft || offUp || offDown);//if its off on any direction, isOnScreen is set to false 

        if (keepOnScreen && !isOnScreen)//if its true that we want to keepOnScreen and is not onScreen
        {
            transform.position = pos;//reset the position 
            isOnScreen = true;//reset is on screen to true 
        }
    }

}
