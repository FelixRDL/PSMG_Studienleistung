using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{

	public const string BOOM_SOUND = "boom", TRIGGER_SOUND = "trigger", DOOR_SOUND = "door", WIN_SOUND = "win", TALK_SOUND = "talk";
	public AudioClip boomAudio, triggerAudio, doorAudio, winAudio, talkAudio;
	private AudioSource boomPlayer, triggerPlayer, doorPlayer, winPlayer, talkPlayer;
	void Awake ()
	{
		boomPlayer = gameObject.AddComponent<AudioSource> ();
		triggerPlayer = gameObject.AddComponent<AudioSource> ();
		doorPlayer = gameObject.AddComponent<AudioSource> ();
		winPlayer = gameObject.AddComponent<AudioSource> ();
		talkPlayer = gameObject.AddComponent<AudioSource> ();

		boomPlayer.clip = boomAudio;
		triggerPlayer.clip = triggerAudio;
		doorPlayer.clip = doorAudio;
		winPlayer.clip = winAudio;
		talkPlayer.clip = talkAudio;
	}

	public void PlaySound (string SoundID)
	{
		switch (SoundID) {
		case BOOM_SOUND:
			boomPlayer.Play ();
			break;
		case TRIGGER_SOUND:
			triggerPlayer.Play ();
			break;
		case DOOR_SOUND:
			doorPlayer.Play ();
			break;
		case WIN_SOUND:
			winPlayer.Play ();
			break;
		case TALK_SOUND:
			talkPlayer.Play ();
			break;
		}
	}
}
