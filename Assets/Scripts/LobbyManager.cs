using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour
{
    private string gameVersion = "1"; // 게임 버전

    public static LobbyManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<LobbyManager>();
                DontDestroyOnLoad(m_instance);
            }
            return m_instance;
        }
    }

    public GameObject effect;

    private static LobbyManager m_instance;

    public Canvas storeCanvas;
    private Canvas inventoryCanvas;
    public StoreManager storemanager;

    public string touch_sound;

    public Text[] text;
    private const int ATK = 0, CRIC = 1, CRID = 2, CDR = 3, SPEED = 4;
    PlayerStat playerstat;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        if (instance != this)
        {
            Destroy(gameObject);
        }

        playerstat = FindObjectOfType<PlayerStat>();
        player = FindObjectOfType<PlayerController>();
    }
    private PlayerController player;
    void Start() {
        Inventory.instance.gameObject.SetActive(false);
        ResetStat();
    }

    public void ResetStat()
    {
        text[ATK].text = (playerstat.atk + playerstat.plus_atk).ToString(); ;// + "(" + playerstat.atk + "+" + playerstat.plus_atk + ")";
        text[CRIC].text = (playerstat.cric + playerstat.plus_cric).ToString();// + "(" + playerstat.cric + "+" + playerstat.plus_cric + ")";
        text[CRID].text = (playerstat.crid + playerstat.plus_crid).ToString();// + "(" + playerstat.crid + "+" + playerstat.plus_crid + ")";
        text[CDR].text = (playerstat.cdr + playerstat.plus_cdr).ToString();// + "(" + playerstat.cdr + "+" + playerstat.cdr + ")";
        text[SPEED].text = (playerstat.speed + playerstat.plus_speed).ToString();// + "(" + playerstat.speed + "+" + playerstat.plus_speed + ")";
    }

    public void ClickGameStart()
    {
        AudioManager.instance.Play(touch_sound);
        player.gameObject.transform.position = new Vector3(0, 0, 0);
        player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        UIManager.instance.EnableBackButton();
        UIManager.instance.resetJoystick();
        PlayerStat.instance.GetComponent<PlayerController>().resetMove();
        gameObject.SetActive(false);
        UnityEngine.SceneManagement.SceneManager.LoadScene("InGame");
    }

    public void ClickStore()
    {
        AudioManager.instance.Play(touch_sound);
        storeCanvas.gameObject.SetActive(true);
    }

    public void ClickInventory()
    {
        AudioManager.instance.Play(touch_sound);
        Inventory.instance.gameObject.SetActive(true);
        Inventory.instance.gameObject.GetComponent<Inventory>().OpenIventory();
    }
}
