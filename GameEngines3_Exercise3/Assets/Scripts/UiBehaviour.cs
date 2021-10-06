/*
Nathan Nguyen
George Brown College
Assignment 1 - GAME2014-F2021
101268067
9/26/2021
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiBehaviour : MonoBehaviour
{
    private int nextSceneIndex;
    private int previousSceneIndex;

    [SerializeField]
    public string SceneSelect;

    void Start()
    {
        nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        previousSceneIndex = SceneManager.GetActiveScene().buildIndex - 1;
    }

    //Next Button
    public void OnNextButtonPressed()
    {
        SceneManager.LoadScene(nextSceneIndex);
        Debug.Log(SceneManager.GetActiveScene().buildIndex);
    }

    //Back Button
    public void UnloadScene()
    {
        SceneManager.LoadScene("Main");
    }

    public void OnExitButtonPressed()
    {
        Application.Quit();  
    }


    public void ButtonGeneralPressed()
    {
        SceneManager.LoadScene(SceneSelect);
        Debug.Log(SceneManager.GetActiveScene().buildIndex);
    }

}
