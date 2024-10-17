using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


/// Faz a c�mera seguir o carro do jogador.
public class CameraFollow : MonoBehaviour
{
    // O carro que a c�mera deve seguir
    private Transform target;

    // Posi��o relativa da c�mera em rela��o ao carro
    [SerializeField] Vector3 offset;

    // Velocidade suave para movimenta��o da c�mera
    public float smoothSpeed = 0.125f;

    // Ajusta a posi��o da c�mera ap�s o spawn do carro.
    void LateUpdate()
    {
        if (target != null)
        {
            // Posi��o desejada da c�mera com o offset
            Vector3 desiredPosition = target.position + offset;

            // Movimento suave da c�mera at� a posi��o desejada
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Aplica a posi��o suavizada � c�mera
            transform.position = smoothedPosition;

            // Opcional: Faz a c�mera olhar para o carro
            transform.LookAt(target);
        }
    }

    // M�todo p�blico para definir o alvo (carro) que a c�mera deve seguir.
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
