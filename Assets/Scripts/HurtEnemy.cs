using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtEnemy : MonoBehaviour
{
    public GameObject prefabs_floating_text;
    public GameObject parent;
    public ParticleSystem ps;
    public float position;

    public string atk_sound;

    private PlayerStat thePlayerStat;
    private PlayerController playerController;
    void Start()
    {
        thePlayerStat = GetComponentInParent<PlayerStat>();////->>>get컴포넌트로 바꾸수닛나?
        playerController = GetComponentInParent<PlayerController>();
        position = 1f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall") && !playerController.attackDamageFinished)
        {
            AudioManager.instance.Play(atk_sound);
            playerController.attackDamageFinished = true;

            int mathAtk = (thePlayerStat.atk + thePlayerStat.plus_atk) * 25;
            int range = (int)(mathAtk * 0.1);

            int damage = Random.Range(mathAtk - range, mathAtk + range);

            bool cri = false;
            if (Random.Range(0f, 100f) <= (thePlayerStat.cric + thePlayerStat.plus_cric))
            {
                cri = true;
                damage = damage * (thePlayerStat.crid + thePlayerStat.plus_crid) / 100;
            }

            //데미지를 입히고 입힌 데미지값은 가져온다.
            int dmg = collision.gameObject.GetComponent<WallStat>().Hit(damage);

            if (position == 1f)
            {
                position = 0.5f;
            }
            else
            {
                position = 1f;
            }
            //collision.GetComponent<R>
            Vector3 vector = transform.position;
            vector.y = 5f;
            vector.x -= position;

            GameObject clone = Instantiate(prefabs_floating_text, vector, Quaternion.Euler(Vector3.zero));
            clone.GetComponent<FloatingText>().text.text = dmg.ToString();
            clone.GetComponent<FloatingText>().text.fontSize = 50;
            clone.transform.SetParent(parent.transform);


            if (cri == false)
            {
                clone.GetComponent<FloatingText>().text.color = Color.black;
            }
            else
            {
                clone.GetComponent<FloatingText>().text.color = Color.red;
            }

            if (ps == null) {
                ps = FindObjectOfType<ParticleSystem>();
            }
            ps.transform.position = new Vector3(transform.position.x, ps.transform.position.y, ps.transform.position.z);
            ps.Emit(5);
        }
    }
}
