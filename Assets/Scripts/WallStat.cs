using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WallStat : MonoBehaviour
{
    public float hp;
    public float currentHp;
    public float recoverHp;
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
    private float wallHeight;

    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxcollider;

    void Start()
    {
        currentHp = hp;
        currentTime = time;

        theCamera = FindObjectOfType<Camera>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        boxcollider = GetComponent<BoxCollider2D>();

        wallHeight = spriteRenderer.size.y;
    }

    public int Hit(int playerAtk)
    {
        float initialSizeX = spriteRenderer.size.x;

        int dmg = def >= playerAtk ? 1 : playerAtk - def;
        currentHp -= dmg;

        if (currentHp <= 0)
        {
            GameOver();
            return dmg;
        }

        CalWallScaleAndHp();

        float gap = initialSizeX - spriteRenderer.size.x;
        CreateWallEffect(gap, spriteRenderer.size.x - 56);

        return dmg;
    }

    private void CreateWallEffect(float gap, float positionX)
    {
        GameObject clone = Instantiate(wallEffect);
        SpriteRenderer cloneRenderer = clone.GetComponent<SpriteRenderer>();

        cloneRenderer.sprite = spriteRenderer.sprite;
        cloneRenderer.size = new Vector2(gap, wallHeight);
        clone.transform.position = new Vector3(positionX, 0, 10f);
    
        Color color = cloneRenderer.color;
        color *= 0.8f;
        cloneRenderer.color = color;
    }

    private void FixedUpdate()
    {
        currentTime -= Time.deltaTime;

        if (currentTime <= 0)
        {
            hpSlider.value = 0;
            timeTxt.text = "0";
            GameOver();
            return;
        }
        else
        {
            float tscale = currentTime / time;
            timeSlider.value = tscale;
            timeTxt.text = currentTime.ToString("N1");
        }

        if (currentHp + Time.deltaTime * recoverHp < hp)
        {
            currentHp += Time.deltaTime * recoverHp;
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
        float hscale = currentHp / hp;

        UpdateWallScale(hscale);
        UpdateHpSlider(hscale);
    }

    private void UpdateWallScale(float scale)
    {
        Vector2 newSize = new Vector2(50 * scale, 20);
        spriteRenderer.size = newSize;
        boxcollider.size = newSize;
        boxcollider.offset = newSize / 2;
    }

    private void UpdateHpSlider(float scale)
    {
        hpSlider.value = (currentHp < hp) ? scale : 1;
    }
    
    public void GameOver()
    {
        UIManager.instance.DisableBackButton();
        UIManager.instance.resetJoystick();

        PlayerController playerController = PlayerStat.instance.GetComponent<PlayerController>();
        playerController.gameObject.transform.position = Vector3.zero;
        playerController.resetMove();

        LobbyManager.instance.gameObject.SetActive(true);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Lobby");
    }
}