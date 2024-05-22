using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    private AudioSource audioSource;
    [SerializeField] private AudioClip bgmClip;

    private void Awake()
    {
        if (null == Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        audioSource.clip = bgmClip;
        audioSource.Play();
    }

    public void PlayBGM(AudioClip newClip)
    {
        audioSource.Stop();

        audioSource.clip = newClip;
        audioSource.Play();
    }

    public void PlayEffect(AudioClip newEffect)
    {
        audioSource.PlayOneShot(newEffect);
    }

    public void StopBGM()
    {
        audioSource.Stop();
    }
}
