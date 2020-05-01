using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    //Button for exiting application
    private Button button;  
    // Finding button object and adding listener.
    void Start()
    {
        button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(Exit);
    }

    // Exiting game
    private void Exit()
    {
        Application.Quit();
    }
}
