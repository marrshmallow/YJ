using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class AudioXFade : MonoBehaviour
{
    [SerializeField] private AudioSource videoSound;
    [SerializeField] private float fadeTime = 0.5f;

    private void Awake()
    {
        fadeTime = 0.5f;
    }

    private void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(FadeOut(videoSound, fadeTime));
        }
    }
    
    private void OnTriggerExit (Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(FadeIn(videoSound, fadeTime));
            Debug.Log("???");
        }
    }

    public IEnumerator FadeOut(AudioSource videoSound, float fadeTime)
    {   
        float startVolume = videoSound.volume;
        while (videoSound.volume > 0)
        {
            videoSound.volume -= startVolume * Time.deltaTime / fadeTime;
            yield return null;
        }
        videoSound.Stop();
    }

        public IEnumerator FadeIn(AudioSource videoSound, float fadeTime)
    {   
        float startVolume = videoSound.volume;
        while (videoSound.volume < 1)
        {
            videoSound.volume += Time.deltaTime / fadeTime;
            yield return null;
        }
        videoSound.Play();
    }
}
