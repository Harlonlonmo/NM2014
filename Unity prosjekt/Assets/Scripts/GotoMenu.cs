using UnityEngine;
using System.Collections;

public class GotoMenu : MonoBehaviour
{

    public string MenuLevelName = "Menu";
	
	// Update is called once per frame
	void Update () {

	    if (Input.GetButtonDown("Menu"))
	    {
	        Application.LoadLevel(MenuLevelName);
	    }
	}
}
