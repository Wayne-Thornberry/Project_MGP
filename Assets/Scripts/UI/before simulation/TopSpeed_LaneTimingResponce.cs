using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopSpeed_LaneTimingResponce : MonoBehaviour
{
    public int TopSpeed;
    public float LaneTimmig;

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void SetTopSpeed()
    {
        string SpeedField = GameObject.Find("SpeedField").GetComponent<InputField>().text; // Gets the text from the input field
        int speed = int.Parse(SpeedField); // The text gets converted to a int

        if (speed <= 0)
        {
            TopSpeed = 10;
        }
        else
        {
            TopSpeed = speed;
        }
        Debug.Log(TopSpeed);
    }

    public int GetTopSpeed()
    {
        return TopSpeed;
    }

    public void SetLaneTimmig()
    {
        string LaneField = GameObject.Find("LaneField").GetComponent<InputField>().text; // Gets the text from the input field
        float time = float.Parse(LaneField); // The text gets converted to a int

        if (time <= 0)
        {
            LaneTimmig = 5;
        }
        else
        {
            LaneTimmig = time;
        }
        Debug.Log(GetLaneTimmig());
    }

    public float GetLaneTimmig()
    {
        return LaneTimmig;
    }
}