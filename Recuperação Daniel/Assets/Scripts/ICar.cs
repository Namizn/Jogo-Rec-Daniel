using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICar
{
    // M�todo para acelerar o carro.
    void Accelerate(float amount);

    // M�todo para virar o carro.
    void Drive(float amount);

    // Atualiza a posi��o do carro.
    void UpdatePosition();
}
