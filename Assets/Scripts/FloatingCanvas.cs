using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingCanvas : MonoBehaviour
{
    public static FloatingCanvas instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<FloatingCanvas>();
                DontDestroyOnLoad(m_instance);
            }
            return m_instance;
        }
    }

    private void Awake()
    {
        if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private static FloatingCanvas m_instance; // 싱글톤이 할당될 static 변수
}
