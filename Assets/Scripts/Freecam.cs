using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freecam : MonoBehaviour
{

    public AI focus;
    
    // Use this for initialization
    void Start () {
		
    }
	
    // Update is called once per frame
    void Update () {
        if(Input.GetKey(KeyCode.W)){
            gameObject.transform.position = gameObject.transform.position + gameObject.transform.up * (Time.deltaTime * 100f);
        }
		
        if(Input.GetKey(KeyCode.S)){
            gameObject.transform.position = gameObject.transform.position - gameObject.transform.up * (Time.deltaTime * 100f);
        }
        
        if (Math.Abs(Input.GetAxis("Mouse ScrollWheel")) > 0f) {
            gameObject.transform.position = gameObject.transform.position + gameObject.transform.forward * ((Input.GetAxis("Mouse ScrollWheel") * 10f) * CamSensitivity);
        }
		
        if(Input.GetKey(KeyCode.D)){
            gameObject.transform.position = gameObject.transform.position + gameObject.transform.right * (Time.deltaTime * 100f);
        }
		
        if(Input.GetKey(KeyCode.A)){
            gameObject.transform.position = gameObject.transform.position - gameObject.transform.right * (Time.deltaTime * 100f);
        }

        if (Input.GetMouseButtonDown (1)){ 
            RaycastHit hit; 
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
            if ( Physics.Raycast (ray,out hit,10000f)) {
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
        
        if ( Input.GetMouseButtonDown (0)){ 
            RaycastHit hit; 
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
            if ( Physics.Raycast (ray,out hit,10000f)) {
                try
                {
                    if (focus != null)
                    {
                        focus.IsFocused = false;
                    }
                    focus = hit.transform.GetComponentInParent<AI>();
                    focus.IsFocused = true;
                    Debug.Log(focus);
                }
                catch (Exception e)
                {
                    
                }
            }
        }


        
    }

    public float CamSensitivity;
}
