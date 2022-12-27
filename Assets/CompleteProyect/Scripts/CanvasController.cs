using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class CanvasController : MonoBehaviour
{
    public GameObject canvas;

    public void StartMenu(string name){
        SceneManager.LoadScene(name);
    }
    
    public void StartPlay(string name){
        SceneManager.LoadScene(name);
    }

    public void StartCredits(string name){
        SceneManager.LoadScene(name);
    }

    public void StartInstruction(string name){
        SceneManager.LoadScene(name);
    }

    public void Exit(){
        Application.Quit();
    }
}
