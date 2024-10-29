using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Classe FastEnemy que herda da classe EnemyMovement
public class FastEnemy : EnemyMovement
{

    private void FixedUpdate()
    {
        FxdUpdate();  // Chama o m�todo FxdUpdate da classe pai para atualizar a f�sica do movimento
    }

    // Sobrescreve o m�todo MeiaVida da classe pai
    public override void MeiaVida()
    {
        // Verifica se os pontos de vida do inimigo est�o iguais � metade da vida total e se a habilidade ainda n�o foi ativada
        if (hitPoints == metadeDaVida && habilidadeAtiva == false)
        {
            habilidadeAtiva = true;  // Ativa a habilidade
            baseSpeed = 4;  // Aumenta a velocidade base do inimigo
        }
    }
}
