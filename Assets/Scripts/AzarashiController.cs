using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AzarashiController : MonoBehaviour
{
    Rigidbody2D rb2d;
    Animator animator;
    float angle;
    bool isDead;

    public float maxHeight;
    public float flapVelocity;
    public float relativeVelocityX;
    public GameObject sprite;

    public bool IsDead()
    {
        return isDead;
    }

    private void Awake()
    {
        //自分自身の情報のみ参照できる。startは違う。順番としては次の通り。Awake ⇒ Start ⇒ Update
        rb2d = GetComponent<Rigidbody2D>();
        animator = sprite.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && transform.position.y < maxHeight)
        {
            Flap();
        }

        ApplyAngle();

        animator.SetBool("flap", angle >= 0.0f && !isDead);
    }

    public void Flap()
    {
        if (isDead)
        {
            return;
        }
        rb2d.velocity = new Vector2(0.0f, flapVelocity);
    }

    void ApplyAngle()
    {
        float targetAngle;
        if (isDead)
        {
            targetAngle = 180.0f;
        }
        else
        {
            //逆三角関数。アークタンジェント。通常は角度から２辺の比率だが、比率から角度を求める。
            //Rad2Deg　ラジアンからデグリーに変換。
            targetAngle = Mathf.Atan2(rb2d.velocity.y, relativeVelocityX) * Mathf.Rad2Deg;

        }
        //線形補間。第１引数と第２引数の差を第３引数の割合で返す
        angle = Mathf.Lerp(angle, targetAngle, Time.deltaTime * 10.0f);

        sprite.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, angle);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead)
        {
            return;
        }
        isDead = true;
    }
}
