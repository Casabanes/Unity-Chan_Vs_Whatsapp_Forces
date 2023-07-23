using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public void RechargeScene(int thisScene)
    {

        SceneManager.LoadScene(thisScene);
    }
    public void MainMenu()
    {
        Debug.Log("me voy al menu");
    }
}
