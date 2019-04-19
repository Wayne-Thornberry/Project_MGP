using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class TrafficLight : MonoBehaviour
{

	public int Speed;
	public int Stage { get; set; }
	public float ElapsedTime { get; set; }
	public int Weight;

	public delegate void LightChanged();
	public event LightChanged OnLightChanged;

	public TrafficLight()
	{
		Randoms = new System.Random();
		TimeToSwitch = Randoms.Next(5,15);
	}

	public Random Randoms { get; set; }

	private void Start()
	{
		Renders = gameObject.GetComponentsInChildren<MeshRenderer>();
		foreach (var r in Renders)
		{
			r.material.shader = Shader.Find("_Color");
			r.material.SetColor("_Color", Color.green);
			r.material.shader = Shader.Find("Specular");
			r.material.SetColor("_SpecColor", Color.green);
		}
	}

	public MeshRenderer[] Renders { get; set; }

	void Update ()
	{
	    ElapsedTime += Time.deltaTime;
		if (!(ElapsedTime > TimeToSwitch)) return;
		NextStage();
		ElapsedTime = 0;
	}

	public float TimeToSwitch { get; set; }

	private void NextStage()
	{
		Stage++;
		if (Stage > 2) Stage = 0;
		switch (Stage)
		{
			case 0: 
				Speed = 30; 
				foreach (var r in Renders)
				{
					r.material.shader = Shader.Find("_Color");
					r.material.SetColor("_Color", Color.green);
					r.material.shader = Shader.Find("Specular");
					r.material.SetColor("_SpecColor", Color.green);
				}
				break;
			case 1: Speed = 5; 
				
				foreach (var r in Renders)
				{
					r.material.shader = Shader.Find("_Color");
					r.material.SetColor("_Color", Color.yellow);
					r.material.shader = Shader.Find("Specular");
					r.material.SetColor("_SpecColor", Color.yellow);
				}
				break;
			case 2: Speed = 0; 
				
				foreach (var r in Renders)
				{
					r.material.shader = Shader.Find("_Color");
					r.material.SetColor("_Color", Color.red);
					r.material.shader = Shader.Find("Specular");
					r.material.SetColor("_SpecColor", Color.red);
				}break;
			default: Speed = 30; break;
		}
		if (OnLightChanged != null) OnLightChanged.Invoke();
	}
}
