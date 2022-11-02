using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    //class properties below 
    static public Main instance;
    [Header("Set in the Unity Inspector")]
    public GameObject[] prefabEnemies;
    public float enemySpawnPerSecond = 0.5f;
    public float enemyDefaultPadding = 1.5f;

    private BoundsCheck _bndCheck;

    void Awake()
    {
        instance = this;
        _bndCheck = GetComponent<BoundsCheck>();
        Invoke("SpawnEnemy", 1f / enemySpawnPerSecond);//calls the SpawnEnemy method after about 2 seconds 

    }

    public void SpawnEnemy()
    {
        int ndx = Random.Range(0, prefabEnemies.Length);//picks an index from 0 to the number of enemies in the prefabEnemies GameObject array 
        GameObject go = Instantiate<GameObject>(prefabEnemies[ndx]);//instantiates the enemy game object 

        float enemyPadding = enemyDefaultPadding;
        if (go.GetComponent<BoundsCheck>() != null)
        {
            enemyPadding = Mathf.Abs(go.GetComponent<BoundsCheck>().radius);//sets a padding to the enemy game object to make sure it doesnt spawn too close to the boundaries 
        }

        Vector3 pos = Vector3.zero;//sets the position of the object to 0,0,0

        float xMin = -_bndCheck.camWidth + enemyPadding + 2;//determines xMin and xMax values 
        float xMax = _bndCheck.camWidth - enemyPadding - 2;

        pos.x = Random.Range(xMin, xMax);//randomly sets the x position of the object to between xMin and xMax
        pos.y = _bndCheck.camHeight + enemyPadding;//sets the y position of the object to the top of the camHeight + its padding 
        go.transform.position = pos;

        Invoke("SpawnEnemy", 1f / enemySpawnPerSecond);//spawns the enemy after 2 second delay 
    }

    public void DelayedRestart(float delay)
    {
        Invoke("Restart", delay);//calls restart method after a delay set in the inspector 
    }

    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");//loads the scene SampleScene 
    }
}
