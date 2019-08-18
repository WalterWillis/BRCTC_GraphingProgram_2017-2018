﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphStreamCoordinates : MonoBehaviour
{
    private void Update()
    {
        if(Input.mousePosition.x < 24 && Input.mousePosition.x >= 0)
        {
            Vector3 p = new Vector3();
            Camera c = Camera.main;
            Event e = Event.current;
            Vector2 mousePos = new Vector2();

            // Get the mouse position from Event.
            // Note that the y position from Event is inverted.
            mousePos.x = e.mousePosition.x;
            mousePos.y = c.pixelHeight - e.mousePosition.y;

            p = c.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, c.nearClipPlane));

            GUI.Box(new Rect(20, 20, 250, 120), Input.mousePosition.x.ToString());
            //GUILayout.Label("Screen pixels: " + c.pixelWidth + ":" + c.pixelHeight);
            //GUILayout.Label("Mouse position: " + mousePos);
            //GUILayout.Label("World position: " + p.ToString("F3"));
            //GUILayout.EndArea();
        }
    }
    private void OnMouseOver()
    {
        
    }
}
