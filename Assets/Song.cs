using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Song : MonoBehaviour
{
  
  public AudioSource musicSource;
  public int songBpm;
  public int beatsPerLoop;
    public int completedLoops;



  void Start()
  {
    musicSource = this.GetComponent<AudioSource>();
        completedLoops = 0;
  }

  public int getBpm()
  {
    return songBpm;
  }

  public int getBeatsPerLoop()
  {
    return beatsPerLoop;
  }

  public int getCurrLoop()
  {
        return completedLoops;
  }

  public void incrementLoop()
  {
        completedLoops++;
  }
  public void resetLoop()
    {
        completedLoops = 0;
    }

 
}

