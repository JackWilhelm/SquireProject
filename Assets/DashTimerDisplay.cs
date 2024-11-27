using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashTimerDisplay : MonoBehaviour
{
    private Text textComponent;
    public PlayerMovement playerMovement;
    // Start is called before the first frame update
    void Start()
    {
        textComponent = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMovement.CanDash()) {
            textComponent.text = "Dash Ready!";
        } else {
            textComponent.text = "Dash Not Ready";
        }
        
    }
}
