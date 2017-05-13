using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustScript : MonoBehaviour {

	void OnGUI() {
        if(GUI.Button(new Rect(10,100,100,30), "Health Up")) {
            GameControl.control.health += 10;
        }
        if(GUI.Button(new Rect(10,140,100,30), "Health Down")) {
            GameControl.control.health -= 10;
        }
        if(GUI.Button(new Rect(10,180,100,30), "Experience Up")) {
            GameControl.control.experience += 10;
        }
        if(GUI.Button(new Rect(10,220,100,30), "Experience Down")) {
            GameControl.control.experience -= 10;
        }
        if(GUI.Button(new Rect(10,260,100,30), "Save")) {
            GameControl.control.Save();
        }
        if(GUI.Button(new Rect(10,300,100,30), "Load")) {
            GameControl.control.Load();
        }

    }
}
