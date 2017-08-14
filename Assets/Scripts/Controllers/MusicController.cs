using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicController : MonoBehaviour {

    public Text songTextValue, MusicTextValue;

	// Use this for initialization
	void Start () {

        if (this.songTextValue.text != null)
        {
            ChangeText(this.songTextValue, PlayerPrefs.GetInt("song"));
        }

        if (this.MusicTextValue.text != null)
        {
            ChangeText(this.MusicTextValue, PlayerPrefs.GetInt("music"));
        }
    }
	
	public void _SetSong()
    {
        int value = 0;
        value = PlayerPrefs.GetInt("song");

        if (value == 1)
        {
            value = 0;
        } else
        {
            value = 1;
        }

        PlayerPrefs.SetInt("song", value);

        ChangeText(this.songTextValue, PlayerPrefs.GetInt("song"));
    }

    public void _SetMusic()
    {
        int value = 0;
        value = PlayerPrefs.GetInt("music");

        if (value == 1)
        {
            value = 0;
        }
        else
        {
            value = 1;
        }

        PlayerPrefs.SetInt("music", value);

        ChangeText(this.MusicTextValue, PlayerPrefs.GetInt("music"));
    }

    void ChangeText(Text textToChange, int Value)
    {
        if(Value == 1)
        {
            textToChange.text = "ON";
        } else
        {
            textToChange.text = "OFF";
        }
    }
}
