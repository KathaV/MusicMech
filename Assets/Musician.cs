using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Musician : MonoBehaviour
{
    public bool isPlaying;
    Dictionary<string, bool> notesPlaying;
    // Start is called before the first frame update
    void Start()
    {
        isPlaying = false;
        notesPlaying = new Dictionary<string, bool>();
        notesPlaying.Add("1", false);
        notesPlaying.Add("2", false);
        notesPlaying.Add("3", false);
        notesPlaying.Add("4", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("1")|| Input.GetKey("2") || Input.GetKey("3") || Input.GetKey("4"))
        {
            isPlaying = true;
        }
        if (isPlaying)
        {
            if (Input.GetKey("1"))
            {
                notesPlaying["1"] = true;
            }
            else
            {
                notesPlaying["1"] = false;
            }
            if (Input.GetKey("2"))
            {
                notesPlaying["2"] = true;
            }
            else
            {
                notesPlaying["2"] = false;
            }
            if (Input.GetKey("3"))
            {
                notesPlaying["3"] = true;
            }
            else
            {
                notesPlaying["3"] = false;
            }
            if (Input.GetKey("4"))
            {
                notesPlaying["4"] = true;
            }
            else
            {
                notesPlaying["4"] = false;
            }
        }
    }
}
