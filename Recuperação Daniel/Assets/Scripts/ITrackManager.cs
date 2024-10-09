using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITrackManager
{
    // M�todo para gerenciar a sincroniza��o de posi��es dos carros
    void SyncCarPositions();

    // M�todo para verificar se algum carro cruzou a linha de chegada
    void CheckFinish();
}
