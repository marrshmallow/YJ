using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class AudioXFade : MonoBehaviour
{
    [SerializeField] private AudioSource videoSound;
    [SerializeField] private float fadeInTime = 0.5f;
    [SerializeField] private float fadeOutTime = 3f;
    [SerializeField] private PlayableDirector director;
    private bool inLobby = true;

    private void Awake()
    {
        fadeInTime = 0.5f;
    }

    private void Update()
    {
        if (inLobby)
        {
            if (director.state == PlayState.Playing)
                StartCoroutine(FadeOut(videoSound, fadeInTime));
            else
                StartCoroutine(FadeIn(videoSound, fadeInTime));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inLobby = false;
            StartCoroutine(FadeOut(videoSound, fadeInTime));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inLobby = true;
            StartCoroutine(FadeIn(videoSound, fadeInTime));
        }
    }

    public IEnumerator FadeOut(AudioSource videoSound, float fadeInTime)
    {
        float startVolume = videoSound.volume;
        while (videoSound.volume > 0)
        {
            videoSound.volume -= startVolume * Time.deltaTime / fadeInTime;
            yield return null;
        }
        videoSound.Stop();
    }

    public IEnumerator FadeIn(AudioSource videoSound, float fadeInTime)
    {
        float startVolume = videoSound.volume;
        while (videoSound.volume < 1)
        {
            videoSound.volume += Time.deltaTime / fadeInTime;
            yield return null;
        }
        videoSound.Play();
    }
}
