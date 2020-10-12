using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Song : MonoBehaviour
{
  
  public AudioSource musicSource;
  public int songBpm;
  public int beatsPerLoop;



  void Start()
  {
    musicSource = this.GetComponent<AudioSource>();
  }

  public int getBpm()
  {
    return songBpm;
  }

  public int getBeatsPerLoop()
  {
    return beatsPerLoop;
  }



 
}

