using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using New.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Freecam : MonoBehaviour
{

    private AI _focuedObject;
    
    // Use this for initialization
    void Start ()
    {
        
    }
	
    // Update is called once per frame
    void Update () {
        if (!FixedCam)
        {
            if (Input.GetKey(KeyCode.W))
            {
                gameObject.transform.position =
                    gameObject.transform.position + gameObject.transform.up * (Time.deltaTime * 100f);
            }

            if (Input.GetKey(KeyCode.S))
            {
                gameObject.transform.position =
                    gameObject.transform.position - gameObject.transform.up * (Time.deltaTime * 100f);
            }

            if (Math.Abs(Input.GetAxis("Mouse ScrollWheel")) > 0f)
            {
                gameObject.transform.position = gameObject.transform.position + gameObject.transform.forward *
                                                ((Input.GetAxis("Mouse ScrollWheel") * 10f) * CamSensitivity);
            }

            if (Input.GetKey(KeyCode.D))
            {
                gameObject.transform.position =
                    gameObject.transform.position + gameObject.transform.right * (Time.deltaTime * 100f);
            }

            if (Input.GetKey(KeyCode.A))
            {
                gameObject.transform.position =
                    gameObject.transform.position - gameObject.transform.right * (Time.deltaTime * 100f);
            }
        }
        
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            foreach (var car in SpawnController.WorldCars)
            {
                car.RunPath = !car.RunPath;
            }
            SpawnController.DisableSpawning = !SpawnController.DisableSpawning;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("TitleScene");
            SpawnController.Cars = 0;
            SpawnController.Log = "";
        }

        
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            SceneManager.LoadScene("EndScene");
        }
        
        if (Input.GetMouseButtonDown(1))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 10000f))
                {
                    try
                    {
                        var ai = hit.transform.GetComponentInParent<AI>();
                        ai.Kill();
                    }
                    catch (Exception e)
                    {

                    }
                }
            }

            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 10000f))
                {
                    try
                    {
                        if (_focuedObject != null)
                        {
                            _focuedObject.IsFocused = false;
                        }
                        _focuedObject = hit.transform.GetComponentInParent<AI>();
                        _focuedObject.IsFocused = true;
                        GameConfig.HudReference.UpdateHUD(_focuedObject);
                        GameConfig.HudReference.InfoPanel.SetActive(true);
                        //Debug.Log(_focuedObject);
                    }
                    catch (Exception e)
                    {

                    }
                }
            
        }


    }

    public bool FixedCam;

    public float CamSensitivity;
}
