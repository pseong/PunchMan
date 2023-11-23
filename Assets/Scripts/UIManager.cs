using UnityEngine;
using UnityEngine.SceneManagement; // 씬 관리자 관련 코드
using UnityEngine.UI; // UI 관련 코드

// 필요한 UI에 즉시 접근하고 변경할 수 있도록 허용하는 UI 매니저
public class UIManager : MonoBehaviour
{
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

    private static UIManager m_instance;

    public PlayerController playerController;
    public RectTransform joystick;

    public Slider expSlider;
    public Text expTxt;

    private void Awake()
    {
        if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        float tscale = (float)PlayerStat.instance.exp / PlayerStat.instance.MEXP[PlayerStat.instance.lv];
        expSlider.value = tscale;
        expTxt.text = string.Format("{0:N2}%", tscale * 100);
    }

    public void ClickAttack()
    {
        playerController.startAttack = true;
    }
}