using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{

    public static BGMManager instance
    {
        get
        {
            // 만약 싱글톤 변수에 아직 오브젝트가 할당되지 않았다면
            if (m_instance == null)
            {
                // 씬에서 GameManager 오브젝트를 찾아 할당
                m_instance = FindObjectOfType<BGMManager>();
                DontDestroyOnLoad(m_instance);
            }

            // 싱글톤 오브젝트를 반환
            return m_instance;
        }
    }

    private static BGMManager m_instance; // 싱글톤이 할당될 static 변수

    public AudioClip[] clips; // 배경음악들

    private AudioSource source;

    private WaitForSeconds waitTime = new WaitForSeconds(0.01f);

    private void Awake()
    {
        if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void Play(int _playMusicTrack)
    {
        source.volume = 1f;
        source.clip = clips[_playMusicTrack];
        source.Play();
    }

    public void SetVolumn(float _volumn)
    {
        source.volume = _volumn;
    }

    public void Pause()
    {
        source.Pause();
    }

    public void Unpause()
    {
        source.UnPause();
    }

    public void Stop()
    {
        source.Stop();
    }

    public void FadeOutMusic()
    {
        StopAllCoroutines();
        StartCoroutine(FadeOutMusicCoroutine());
    }

    IEnumerator FadeOutMusicCoroutine()
    {
        for (float i = 1.0f; i >= 0f; i -= 0.01f)
        {
            source.volume = i;
            yield return waitTime;
        }
    }

    public void FadeInMusic()
    {
        StopAllCoroutines();
        StartCoroutine(FadeInMusicCoroutine());
    }
    IEnumerator FadeInMusicCoroutine()
    {
        for (float i = 0f; i <= 1f; i += 0.01f)
        {
            source.volume = i;
            yield return waitTime;
        }
    }
}
