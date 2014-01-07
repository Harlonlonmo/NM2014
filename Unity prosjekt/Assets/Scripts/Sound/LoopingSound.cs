using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class LoopingSound : MonoBehaviour
{

    public AudioClip StartSound;
    public AudioClip LoopSound;
    public AudioClip EndSound;

    private float waitTime;
    private bool playing = false;

    public void Play()
    {
        if (!playing)
            StartCoroutine(Loop());
    }

    private IEnumerator Loop()
    {
        audio.PlayOneShot(StartSound);
        waitTime = StartSound.length;
        playing = true;
        yield return new WaitForSeconds(waitTime);
        audio.clip = LoopSound;
        audio.Play();
        audio.loop = true;
        while (playing)
            yield return 0;
        audio.Stop();
        audio.PlayOneShot(EndSound);
        playing = false;
    }

    public void Stop()
    {
        playing = false;
    }
}
