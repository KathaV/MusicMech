using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonColourer : MonoBehaviour
{
    public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    public GameObject button4;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TurnWhite(string button)
    {
        //print("turn white on: " + button);
        if (button.Equals("1"))
        {
            button1.GetComponent<Image>().color = Color.white;
        }
        else if(button.Equals("2"))
        {
            button2.GetComponent<Image>().color = Color.white;
        }
        else if (button.Equals("3"))
        {
            button3.GetComponent<Image>().color = Color.white;
        }
        else if (button.Equals("4"))
        {
            button4.GetComponent<Image>().color = Color.white;
        }
    }

    public void TurnGreen (string button)
    {
        //print("turn green on: " + button);
        if (button.Equals("1"))
        {
            button1.GetComponent<Image>().color = Color.green;
        }
        else if (button.Equals("2"))
        {
            button2.GetComponent<Image>().color = Color.green;
        }
        else if (button.Equals("3"))
        {
            button3.GetComponent<Image>().color = Color.green;
        }
        else if (button.Equals("4"))
        {
            button4.GetComponent<Image>().color = Color.green;
        }
    }
}
