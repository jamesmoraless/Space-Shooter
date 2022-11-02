using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //class properties below 
    [Header("Set in the Unity Inspector")]
    public float speed = 10f;
    public float fireRate = 0.3f;
    public float health = 10;
    public int score = 100;
    protected BoundsCheck _boundChk;//protected since we want to use this in the Enemy2 class thus it cant be private 
   
    public Vector3 pos//class propertie named pos
    {
        get
        {
            return (this.transform.position);
        }
        set
        {
            this.transform.position = value;
        }
    }

    void Awake()//initiates before the start method and instantiates the _boundChk variable 
    {
        _boundChk = GetComponent<BoundsCheck>();
        
    }

    void Update()//updates at every frame 
    {
        Move();//moves the enemy object 

        if (_boundChk != null && !_boundChk.isOnScreen)
        {
            if (pos.y < _boundChk.camHeight - _boundChk.radius)//if below the screen 
            {
                Destroy(gameObject);//destroys the enemy object 
            }
        }
    }

    public virtual void Move()
    {
        Vector3 tempPos = pos;
        tempPos.y -= speed * Time.deltaTime;//object is moved with speed and time to give it a velocity down the screen (-=)
        pos = tempPos;//position of the object is set at every jump in the y direction 
    }

}
