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

    //public string dmgSound;

    public SpriteRenderer hat;
    public SpriteRenderer head;
    public SpriteRenderer clothes;
    public SpriteRenderer glove_r;
    public SpriteRenderer glove_l;
    public SpriteRenderer shoe_r;
    public SpriteRenderer shoe_l;

    //public GameObject prefabs_Floating_text;
    //public GameObject parent;

    void Start ()
    {

    }

    /*
    public void Hit(int _enemyAtk)
    {
        int dmg;

        if (def >= _enemyAtk)
            dmg = 1;
        else
            dmg = _enemyAtk - def;

        currentHp -= dmg;

        if (currentHp <= 0)
        {
            //----------------------------------------------
        }

        //AudioManager.instance.Play(dmgSound);

        Vector3 vector = transform.position;
        vector.y += 60;

        //GameObject clone = Instantiate(prefabs_Floating_text, vector, Quaternion.Euler(Vector3.zero);
        //clone.GetComponent<FloatingText>().text.text = dmg.ToString();
        //clone.GetComponent<FloatingText>().text.color = Color.red;
        //clone.GetComponent<FloatingText>().text.fontSize = 25;
        //clone.transform.SetParent(parent.transform);
        //StopAllCoroutines();
        //StartCouroutine(HitCoroutine()):
    }*/

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
