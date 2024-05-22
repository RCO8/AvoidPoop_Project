using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    private AudioSource[] audioSources;
    [SerializeField] private AudioClip bgmClip;
    [SerializeField] private AudioMixer audioMixer;

    private void Awake()
    {
        if (null == Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);

        audioSources = GetComponents<AudioSource>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        audioMixer.SetFloat("BGM", 0.1f);
        audioMixer.SetFloat("Effect", 1f);

        audioSources[0].clip = bgmClip;
        audioSources[0].Play();
    }

    public void PlayBGM(AudioClip newClip)
    {
        audioSources[0].Stop();

        audioSources[0].clip = newClip;
        audioSources[0].Play();
    }

    public void PlayEffect(AudioClip newClip)
    {
        audioSources[1].PlayOneShot(newClip);
    }

    public void StopBGM()
    {
        audioSources[0].Stop();
    }
}
