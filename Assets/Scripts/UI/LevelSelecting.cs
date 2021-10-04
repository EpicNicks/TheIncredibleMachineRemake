using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelSelecting : MonoBehaviour
{
    public void Level1()
    {
        SceneManager.LoadScene("IanTestMainScene");

    }
    public void Level2()
    {
        Debug.Log("level2");
    }
    public void Level3()
    {
        Debug.Log("level3");
    }
    public void Level4()
    {
        Debug.Log("level4");
    }
    public void Level5()
    {
        Debug.Log("level5");
    }
}