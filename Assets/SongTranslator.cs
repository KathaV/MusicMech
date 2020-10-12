using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System.Diagnostics;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.InteropServices;
using System.CodeDom;

public class SongTranslator : MonoBehaviour
{
    public TextAsset beatTrack;
    //public int interval = 128;
    private const int NOTE = 3;
    private const int NOTE_VAL = 1;
    private const int IS_PLAYING = 1;
    private const int TIMESTAMP = 0;
    private bool onNotes = false;
    //number of lines in text to be ignored (trailing AND leading lines), and are not part of the notes.
    //public int textDisplace = 5;
    //smallest unit of note (in this case: a semi-quaver) in terms of timestamp units
    public int noteUnit = 32;
    private int currNote;
    private List<Note> notes;
    private List<Note> currNotes;
    private List<int> noteVals;
    private int nextNote;
    //private const int CHANNEL = 2;
    //private const int VOLUME = 4;

    void Awake()
    {
        string text = beatTrack.text;

        string[] lines = text.Split(System.Environment.NewLine.ToCharArray());
        notes = new List<Note>();
        currNote = 0;
        currNotes = new List<Note>();
        noteVals = new List<int>();
        foreach (string line in lines)
        {
            int timestamp = -1, noteVal = -1;

            //print(line.Split(' ')[TIMESTAMP]);
            //print("length:" + line.Split(' ').Length);
            if (line.Split(' ').Length != 5)
            {

                continue;
            }
            try
            {
                timestamp = Int32.Parse(line.Split(' ')[TIMESTAMP]);
                Console.WriteLine(timestamp);
            }
            catch (FormatException)
            {
                Console.WriteLine($"Unable to parse '{line.Split(' ')[TIMESTAMP]}'");
            }
            string isPlaying = (line.Split(' '))[IS_PLAYING];


            try
            {
                noteVal = Int32.Parse(noteParser(line.Split(' ')[NOTE]));
                print("note: " + noteVal);
            }
            catch (FormatException)
            {
                print($"Unable to parse '{line.Split(' ')[NOTE]}'");
            }


            // check if text has started on the track notes (ie, we have received a line that has 'On' in isPlaying)
            if (isPlaying.Equals("On"))
            {
                onNotes = true;
            }

            if (onNotes)
            {
                if (isPlaying.Equals("On"))
                {

                    notes.Add(new Note(timestamp / noteUnit, noteVal));
                    noteVals.Add(noteVal);
                }

                else if (isPlaying.Equals("Off"))
                {
                    notes[currNote].hasEnded(timestamp / noteUnit);

                    currNote++;

                }
            }
        }
        currNotes = new List<Note>();
        toInput();
        //checkNotes();
        nextNote = 0;

    }

    void checkNotes()
    {
        foreach (Note note in notes)
        {
            string test = "start: ";
            test += note.getStart();
            test += ", end: " + note.getEnd();
            test += ", val: " + note.getVal();
            test += ", input: " + note.getInput();
            print(test);
        }
    }

    string noteParser(string note)
    {
        return note.Split('=')[1];
    }

    void toInput()
    {
        //print("Medians: "+ MyMath.FirstQuartile(noteVals)+", "+MyMath.Median(noteVals)+", " + MyMath.ThirdQuartile(noteVals));
        int firstQuartile = MyMath.FirstQuartile(noteVals);
        int median = MyMath.Median(noteVals);
        int thirdQuartile = MyMath.ThirdQuartile(noteVals);


        foreach (Note note in notes)
        {
            if (note.getVal() <= firstQuartile)
            {
                note.setInput("1");
            }
            else if (note.getVal() <= median)
            {
                note.setInput("2");
            }
            else if (note.getVal() <= thirdQuartile)
            {
                note.setInput("3");
            }
            else
            {
                note.setInput("4");
            }
        }
    }

    public void updateCurrNotes(float loopPosition)
    {
        print("UPDATE");
        //print("nextToRemove:" + nextToRemove + ", nextToPlay: " + nextNote +"timestamp:" +loopPosition);
        //currNotes = new List<Note>();
        int i=-1;
        checkCurrNotes();

        print("pre-nextToPlay: " + nextNote + ", count"+ notes.Count);
        //for (i = 0; i < notes.Count; i++)
       
            for (i = nextNote; i < notes.Count && notes[i].getStartInBeats() <= loopPosition; i++)
            {
                print("added:" + notes[i]);
                currNotes.Add(notes[i]);

                nextNote = i + 1;
            }
        
        print("nextToPlay: " + nextNote);
        checkCurrNotes();

        
            
    }

    Note copyNote(Note note)
    {
        print(note);
        Note copy = new Note(note.getStart(), note.getVal(), note.getEnd(), note.getInput());
        print("copy:" + copy);
        return copy;
    }

    void checkCurrNotes()
    {
        print("curr lenth: "+ currNotes.Count);
        foreach(Note note in currNotes)
        {
            print("check:"+note);
        }
    }

    public List<Note> getCurrNotes()
    {
        return currNotes;
    }

    public void removeNotes(float timestamp)
    {
        print("REMOVE");
        //print("nextToRemove:" + nextToRemove + ", nextToPlay: " + nextNote);
        List<Note> temp = new List<Note>();
        foreach(Note note in currNotes)
        {
            if (note.getEndInBeats() > timestamp)
            {
                temp.Add(note);
                print("time:"+timestamp+", "+note);

                
            }

        }

        currNotes = temp;

        //checkCurrNotes();
    }

    public void newLoop()
    {
        currNotes = new List<Note>();
        nextNote = 0;
        print("LOOP");
        print("loop check: ");
        checkCurrNotes();
    }

}