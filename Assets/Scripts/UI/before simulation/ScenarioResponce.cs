using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScenarioResponce : MonoBehaviour
{
    public string ScenarioChoosen;

    public void SetScenarioChoosen(string choice)
    {
        ScenarioChoosen = choice;
        Debug.Log(GetScenarioChoosen());
    }

    public String GetScenarioChoosen()
    {
        return ScenarioChoosen;
    }

    void Awake()
    {
        DontDestroyOnLoad(this);
    }
}