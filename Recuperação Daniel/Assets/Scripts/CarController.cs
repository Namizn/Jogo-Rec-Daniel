using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; // Biblioteca para RPC
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
    private Vector3 networkPosition;
    private Quaternion networkRotation;

    // Inicializa o componente Rigidbody.

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (photonView.IsMine)
        {
            // Opcional: Configure a c�mera para seguir este carro
        }
        else
        {
            // Desativa o controle local para carros que n�o s�o do jogador
            enabled = false;
        }
    }

    // Captura a entrada do jogador.

    void Update()
    {
        if (photonView.IsMine)
        {
            moveInput = Input.GetAxis("Vertical");
            turnInput = Input.GetAxis("Horizontal");
        }
        else
        {
            // Atualiza a posi��o e rota��o do carro de acordo com a rede
            transform.position = Vector3.Lerp(transform.position, networkPosition, Time.deltaTime * 10);
            transform.rotation = Quaternion.Lerp(transform.rotation, networkRotation, Time.deltaTime * 10);
        }
    }

    // Movimenta e rotaciona o carro com base na entrada do jogador.
    
    void FixedUpdate()
    {
        if (photonView.IsMine)
        {
            // Movimenta o carro aplicando for�a no Rigidbody2D
            Vector2 movement = transform.up * moveInput * speed * Time.fixedDeltaTime;
            rb.AddForce(movement);

            // Rotaciona o carro
            float rotation = turnInput * turnSpeed * Time.fixedDeltaTime;
            rb.MoveRotation(rb.rotation - rotation); // Usamos -rotation para inverter a rota��o no eixo 2D

            // Sincroniza a posi��o e rota��o com os outros jogadores
            photonView.RPC("SyncPosition", RpcTarget.Others, rb.position, rb.rotation);
        }
    }
    
    // M�todo RPC para sincronizar a posi��o do carro com outros jogadores.
    [PunRPC]
    void SyncPosition(Vector2 position, float rotation)
    {
        networkPosition = position;
        networkRotation = Quaternion.Euler(0, 0, rotation); // Converte o valor de rota��o em um Quaternion
    }

    // Implementa��o da acelera��o do carro.
    public void Accelerate(float amount)
    {
        // Implementa��o da acelera��o (pode ser expandida)
    }

    // Implementa��o da dire��o do carro.
    public void Drive(float amount)
    {
        // Implementa��o da dire��o (pode ser expandida)
    }


    // Atualiza a posi��o do carro.
    public void UpdatePosition()
    {
        // Implementa��o da atualiza��o de posi��o (pode ser expandida)
    }
}
