using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conductor : MonoBehaviour
{

  
  public GameObject song;

  public Song songScript;



  //Song beats per minute
  //This is determined by the song you're trying to sync up to
  public float songBpm;

  //The number of seconds for each song beat
  public float secPerBeat;

  //Current song position, in seconds
  public float songPosition;

  //Current song position, in beats
  public float songPositionInBeats;

  //How many seconds have passed since the song started
  public float dspSongTime;

  //an AudioSource attached to this GameObject that will play the music.
  public AudioSource musicSource;
    public AudioSource wrongSource;
    //the number of beats in each loop
    public float beatsPerLoop;

  //the total number of loops completed since the looping clip first started
  public int completedLoops = 0;

  //The current position of the song within the loop in beats.
  public float loopPositionInBeats;

  //The current relative position of the song within the loop measured between 0 and 1.
  public float loopPositionInAnalog;

  //Conductor instance
  public static Conductor instance;
    
    //error allowance
    public float errorAllowance = 1/60;
    private ButtonColourer buttonColourer;

  private GameObject musician;
    private SongTranslator songNotes;

    private bool button1IsPlaying;
    private bool button2IsPlaying;
    private bool button3IsPlaying;
    private bool button4IsPlaying;

    private GameObject button1;
    private GameObject button2;
    private GameObject button3;
    private GameObject button4;
    private GameObject canvas;
    


  void Awake()
  {
    instance = this;
  }

  // Start is called before the first frame update
    void Start()
    {
        //button1 = GameObject.Find("Canvas").transform.Find("Button 1");
        //button1 = GameObject.Find("Canvas").transform.Find("Button 2");
        //button1 = GameObject.Find("Canvas").transform.Find("Button 3");
        //button1 = GameObject.Find("Canvas").transform.Find("Button 4");

       
        songNotes = GameObject.FindWithTag("SongTranslator").gameObject.GetComponent<SongTranslator>();
        canvas = GameObject.Find("Canvas");
        buttonColourer=canvas.gameObject.GetComponent<ButtonColourer>();
        song = GameObject.FindWithTag("Track");
        musician = GameObject.FindWithTag("Player");
        songScript = song.gameObject.GetComponent<Song>();
        songBpm = songScript.getBpm();
        beatsPerLoop = songScript.getBeatsPerLoop();
        //Load the AudioSource attached to the Conductor GameObject
        musicSource = song.GetComponent<AudioSource>();
        wrongSource = this.GetComponent<AudioSource>();
        //Calculate the number of seconds in each beat
        secPerBeat = 60f / songBpm;

          //Record the time when the music starts
        dspSongTime = (float)AudioSettings.dspTime;

          //Start the music
        musicSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
          //determine how many seconds since the song started
        songPosition = (float)(AudioSettings.dspTime - dspSongTime);

          //determine how many beats since the song started
        songPositionInBeats = songPosition / secPerBeat;

        if (songPositionInBeats >= (completedLoops + 1) * beatsPerLoop)
        {
           completedLoops++;
            print("checkp 0");
            songNotes.newLoop();
            musicSource.Play();
            print("checkp 2");
        }

        loopPositionInBeats = songPositionInBeats - completedLoops * beatsPerLoop;
        loopPositionInAnalog = loopPositionInBeats / beatsPerLoop;

        print("loop beats:"+loopPositionInBeats);
            songNotes.removeNotes(loopPositionInBeats);
            songNotes.updateCurrNotes(loopPositionInBeats);
            List<Note> currNotes = songNotes.getCurrNotes();
        //TODO
        checkCurrNotes(currNotes);
            resetInputBool();
            checkInput(currNotes);
            renderInput();
        
        
    }
    void checkCurrNotes(List<Note> currNotes)
    {
        print("CONDUCTOR, curr notes in ");
        print("curr lenth: " + currNotes.Count);
        foreach (Note note in currNotes)
        {
            print("check:" + note);
        }
    }
    void checkInput(List<Note> currNotes)
    {
        foreach (Note note in currNotes)
        {

            print("checkInput:"+note);
            setInputBool(note.getInput());

            /*if (completedLoops>1 && !Input.GetKey(note.getInput()))
            {
                musicSource.Stop();

                wrongSource.Play();
            }*/
            
        }
    }

    void setInputBool(string inputKey)
    {
        if (inputKey.Equals("1"))
        {
            button1IsPlaying = true;
        }
        else if (inputKey.Equals("2"))
        {
            button2IsPlaying = true;
        }
        else if (inputKey.Equals("3"))
        {
            button3IsPlaying = true;
        }
        else if (inputKey.Equals("4"))
        {
            button4IsPlaying = true;
        }
    }

    void resetInputBool()
    {
        button1IsPlaying = false;
        button2IsPlaying = false;
        button3IsPlaying = false;
        button4IsPlaying = false;
    }

    void renderInput()
    {
        if (button1IsPlaying)
        {
            buttonColourer.TurnGreen("1");
        }
        else
        {
            buttonColourer.TurnWhite("1");
        }
        if (button2IsPlaying)
        {
            buttonColourer.TurnGreen("2");
        }
        else
        {
            buttonColourer.TurnWhite("2");
        }
        if (button3IsPlaying)
        {
            buttonColourer.TurnGreen("3");
        }
        else
        {
            buttonColourer.TurnWhite("3");
        }
        if (button4IsPlaying)
        {
            buttonColourer.TurnGreen("4");
        }
        else
        {
            buttonColourer.TurnWhite("4");
        }
    }
}
