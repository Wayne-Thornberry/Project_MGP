using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class duringSimUI : MonoBehaviour
{
    public int AI_amount;   // Amount of AI running
    public Text RunningAIsText;

    public double AvgTime;  // Avg processing time for ai to calculate 
    public Text AverageTimeText;

    void Start ()
    {
        AI_amount = 0;
        setRunningAIsText();

        AvgTime = 0.0;
        setAverageTimeText();

    }

    void Update ()
    {
        setRunningAIsText();
        setAverageTimeText();


    }

    void setRunningAIsText()
    {
        RunningAIsText.text = "AI amount: " + AI_amount.ToString();
    }

    void setAverageTimeText()
    {
        AverageTimeText.text = "Avg Time: " + AvgTime.ToString();
    }

    void setAvgTime()
    {
        double total_time = 0.0;

        double time_taken = 0.0; // time it took to calculate its path variable 

        // loop through each AI to add up each idividuals time
        for (int i=0; i< AI_amount;i++)
        {
            total_time += time_taken; 
        }
        AvgTime = total_time / AI_amount; // total time divided by total of AIs
    }

    // when each AI is created num of them is incremented to be passed to here
    public void setAI_Amount(int num)
    {
        AI_amount = num;
    }
}


//RunningAisText