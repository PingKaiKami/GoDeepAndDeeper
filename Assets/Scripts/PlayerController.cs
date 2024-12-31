using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

// 修復
// 修復體力歸零 心率不增加
// 心率為0會消耗氧
// 單按shift消耗氧氣

public class PlayerController : MonoBehaviour
{
    public float acceleration = 10f;  // 加速度
    public float maxSpeed = 10f;     // 最大速度
    public float decelerationDistance = 1f;  // 開始減速的距離
    public float sprintMultiplier = 3f;   // 衝刺時的速度
    public float suckForce = 10f;
    public float risingSpeed = 2f;
    public float seaSurface = 0f;
    public bool isSuck;
    public GameObject box;
    public Volume volume;
    [SerializeField] private float Force = 2;
    protected private bool isdied = false;
    private bool isReborn = false;
    private bool isRed = false;
    protected private Rigidbody2D rb;
    protected private Animator animator;
    public AudioManager audioManager;

    private bool isSwimming = false;
    protected private void Move()
    {
        // 檢測左鍵是否按下
        if (Input.GetMouseButton(0))
        {
            // 獲取鼠標的位置並將其轉換為世界座標
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;  // 設定 z 軸，確保角色只在 2D 平面移動

            // 計算角色與鼠標之間的距離
            Vector2 direction = (mousePosition - transform.position).normalized;
            if (direction.x < 0)
            {
                transform.localScale = new Vector3(-0.8f, 0.8f, 1);
            }
            else
            {
                transform.localScale = new Vector3(0.8f, 0.8f, 1);
            }
            float distance = Vector2.Distance(mousePosition, transform.position);

            // 如果距離足夠近，減速；否則加速
            float targetSpeed = (distance < decelerationDistance) ? Mathf.Lerp(0, maxSpeed, distance / decelerationDistance) : maxSpeed;
            animator.SetFloat("speed", targetSpeed);

            // 按下shift 使速度3倍 (體力充足) 同slidercontroller的增減規則
            if (Input.GetKey(KeyCode.LeftShift) && ValueController.Instance.GetEnergy() > 0.01f)
            {
                targetSpeed *= sprintMultiplier;

                AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
                if (stateInfo.IsName("Player_idle") || stateInfo.IsName("Player_swimming"))
                    animator.SetTrigger("rush");
            }
            else
            {
                animator.SetTrigger("stopRush");
            }
            // 計算目標速度並應用加速度
            Vector2 targetVelocity = direction * targetSpeed;
            rb.velocity = Vector2.MoveTowards(rb.velocity, targetVelocity, acceleration * Time.deltaTime);
            
            if (!isSwimming)
            {
                isSwimming = true;
                audioManager.Play(5, "sdSwim", true);
            }
        }
        else
        {
            animator.SetFloat("speed", 0);
            if(isSwimming)
            {
                isSwimming = false;
                audioManager.Stop(5);
            }
        }

    }
    protected private void Suck()
    {
        Vector3 dir = box.transform.position - transform.position;
        Force = Mathf.Lerp(Force, suckForce, Time.deltaTime * 0.1f);
        rb.AddForce(dir * Force, ForceMode2D.Force);
    }
    //also check oxygen amount
    public bool isAlive()
    {
        if (ValueController.Instance != null)
        {
            if (ValueController.Instance.GetOxygen() < 0.4f)
            {
                if (!isRed)
                {
                    StartCoroutine(ChangeScreenEdgeColor(0));
                    isRed = true;
                }
            }
            else
            {
                if (isRed)
                {
                    StartCoroutine(ChangeScreenEdgeColor(1));
                    isRed = false;
                }
            }
            return ValueController.Instance.GetOxygen() > 0 && !isdied;
        }
        return true;
    }
    protected private void Died()
    {
        if (!isdied)
        {
            GetComponent<Collider2D>().enabled = false;
            animator.SetTrigger("died");
            isdied = true;
            StartCoroutine(ChangeScreenColor(0));
        }
        if (!isReborn)
        {
            rb.MovePosition(transform.position += new Vector3(0, 2 * Time.deltaTime, 0));
            if (transform.position.y >= seaSurface && !isReborn)
            {
                isReborn = true;
                StartCoroutine(Reborn());
                StartCoroutine(ChangeScreenColor(1));
            }
        }
    }
    private IEnumerator ChangeScreenColor(int mode)
    {
        // black-white
        if (mode == 0)
        {
            if (volume.profile.TryGet(out ColorAdjustments ca))
            {
                while (ca.saturation.value >= -99 && !isReborn)
                {
                    ca.saturation.value -= 10 * Time.deltaTime;
                    yield return null;
                }
            }
        }
        // from black-white back to original
        else if (mode == 1)
        {
            if (volume.profile.TryGet(out ColorAdjustments ca))
            {
                while (ca.saturation.value <= 0 && isReborn)
                {
                    ca.saturation.value += 10 * Time.deltaTime;
                    yield return null;
                }
            }
        }
    }
    private IEnumerator ChangeScreenEdgeColor(int mode)
    {
        // red
        if (mode == 0)
        {
            if (volume.profile.TryGet(out Vignette vig))
            {
                while (vig.intensity.value < 0.5)
                {
                    vig.intensity.value += 0.1f * Time.deltaTime;
                    yield return null;
                }
            }
        }
        // from red back to original
        else if (mode == 1)
        {
            if (volume.profile.TryGet(out Vignette vig))
            {
                while (vig.intensity.value > 0)
                {
                    vig.intensity.value -= 0.1f * Time.deltaTime;
                    yield return null;
                }
            }
        }
    }
    private IEnumerator Reborn()
    {
        rb.bodyType = RigidbodyType2D.Kinematic;
        ValueController.Instance.IncreaseMaxOxygen(1);
        while (ValueController.Instance.GetOxygen() < 0.99f)
        {
            ValueController.Instance.IncreaseOxygen(0.001f);
            ValueController.Instance.IncreaseEnergy(0.001f);
            ValueController.Instance.DecreaseHeartRate(0.001f);
            yield return null;
        }
        rb.bodyType = RigidbodyType2D.Dynamic;
        animator.SetTrigger("alive");
        isdied = false;
        isReborn = false;
        GetComponent<Collider2D>().enabled = true;
    }
    public void Disappear()
    {
        gameObject.SetActive(false);
    }
}
