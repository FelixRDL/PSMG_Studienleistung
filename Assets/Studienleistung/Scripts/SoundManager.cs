/// <summary>
/// Sound manager.
/// 
/// Controls all audio input within the game.
/// </summary>

using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
	// identifier strings for sounds
	public const string BOOM_SOUND = "boom", TRIGGER_SOUND = "trigger", DOOR_SOUND = "door", WIN_SOUND = "win", TALK_SOUND = "talk";
	// sound clips, that will be embedded into their players on Awake
	public AudioClip boomAudio, triggerAudio, doorAudio, winAudio, talkAudio;
	// actual audio players for sound playback
	private AudioSource boomPlayer, triggerPlayer, doorPlayer, winPlayer, talkPlayer;
	void Awake ()
	{
		// create audio players
		boomPlayer = gameObject.AddComponent<AudioSource> ();
		triggerPlayer = gameObject.AddComponent<AudioSource> ();
		doorPlayer = gameObject.AddComponent<AudioSource> ();
		winPlayer = gameObject.AddComponent<AudioSource> ();
		talkPlayer = gameObject.AddComponent<AudioSource> ();

		// assign sounds to audio players
		boomPlayer.clip = boomAudio;
		triggerPlayer.clip = triggerAudio;
		doorPlayer.clip = doorAudio;
		winPlayer.clip = winAudio;
		winPlayer.volume = .2f;
		talkPlayer.clip = talkAudio;
	}

	/// <summary>
	/// Audio function awaiting external calls. Use class constants for accessing sound ID strings
	/// </summary>
	/// <param name="SoundID">Sound I.</param>
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
