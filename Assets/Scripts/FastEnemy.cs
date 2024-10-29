using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Classe FastEnemy que herda da classe EnemyMovement
public class FastEnemy : EnemyMovement
{
    private void Update()
    {
        MeiaVida();  // Chama o método MeiaVida para verificar se a habilidade deve ser ativada
        Updt();  // Chama o método Updt da classe pai para atualizar o movimento do inimigo
    }

    private void FixedUpdate()
    {
        FxdUpdate();  // Chama o método FxdUpdate da classe pai para atualizar a física do movimento
    }

    // Sobrescreve o método MeiaVida da classe pai
    public override void MeiaVida()
    {
        // Verifica se os pontos de vida do inimigo estão iguais à metade da vida total e se a habilidade ainda não foi ativada
        if (hitPoints == metadeDaVida && habilidadeAtiva == false)
        {
            habilidadeAtiva = true;  // Ativa a habilidade
            baseSpeed = 4;  // Aumenta a velocidade base do inimigo
        }
    }
}
