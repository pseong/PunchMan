using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{/*
    public static GameManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<GameManager>();
                DontDestroyOnLoad(m_instance);
            }
            return m_instance;
        }
    }*/

    private void Awake()
    {/*
        if (instance != this)
        {
            Destroy(gameObject);
        }*/
        Application.targetFrameRate = 60;
    }

   /* private static GameManager m_instance;*/
}
