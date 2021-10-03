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
}