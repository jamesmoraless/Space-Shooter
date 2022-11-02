using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    static public Hero player;

    [Header("Set in the Unity Inspector")]
    //Hero properties below which are changeable in the inspector 
    public float speed = 30;
    public float rollMult = -45;
    public float pitchMult = 30;
    public float gameRestartDelay = 2f;//2 second restart delay 

    [Header("These fields are set dynamically")]
    [SerializeField]
    private float _shieldLevel = 1;//"Shield level" of 1 which is just the health of the hero ship 

    private GameObject _lastTriggerGo = null;

    void Awake()
    {
        player = this;//instantiates the player to this class before "start" of the scene 
    }

    void Update()
    {
        float xAxis = Input.GetAxis("Horizontal");// sets x axis 
        float yAxis = Input.GetAxis("Vertical");//sets y axis

        Vector3 pos = transform.position;
        pos.x += xAxis * speed * Time.deltaTime;// moves left and right depending on the user input (left, right, a, d)
        pos.y += yAxis * speed * Time.deltaTime;//moves up and down depending on the user input 
        transform.position = pos;//sets the x and y position of the hero object

        transform.rotation = Quaternion.Euler(yAxis * pitchMult, xAxis * rollMult, 0);//sets the feeling of rotation to the object 
    }

    void OnTriggerEnter(Collider other) 
    {
        Transform rootT = other.gameObject.transform.root;//searches for the "root" transform of the game object of the hero 
        GameObject go = rootT.gameObject;

        if (go == _lastTriggerGo) 
        {
            return;
        }
        _lastTriggerGo = go;

        if (go.tag == "Enemy")//if coliding with game object that is tagged as "Enemy", destroy the game object and set hero shield level lower 
        {
            shieldLevel--;
            Destroy(go);
        }
        else 
        {
            print("Triggered by non-Enemy: " + go.name);
        }
    }

    public float shieldLevel //shield level property of the hero class 
    {
        get{
            
            return (_shieldLevel);
        }
        set{
            _shieldLevel = Mathf.Min(value, 1);//at reset the maximum value of shieldLevel should be set to 1
            if (_shieldLevel == 0) //if the shield level reaches 0, destroy the hero object and restart the scene after 2 second delay 
            {
                Destroy(this.gameObject);
                Main.instance.DelayedRestart(gameRestartDelay);
            }
        }
    }
}
