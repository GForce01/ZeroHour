using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menuscript : MonoBehaviour
{
    [SerializeField] GameObject credits;
    [SerializeField] GameObject mainmenu;


    public void play()
    {
        
    }

    public void presscredits()
    {
        credits.SetActive(true);
        mainmenu.SetActive(false);
    }

    public void goback()
    {
        credits.SetActive(false);
        mainmenu.SetActive(true);
    }

    public void exit()
    {
        Application.Quit();
        print("Game Exited");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
