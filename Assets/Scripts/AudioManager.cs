using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name; // 사운드의 이름.

    public AudioClip clip; // 사운드 파일
    private AudioSource source; // 사운드 플레이어

    public float Volumn;
    public bool loop;

    public void SetSource(AudioSource _source)
    {
        source = _source;
        source.clip = clip;
        source.loop = loop;
        source.volume = Volumn;
    }

    public void SetVolumn()
    {
        source.volume = Volumn;
    }

    public void Play()
    {
        source.Play();
    }

    public void Stop()
    {
        source.Stop();
    }

    public void SetLoop()
    {
        source.loop = true;
    }

    public void SetLoopCancel()
    {
        source.loop = false;
    }
}


public class AudioManager : MonoBehaviour
{
    public static AudioManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<AudioManager>();
                DontDestroyOnLoad(m_instance);
            }
            return m_instance;
        }
    }

    private static AudioManager m_instance;

    private void Awake()
    {
        if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    [SerializeField]
    public Sound[] sounds;

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            GameObject soundObject = new GameObject("사운드 파일 이름 : " + i + " = " + sounds[i].name);
            sounds[i].SetSource(soundObject.AddComponent<AudioSource>());
            soundObject.transform.SetParent(this.transform);


            // ->>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>궁금증 : 오디오 소스(카세트)를 꼭 오브젝트에 붙여줘야 소리가 재생 되는가?
        }
    }

    public void Play(string _name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (_name == sounds[i].name)
            {
                sounds[i].Play();
                return;
            }
        }
    }

    public void Stop(string _name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (_name == sounds[i].name)
            {
                sounds[i].Stop();
                return;
            }
        }
    }

    public void SetLoop(string _name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (_name == sounds[i].name)
            {
                sounds[i].SetLoop();
                return;
            }
        }
    }

    public void SetLoopCancel(string _name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (_name == sounds[i].name)
            {
                sounds[i].SetLoopCancel();
                return;
            }
        }
    }

    public void SetVolumn(string _name, float _Volumn)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (_name == sounds[i].name)
            {
                sounds[i].Volumn = _Volumn;
                sounds[i].SetVolumn();
                return;
            }
        }
    }
}
