using UnityEngine;
using UnityEngine.SceneManagement; // 씬 관리자 관련 코드
using UnityEngine.UI; // UI 관련 코드

// 필요한 UI에 즉시 접근하고 변경할 수 있도록 허용하는 UI 매니저
public class UIManager : MonoBehaviour
{
    public PlayerController playercontroller;
    public RectTransform joystick;

    // 싱글톤 접근용 프로퍼티
    public static UIManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<UIManager>();
                DontDestroyOnLoad(m_instance);
            }

            return m_instance;
        }
    }

    private static UIManager m_instance; // 싱글톤이 할당될 변수

    public Button BackButton;
    public string touch_sound;

    private void Awake()
    {
        if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void ClickAttack()
    {
        playercontroller.startAttack = true;
    }

    public void ClickBack()
    {
        AudioManager.instance.Play(touch_sound);
        PlayerStat.instance.GetComponent<PlayerController>().gameObject.transform.position = new Vector3(0, 0, 0);
        PlayerStat.instance.GetComponent<PlayerController>().resetMove();
        LobbyManager.instance.gameObject.SetActive(true);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Lobby");
    }
}