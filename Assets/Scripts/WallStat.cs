using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WallStat : MonoBehaviour
{
    public float hp;
    public float currentHp;
    public float RecoverHp;
    public int atk;
    public int def;
    public int exp;

    public float time;
    public float currentTime;

    public Slider hpSlider;
    public Text hpTxt;
    public Slider timeSlider;
    public Text timeTxt;

    public GameObject wallEffect;

    private Camera theCamera;
    private float halfHeight;
    float wallHeightScale;

    private SpriteRenderer renderer;
    private BoxCollider2D boxcollider;

    void Start()
    {
        currentHp = hp;
        currentTime = time;

        theCamera = FindObjectOfType<Camera>();
        halfHeight = theCamera.orthographicSize;
        double a = halfHeight + 4.5;
        wallHeightScale = (float)a;

        renderer = GetComponent<SpriteRenderer>();
        boxcollider = GetComponent<BoxCollider2D>();
    }

    public int Hit(int _playerAtk)
    {
        float saveX = renderer.size.x;
        //float savePositionX = saveScaleX * 32 - 56;
        int playerAtk = _playerAtk;
        int dmg;
        if (def >= playerAtk)
            dmg = 1;
        else
            dmg = playerAtk - def;

        if (currentHp - dmg <= 0)
        {
            GameOver();
        }
        else
        {
            currentHp -= dmg;
            
            //if (currentHp <= 0)
            //{
            //    Destroy(this.gameObject);
            //    PlayerStat.instance.currentExp += exp;
            //}
        }

        CalWallScaleAndHp();

        float savePositionX = renderer.size.x - 56;

        float gap = saveX - renderer.size.x;
        GameObject clone = Instantiate(wallEffect);
        clone.GetComponent<SpriteRenderer>().sprite = renderer.sprite;
        clone.GetComponent<SpriteRenderer>().size = new Vector2(gap, wallHeightScale);
        clone.transform.position = new Vector3(savePositionX, 0, 10f);

        Color color = renderer.color;
        color.r *= (float)0.8;
        color.g *= (float)0.8;
        color.b *= (float)0.8;
        clone.GetComponent<SpriteRenderer>().color = color;

        return dmg;
    }

    private void FixedUpdate()
    {
        currentTime -= Time.deltaTime;

        if (currentTime <= 0)
        {
            hpSlider.value = 0;
            timeTxt.text = "0";
            GameOver();
        }
        else
        {
            float tscale = currentTime / time;
            timeSlider.value = tscale;
            timeTxt.text = currentTime.ToString("N1");
        }

        if (currentHp + Time.deltaTime * RecoverHp < hp)
        {
            currentHp += Time.deltaTime * RecoverHp;
        }
        else
        {
            currentHp = hp;
        }

        CalWallScaleAndHp();
        hpTxt.text = currentHp.ToString("N0") + " HP";
    }

    void CalWallScaleAndHp()
    {
        /*
        float hscale = currentHp / hp;
        transform.localScale = new Vector3(hscale * 100, 40, 1);
        if (currentHp <= hp)
        {
            hpSlider.value = hscale;
        }
        else
        {
            hpSlider.value = 1;
        }*/
        
        float hscale = currentHp / hp;
        renderer.size = new Vector2(50 * hscale, 20);
        boxcollider.size = new Vector2(50 * hscale, 20);
        boxcollider.offset = new Vector2(50 * hscale / 2, 20 / 2);
        if (currentHp < hp)
        {
            hpSlider.value = hscale;
        }
        else
        {
            hpSlider.value = 1;
        }
    }
    
    public void GameOver()
    {
        UIManager.instance.DisableBackButton();
        UIManager.instance.resetJoystick();
        PlayerStat.instance.GetComponent<PlayerController>().gameObject.transform.position = new Vector3(0, 0, 0);
        PlayerStat.instance.GetComponent<PlayerController>().resetMove();
        LobbyManager.instance.gameObject.SetActive(true);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Lobby");
    }
}