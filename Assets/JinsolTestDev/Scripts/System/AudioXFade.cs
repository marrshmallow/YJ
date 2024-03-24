using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class AudioXFade : MonoBehaviour
{
    [SerializeField] private AudioSource videoSound;
    [SerializeField] private float fadeTime = 1f;
    private float cutsceneFadeTime = 0.5f;
    [SerializeField] private PlayableDirector director;
    private bool inLobby = true;

    private void Update()
    {
        if (inLobby)
        {
            if (director.state == PlayState.Playing)
                StartCoroutine(FadeOut(videoSound, cutsceneFadeTime));
            else
                StartCoroutine(FadeIn(videoSound, fadeTime));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inLobby = false;
            StartCoroutine(FadeOut(videoSound, fadeTime));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inLobby = true;
            StartCoroutine(FadeIn(videoSound, fadeTime));
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
