using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; // Biblioteca para RPC
using UnityEngine;
using Photon.Pun;

/// <summary>
/// Controla o movimento e a sincroniza��o do carro do jogador em 2D.
/// </summary>
public class CarController : MonoBehaviourPun, ICar
{
    // Velocidade de movimento do carro
    public float speed = 10f;
    // Velocidade de rota��o do carro
    public float turnSpeed = 100f;

    // Refer�ncia ao Rigidbody do carro
    private Rigidbody2D rb;

    // Vari�veis para armazenar entradas
    private float moveInput;
    private float turnInput;

    // Vari�veis para sincroniza��o
    private Vector2 networkPosition;
    private float networkRotation;

    // Inicializa o componente Rigidbody2D
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (photonView.IsMine)
        {
            // Configure a c�mera para seguir este carro, se necess�rio
        }
        else
        {
            // Desativa o controle local para carros que n�o s�o do jogador
            enabled = false;
        }
    }

    // Captura a entrada do jogador
    void Update()
    {
        if (photonView.IsMine)
        {
            moveInput = Input.GetAxis("Vertical"); // Input de acelera��o para frente/tr�s
            turnInput = Input.GetAxis("Horizontal"); // Input para virar � esquerda/direita
        }
        else
        {
            // Atualiza a posi��o e rota��o do carro de acordo com a rede
            transform.position = Vector2.Lerp(transform.position, networkPosition, Time.deltaTime * 10);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, networkRotation), Time.deltaTime * 10);
        }
    }

    // Movimenta e rotaciona o carro com base na entrada do jogador
    void FixedUpdate()
    {
        if (photonView.IsMine)
        {
            // Aplica a rota��o primeiro (carro se orienta antes de se mover)
            float rotationAmount = turnInput * turnSpeed * Time.fixedDeltaTime;
            rb.MoveRotation(rb.rotation - rotationAmount);

            // Movimenta o carro na dire��o em que est� apontando (eixo up � a frente)
            Vector2 movement = transform.up * moveInput * speed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + movement); // Atualiza a posi��o com MovePosition

            // Sincroniza a posi��o e rota��o com os outros jogadores
            photonView.RPC("SyncPosition", RpcTarget.Others, rb.position, rb.rotation);
        }
    }

    // M�todo RPC para sincronizar a posi��o do carro com outros jogadores
    [PunRPC]
    void SyncPosition(Vector2 position, float rotation)
    {
        networkPosition = position;
        networkRotation = rotation; // Armazena o valor da rota��o (em graus)
    }

    // Implementa��o da acelera��o do carro
    public void Accelerate(float amount)
    {
        // Implementa��o adicional de acelera��o, se necess�rio
    }

    // Implementa��o da dire��o do carro
    public void Drive(float amount)
    {
        // Implementa��o adicional de dire��o, se necess�rio
    }

    // Atualiza a posi��o do carro (m�todo adicional)
    public void UpdatePosition()
    {
        // Implementa��o adicional para atualiza��o de posi��o
    }
}