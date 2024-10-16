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
    private Rigidbody rb;


    // Inicializa o componente Rigidbody.
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Captura a entrada do jogador e movimenta o carro se for o jogador local.
    void Update()
    {
    // Verifica se este objeto pertence ao jogador local
        if (photonView.IsMine)
        {
        // Captura entrada de movimento e rota��o
            float move = Input.GetAxis("Vertical") * speed;
            float turn = Input.GetAxis("Horizontal") * turnSpeed;

        // Calcula o movimento e a rota��o
            Vector3 movement = transform.forward * move * Time.deltaTime;
            Quaternion rotation = Quaternion.Euler(0, turn * Time.deltaTime, 0);

        // Move e rotaciona o carro
            rb.MovePosition(rb.position + movement);
            rb.MoveRotation(rb.rotation * rotation);

        // Sincroniza a posi��o com os outros jogadores
            photonView.RPC("SyncPosition", RpcTarget.Others, rb.position, rb.rotation);
        }
    }

    // M�todo RPC para sincronizar a posi��o do carro com outros jogadores.

    [PunRPC]
    void SyncPosition(Vector3 position, Quaternion rotation)
    {
        rb.position = position;
        rb.rotation = rotation;

    }
// Implementa��o da acelera��o do carro
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
