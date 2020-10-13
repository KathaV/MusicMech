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
  //public float songBpm;

  //The number of seconds for each song beat
  //public float secPerBeat;

  //Current song position, in seconds
  //public float songPosition;

  //Current song position, in beats
  //public float songPositionInBeats;

  //How many seconds have passed since the song started
  //public float dspSongTime;

  //an AudioSource attached to this GameObject that will play the music.
  //public AudioSource musicSource;
    public AudioSource wrongSource;
    //the number of beats in each loop
    //public float beatsPerLoop;

  //the total number of loops completed since the looping clip first started
  //public int completedLoops = 0;

  //The current position of the song within the loop in beats.
  //public float loopPositionInBeats;

  //The current relative position of the song within the loop measured between 0 and 1.
  //public float loopPositionInAnalog;

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

    private bool isTeaching;
    private bool isPlaying;

    private GameObject button1;
    private GameObject button2;
    private GameObject button3;
    private GameObject button4;
    private GameObject canvas;

    private float dspSongTime;
    private int maxLoops = 2;


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
        //songScript = song.gameObject.GetComponent<Song>();
        //songBpm = songScript.getBpm();
        //beatsPerLoop = songScript.getBeatsPerLoop();
        //Load the AudioSource attached to the Conductor GameObject
        //musicSource = song.GetComponent<AudioSource>();
        wrongSource = this.GetComponent<AudioSource>();
        //Calculate the number of seconds in each beat
        //secPerBeat = 60f / songBpm;
        isTeaching = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        //if start teaching (temporary key: space)
        if (Input.GetKeyDown("space"))
        {
            isTeaching = true;
            //start music
            dspSongTime= startMusic(song);
        }

        //if teacher is teaching or musician is playing
        if (isTeaching || isPlaying)
        {
            
            List<Note> currNotes = playMusic(dspSongTime, songNotes, song, maxLoops);
            if (isTeaching)
            {

                teachPattern(currNotes);
            }
            else if (isPlaying)
            {
                bool isValid = validateInput(musician);
                if (!isValid)
                {
                    stopMusic(song);
                    wrongSource.Play();
                }
            }
        }
        
        

        
    }

    bool validateInput(GameObject musician)
    {
        return true;
    }
    
    void teachPattern(List<Note> currNotes)
    {
        resetInputBool();
        checkInput(currNotes);
        renderInput();
    }

    void stopMusic(AudioSource audioSource)
    {
        audioSource.Stop();
    }
    void stopMusic(GameObject song)
    {
        Song songScript = song.gameObject.GetComponent<Song>();
        AudioSource audioSource = songScript.GetComponent<AudioSource>();
        audioSource.Stop();
    }

    float startMusic(AudioSource audioSource)
    {

        //Record the time when the music starts
        float dspSongTime = (float)AudioSettings.dspTime;

        //Start the music
        audioSource.Play();
        return dspSongTime;
    }

    float startMusic(GameObject song)
    {
        Song songScript = song.gameObject.GetComponent<Song>();
        AudioSource audioSource = songScript.GetComponent<AudioSource>();
        //Record the time when the music starts
        float dspSongTime = (float)AudioSettings.dspTime;
        songScript.resetLoop();
        //Start the music
        audioSource.Play();
        return dspSongTime;
    }

    List<Note> playMusic(float dspSongTime, SongTranslator songNotes, GameObject song, int maxLoops)
    {
        Song songScript = song.gameObject.GetComponent<Song>();
        int songBpm = songScript.getBpm();
        int beatsPerLoop = songScript.getBeatsPerLoop();
        //Load the AudioSource attached to the Conductor GameObject
        AudioSource audioSource = song.GetComponent<AudioSource>();
        //Calculate the number of seconds in each beat
        float secPerBeat = 60f / songBpm;

        //determine how many seconds since the song started
        float songPosition = (float)(AudioSettings.dspTime - dspSongTime);

        //determine how many beats since the song started
        float songPositionInBeats = songPosition / secPerBeat;

        if (songPositionInBeats >= (songScript.getCurrLoop() + 1) * beatsPerLoop)
        {
            songScript.incrementLoop();
            loopMusic(audioSource, songNotes);
        }

        float loopPositionInBeats = songPositionInBeats - songScript.getCurrLoop() * beatsPerLoop;
        float loopPositionInAnalog = loopPositionInBeats / beatsPerLoop;

        songNotes.removeNotes(loopPositionInBeats);
        songNotes.updateCurrNotes(loopPositionInBeats);
        List<Note> currNotes = songNotes.getCurrNotes();
        return currNotes;
    }
    void loopMusic(AudioSource audioSource, SongTranslator songNotes)
    {
        songNotes.newLoop();
        audioSource.Play();
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

            //print("checkInput:"+note);
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
