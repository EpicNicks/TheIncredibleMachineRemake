using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinningCondition : MonoBehaviour
{

    private bool isWon = false;
    public GameObject winningMenuUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isWon == true)
        {
            winningMenuUI.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            isWon = true;
        }
    }
}
