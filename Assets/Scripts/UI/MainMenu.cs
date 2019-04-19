using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
	public Button StartBtn;
	public Button ConfigBtn;
	public Button QuitBtn;

	public Button[] LevelsBtn;

	public GameObject LevelSelectPanel;
	public GameObject GameConfigPanel;


	public Toggle UseCustomToggle;
	public Toggle UseDikjstraAlgo;
	public Toggle EnableCarDespawnToggle;
	public InputField InputCarsField;
	public InputField InputIntervialField;
	

	// Use this for initialization
	void Start ()
	{
		StartBtn.onClick.AddListener(StartBtnClicked);
		ConfigBtn.onClick.AddListener(ConfigBtnClicked);
		QuitBtn.onClick.AddListener(QuitBtnClicked);
		
		LevelSelectPanel.SetActive(false);
		GameConfigPanel.SetActive(false);

		foreach (var btn in LevelsBtn)
		{
			btn.onClick.AddListener(delegate { LevelBtnClicked(btn.name); });
		}
	}

	private void LevelBtnClicked(string name)
	{
		if (UseCustomToggle.isOn)
		{
			GameConfig.UseCustom = UseCustomToggle.isOn;
			GameConfig.UseDijkstra = UseDikjstraAlgo.isOn;
			AI.EnableCarDespawning = EnableCarDespawnToggle.isOn;
			try
			{
				GameConfig.CarLimit = int.Parse(InputCarsField.text);
				GameConfig.CarIntervol = float.Parse(InputIntervialField.text);
			}
			catch (Exception e)
			{
				GameConfig.CarLimit = 10;
			}
		}

		switch (name)
		{
			case "rbLevel":
				SceneManager.LoadScene("Demo_Roundabout_Scene");break;
			case "tlLevel":
				SceneManager.LoadScene("Demo_TL_Scene");break;
			case "cLevel":
				SceneManager.LoadScene("Demo_Complex_Scene");break;
			case "ciLevel":
				SceneManager.LoadScene("Demo_City_Scene");break;
			case "mwLevel":
				SceneManager.LoadScene("Demo_Motorway_Scene");break;
		}
	}

	private void QuitBtnClicked()
	{
		Application.Quit();
	}

	private void ConfigBtnClicked()
	{
		GameConfigPanel.SetActive(!GameConfigPanel.activeSelf);
		LevelSelectPanel.SetActive(false);
	}

	private void StartBtnClicked()
	{
		GameConfigPanel.SetActive(false);
		LevelSelectPanel.SetActive(!LevelSelectPanel.activeSelf);
	}

	// Update is called once per frame
	void Update () {
		
	}
}
