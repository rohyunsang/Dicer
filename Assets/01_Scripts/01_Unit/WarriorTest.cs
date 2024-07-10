using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorTest : MonoBehaviour
{
    public float moveSpeed = 5.0f; // �̵� �ӵ�
    private Rigidbody2D rb; // Rigidbody2D ������Ʈ
    private Animator animator; // Animator ������Ʈ

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Rigidbody2D ������Ʈ ��������
        animator = GetComponent<Animator>(); // Animator ������Ʈ ��������
    }

    void Update()
    {
        HandleAttack(); // ���� ó��
    }

    void FixedUpdate()
    {
        Move(); // �� �����Ӹ��� �̵� ó��
    }

    private void Move()
    {
        // WASD �Է¿� ���� ���� ����
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        Vector2 moveVector = new Vector2(moveX, moveY).normalized; // ����ȭ�Ͽ� �밢�� �̵� �ӵ� �����ϰ� ó��

        // Rigidbody�� ����Ͽ� ��ġ ������Ʈ
        rb.velocity = moveVector * moveSpeed;

    }

    private void HandleAttack()
    {
        if (Input.GetKeyDown(KeyCode.J)) // A Ű�� ������ ��
        {
            animator.SetTrigger("Attack"); // Animator���� Attack Ʈ���� Ȱ��ȭ
        }
    }
}
