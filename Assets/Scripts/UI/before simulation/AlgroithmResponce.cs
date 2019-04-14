using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlgroithmResponce : MonoBehaviour
{
    public string AlgoSelected;
    
    public void SetAlgoSelected(string choice)
    {
        AlgoSelected = choice;
        Debug.Log(AlgoSelected);
    }

    public string GetAlgoSelected()
    {
        return AlgoSelected;
    }
}