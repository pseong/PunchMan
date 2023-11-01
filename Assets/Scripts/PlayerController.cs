using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //public AudioClip deathClip; // 사망시 재생할 오디오 클립
    public JoyStick joystick;

    public bool attacking = false;
    public float attackDelay;  //어택 에니메이션 시간이랑 같아야 할 것 같다.
    public float comboDelay;

    //private float currentAttackDelay;
    public bool startAttack = false;
    public bool attackDamageFinished = true;

    public float move { get; private set; } // 감지된 움직임 입력값

    private CameraManager camera;
    private Rigidbody2D playerRigidbody; // 사용할 리지드바디 컴포넌트
    private SpriteRenderer playerRenderer;
    private Animator animator;
    private PlayerStat playerStat;

    public bool playing = false;

    void Start()
    {
        // 사용할 컴포넌트들의 참조를 가져오기
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        camera = FindObjectOfType<CameraManager>();
        playerStat = FindObjectOfType<PlayerStat>();

        animator.SetFloat("Direction", -1f);
    }

    void Update()
    {
        if (!attacking && startAttack)
        {
            animator.SetFloat("Direction", -1f);
            attacking = true;
            attackDamageFinished = false;

            if (animator.GetFloat("Combo") == 0f)
            {
                animator.SetFloat("Combo", 1f);
                animator.SetBool("Attacking", true);

                StartCoroutine(IEattacking());
                StartCoroutine(IEshaking());
                StartCoroutine(IEcombo());
            }
            else if (animator.GetFloat("Combo") == 1f)
            {
                StopAllCoroutines();
                animator.SetFloat("Combo", 2f);
                animator.SetBool("Attacking", true);
                StartCoroutine(IEshaking());
                StartCoroutine(IEattacking());
                StartCoroutine(IEcombo());
            }
            else if (animator.GetFloat("Combo") == 2f)
            {
                StopAllCoroutines();
                animator.SetFloat("Combo", 3f);
                animator.SetBool("Attacking", true);
                camera.Shake_();
                StartCoroutine(IEattacking());
                StartCoroutine(IEcomboLast());
            }
            startAttack = false;
        }

        if (joystick.GetHorizontalValue() > 0)
        {
            move = 1;
        }
        else if (joystick.GetHorizontalValue() < 0)
        {
            move = -1;
        }
        else if (joystick.GetHorizontalValue() == 0)
        {
            move = 0;
        }

        move = Input.GetAxisRaw("Horizontal");

        Move();
    }

    IEnumerator IEshaking()
    {
        yield return new WaitForSeconds(0.1f);
        camera.Shake_();
    }
    IEnumerator IEcomboLast()
    {
        yield return new WaitForSeconds(0.29f);
        animator.SetFloat("Combo", 0f);
    }

    IEnumerator IEcombo()
    {
        yield return new WaitForSeconds(attackDelay + comboDelay);
        animator.SetFloat("Combo", 0f);
    }

    IEnumerator IEattacking()
    {
        yield return new WaitForSeconds(attackDelay);
        animator.SetBool("Attacking", false);
        attacking = false;
        attackDamageFinished = true;
    }

    public void resetMove()
    {
        move = 0;
    }

    private void Move()
    {
        transform.position += Vector3.right * move * (playerStat.speed + playerStat.plus_speed) / 10 * Time.deltaTime;

        if (move > 0)
        {
            animator.SetFloat("Direction", 1f);
            animator.SetBool("isMoving", true);
        }
        else if (move < 0)
        {
            animator.SetFloat("Direction", -1f);
            animator.SetBool("isMoving", true);
        }
        else if (move == 0)
        {
            animator.SetBool("isMoving", false);
        }
    }
}
