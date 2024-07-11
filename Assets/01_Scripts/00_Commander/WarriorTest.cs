using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Addons.Physics;

public class WarriorTest : NetworkBehaviour
{
    public float moveSpeed = 5.0f; // �̵� �ӵ�
    private NetworkRigidbody2D _rigid;  // Fusion.Addons.Physics
    private Animator _animator; // Animator ������Ʈ

    [Networked]
    public int PlayerID { get; private set; }

    void Start()
    {
        _rigid = GetComponent<NetworkRigidbody2D>(); // Rigidbody2D ������Ʈ ��������
        _animator = GetComponent<Animator>(); // Animator ������Ʈ ��������
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
        // _rigid.velocity = moveVector * moveSpeed;

    }

    private void HandleAttack()
    {
        if (Input.GetKeyDown(KeyCode.J)) // A Ű�� ������ ��
        {
            _animator.SetTrigger("Attack"); // Animator���� Attack Ʈ���� Ȱ��ȭ
        }
    }
}
