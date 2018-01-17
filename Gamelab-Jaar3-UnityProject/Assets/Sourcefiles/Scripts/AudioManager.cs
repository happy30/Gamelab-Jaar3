using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	AudioSource source;
	float fadeOutSpeed;

	public enum AudioClips
	{
		BGM_Eerie,
		BGM_CC_Escape
	};


	void Awake()
	{
		source = GetComponent<AudioSource>();
	}
	// Use this for initialization
	void Start ()
	{
		fadeOutSpeed = 0.5f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void StopBGM()
	{
		StartCoroutine(FadeOut());
	}

	IEnumerator FadeOut()
	{
		while(source.volume > 0)
		{
			print("fading out");
			source.volume -= fadeOutSpeed * Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		yield break;
	}
}
