using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5f;     // Velocidade de movimentação
    public float jumpForce = 10f;    // Força do pulo
    public LayerMask groundLayer;    // Camada do chão
    public Transform groundCheck;    // Ponto de verificação do chão
    public float groundCheckRadius = 0.2f; // Tamanho da área de verificação do chão

    private Rigidbody2D rb;
    private bool isGrounded;
    private float screenWidth;
    private bool facingRight = true;  // Verifica se o personagem está virado para a direita

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        screenWidth = Screen.width;  // Obtém a largura da tela
    }

    void Update()
    {
        CheckIfGrounded();
        Jump();
        Move();
    }

    void Move()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Se tocar no lado esquerdo da tela, move para a esquerda
            if (touch.position.x < screenWidth / 2)
            {
                rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
                if (facingRight) // Se estava virado para a direita, inverte
                {
                    Flip();
                }
            }
            // Se tocar no lado direito da tela, move para a direita
            else if (touch.position.x > screenWidth / 2)
            {
                rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
                if (!facingRight) // Se estava virado para a esquerda, inverte
                {
                    Flip();
                }
            }
        }
    }

    void Jump()
    {
        if (isGrounded && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);  // Aplica a força do pulo
        }
    }

    void CheckIfGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    // Função de Flip para inverter o personagem
    void Flip()
    {
        facingRight = !facingRight;  // Inverte o estado do personagem

        // Inverte a escala no eixo X para virar o sprite
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;  // Inverte o valor de X
        transform.localScale = scaler;
    }

    

}