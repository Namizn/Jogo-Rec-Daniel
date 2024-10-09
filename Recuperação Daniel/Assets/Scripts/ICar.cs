using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICar
{
    // M�todo para movimentar o carro
    void Move(float horizontalInput, float verticalInput);

    // M�todo que detecta quando o carro cruza a linha de chegada
    bool CheckFinishLine();
}
