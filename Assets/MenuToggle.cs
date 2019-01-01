using System;
using UnityEngine;
using VRTK;

public class MenuToggle : MonoBehaviour {
public VRTK_ControllerEvents controllerEvents;
public GameObject menu;
bool menuState;

void OnEnable(){
	controllerEvents.ButtonTwoReleased += ControllerEvents_ButtonTwoReleased;
}

    void OnDisable(){
	controllerEvents.ButtonTwoReleased -= ControllerEvents_ButtonTwoReleased;
}
  private void ControllerEvents_ButtonTwoReleased(object sender, ControllerInteractionEventArgs e)
    {
		menuState = !menuState;
		menu.SetActive(menuState);
    }


}
