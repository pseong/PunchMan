using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemResourceManager : MonoBehaviour
{
    public static ItemResourceManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<ItemResourceManager>();
                DontDestroyOnLoad(m_instance);
            }
            return m_instance;
        }
    }

    private static ItemResourceManager m_instance;

    public Sprite[] sprites = new Sprite[101010];

    void Awake()
    {
        if (instance != this)
        {
            Destroy(gameObject);
        }

        for (int i = 0; i < 101010; i++)
        {
            sprites[i] = Resources.Load<Sprite>("ItemIcon/" + i.ToString());
        }
    }
}
