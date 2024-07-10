using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class WarriorTest : MonoBehaviour
{
    public float moveSpeed = 5.0f; // 이동 속도
    private Rigidbody2D rb; // Rigidbody2D 컴포넌트
    private Animator animator; // Animator 컴포넌트

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Rigidbody2D 컴포넌트 가져오기
        animator = GetComponent<Animator>(); // Animator 컴포넌트 가져오기
    }

    void Update()
    {
        HandleAttack(); // 공격 처리
    }

    void FixedUpdate()
    {
        Move(); // 매 프레임마다 이동 처리
    }

    private void Move()
    {

        // WASD 입력에 따라 벡터 생성
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        Vector2 moveVector = new Vector2(moveX, moveY).normalized; // 정규화하여 대각선 이동 속도 일정하게 처리

        // Rigidbody를 사용하여 위치 업데이트
        rb.velocity = moveVector * moveSpeed;

    }

    private void HandleAttack()
    {
        if (Input.GetKeyDown(KeyCode.J)) // A 키를 눌렀을 때
        {
            animator.SetTrigger("Attack"); // Animator에서 Attack 트리거 활성화
        }
    }
}
