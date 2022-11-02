using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy_2:Enemy//inherits properties of Enemy class 
{
    //new Enemy_2 class properties 
    private bool movingLeft;
    private bool result;
    System.Random rnd = new System.Random();

    
    void Start()
    {
        movingLeft = randomizer();//randomizes whether moving left is true or false at the beginning of the scene
    }

    public void Update()
    {
        //min and max values determined
        float xMin = -_boundChk.camWidth + _boundChk.radius + 4;
        float xMax = _boundChk.camWidth - _boundChk.radius - 4;

        if (movingLeft == true)//if moves left, call Move() and once passed the minimum x value, set moving left to false 
        {
            Move();
            if (pos.x < xMin) 
                movingLeft = false;
        }
        else//if moving left is false, call Move2() and once the maximum x value is passes, set moving left to true 
        {
            Move2();
            if (pos.x > xMax)
                movingLeft = true;
        }

        if (_boundChk != null && !_boundChk.isOnScreen)//if no longer on the screen (y), delete the game object 
        {
            if (pos.y < _boundChk.camHeight - _boundChk.radius)
            {
                Destroy(gameObject);
            }
        }
    }

    public override void Move()
    {                
        Vector3 tempPos = pos;
        tempPos.y -= speed * Time.deltaTime;//move down 
        tempPos.x -= speed * Time.deltaTime;//Move in the left direction with the same speed as the down direction making it a 45 degree angle 
        pos = tempPos;
    }

    public void Move2() 
    {
        Vector3 tempPos = pos;
        tempPos.y -= speed * Time.deltaTime;//move down the screen as Enemy_1 does 
        tempPos.x += speed * Time.deltaTime;//Move in the right direction by adding to the object's x direction (displacing positively to the right)
        pos = tempPos;
    }

    public bool randomizer()//returns true or false... rnd.NextDouble() returns a value from 0 to 1 if the condition is true (x>0.5), the method returns true, otherwise its false 
    {

        return rnd.NextDouble() > 0.5;//the 0.5 gives the output of true or false a 50/50 chance of being outputted 
    }
}
