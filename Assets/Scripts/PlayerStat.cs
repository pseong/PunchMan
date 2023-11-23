using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    public static PlayerStat instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<PlayerStat>();
                DontDestroyOnLoad(m_instance);
            }
            return m_instance;
        }
    }

    private static PlayerStat m_instance;

    private void Awake()
    {
        if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public int gold;

    public int atk;
    public int cric;
    public int crid;
    public int cdr;
    public float speed;

    public int plus_atk;
    public int plus_cric;
    public int plus_crid;
    public int plus_cdr;
    public float plus_speed;

    public int[] MEXP = new int[] { 0, 100, 150, 200, 250, 300, 350, 400, 450, 500, 550, 600, 650, 700, 750, 800, 850, 900, 950, 1000 };
    public int lv;

    public int exp;
    public void AddExp(int value)
    {
        exp += value;
        if (exp > MEXP[lv])
        {
            if (lv < MEXP.Length - 1)
            {
                exp = exp - MEXP[lv];
                lv++;
            }
            else
            {
                exp = MEXP[lv];
            }
        }
    }

    public SpriteRenderer hat;
    public SpriteRenderer head;
    public SpriteRenderer clothes;
    public SpriteRenderer glove_r;
    public SpriteRenderer glove_l;
    public SpriteRenderer shoe_r;
    public SpriteRenderer shoe_l;

    IEnumerator HitCoroutine()
    {
        Color color = GetComponent<SpriteRenderer>().color;
        color.a = 0;
        GetComponent<SpriteRenderer>().color = color;
        yield return new WaitForSeconds(0.1f);
        color.a = 1f;
        GetComponent<SpriteRenderer>().color = color;
        yield return new WaitForSeconds(0.1f);
        color.a = 0f;
        GetComponent<SpriteRenderer>().color = color;
        yield return new WaitForSeconds(0.1f);
        color.a = 1f;
        GetComponent<SpriteRenderer>().color = color;
        yield return new WaitForSeconds(0.1f);
        color.a = 0f;
        GetComponent<SpriteRenderer>().color = color;
        yield return new WaitForSeconds(0.1f);
        color.a = 1f;
        GetComponent<SpriteRenderer>().color = color;
    }
}