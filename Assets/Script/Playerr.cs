using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerr : MonoBehaviour
{
    public float moveSpeed ;

    public float dashBoost;
    private float dashTime;
    public float DashTime;
    private bool once = false;
    public Rigidbody2D rb;
    public Animator animator;
    public Vector3 moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        // di chuyen // Movement
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        transform.position += moveSpeed * Time.deltaTime * moveInput;

        animator.SetFloat("Speed", moveInput.sqrMagnitude);


        if (Input.GetKeyDown(KeyCode.Space) && dashTime <= 0)
        {
            animator.SetBool("Roll", true);
            moveSpeed += dashBoost;
            dashTime = DashTime;
            once = true;
        }

        if (dashTime <= 0 && once == true)
        {
            animator.SetBool("Roll", false);
            moveSpeed -= dashBoost;
            once = false;
        }
        else
        {
            dashTime -= Time.deltaTime;
        }

        // quay dau // Rotate Face
        if (moveInput.x != 0)
            if (moveInput.x > 0)
            transform.localScale = new Vector3(1, 1, 0);
            else
            transform.localScale = new Vector3(-1, 1, 0);
    }
}
