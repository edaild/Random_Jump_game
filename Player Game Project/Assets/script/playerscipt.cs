
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class playerscipt : MonoBehaviour
{
    public float speed;
    float hAxis;
    float vAxis;
    bool WDown;
    bool jump;

    bool isJump;

    Vector3 moveVec;

    Rigidbody rb; // �ɸ��͸� �����̱� ���� ����
    Animator anim;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();

    }
    void Update()
    {
        GetInput();
        Move();
        Turn();
        Jump();
    }


    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        WDown = Input.GetButton("wolk");
        jump = Input.GetButtonDown("Jump");
    }

    void Move()
    {
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        transform.position += moveVec * speed * (WDown ? 0.3f : 1f) * Time.deltaTime;

        anim.SetBool("isRun", moveVec != Vector3.zero);
        anim.SetBool("isWolk", WDown);
    }

    void Turn()
    {
        transform.LookAt(transform.position + moveVec);
    }

    void Jump() // ����
    {
        if (jump && !isJump) // ! ������ bool ���� ����
        {
            rb.AddForce(Vector3.up * 5, ForceMode.Impulse);
                anim.SetBool("triggerJump", true);
                 isJump = true;
        }
    }
    private void OnCollisionEnter(Collision collision) 
    {
        if (collision.gameObject.tag == "left Game Cube") {
           anim.SetBool("triggerJump", false);
           isJump = false;
        }
     
        if (collision.gameObject.tag == "right Game Cube")
        {
            anim.SetBool("triggerJump", false);
            isJump = false;
        }
        if (collision.gameObject.tag == "Finish Box")
        {
           gameObject.SetActive(false);
        }
    }

}

