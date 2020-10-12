using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* class representation of a note to be played*/
public class Note
{

    private int startTime;
    private int noteVal;
    private int endTime;
    private string inputKey;

    /* constructor*/
    public Note(int startTime, int noteVal)
    {
      this.startTime = startTime;
      this.noteVal = noteVal;
    }

    public Note(int startTime, int noteVal, int endTime, string inputKey)
    {
        this.startTime = startTime;
        this.noteVal = noteVal;
        this.endTime = endTime;
        this.inputKey = inputKey; 
    }

    /* end of note, hence assigning duration*/
    public void hasEnded(int timestamp)
    {
      this.endTime = timestamp;
    }

    public void setInput(string inputKey)
    {
        this.inputKey = inputKey;
    }

    public string getInput()
    {
        return inputKey;
    }
    public int getStart()
    {
      return startTime;
    }

    public int getVal()
    {
      return noteVal;
    }

    public int getEnd()
    {
      return endTime;
    }

    public float getEndInBeats()
    {
        return (float) endTime / 4;
    }
    public float getStartInBeats()
    {
        return (float) startTime / 4;
    }


    public override string ToString()
    {
        return "Start: " + startTime + ", End: " + endTime + ", Val: " + noteVal + ", Input: " + inputKey;

    }
}
