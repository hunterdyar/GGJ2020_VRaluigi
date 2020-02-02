using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMusic : MonoBehaviour
{
    public bool startWithRandomSong;
    public bool randomSongEachTurn;
    public AudioClip[] trackList;
    
    int trackIndex = 0;
    int playingIndex;
    AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if(startWithRandomSong)
        {
            trackIndex = Random.Range(0,trackList.Length-1);
        }
        PlaySong(trackIndex);
    }

    public void PlaySong(int indexToPlay)
    {
        audioSource.clip = trackList[indexToPlay];
        StartCoroutine(WaitForSongEnd(audioSource.clip.length));
        audioSource.Play();
        playingIndex = indexToPlay;
    }
    IEnumerator WaitForSongEnd(float songLength)
    {
        yield return new WaitForSeconds(songLength);
        if(randomSongEachTurn)
        {
            while(trackIndex == playingIndex){//wont repeat itself twice in a row.
                trackIndex = Random.Range(0,trackList.Length-1);
            }
        }
        else
        {
            trackIndex++;
            if(trackIndex >= trackList.Length)
            {
                trackIndex = 0;
            }
        }
        PlaySong(trackIndex);
    }
}
