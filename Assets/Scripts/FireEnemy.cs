using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Classe FireEnemy que herda da classe EnemyMovement
public class FireEnemy : EnemyMovement
{
    private void Update()
    {
        MeiaVida();  // Chama o m�todo MeiaVida para verificar se a habilidade deve ser ativada
        Updt();  // Chama o m�todo Updt da classe pai para atualizar o movimento do inimigo
    }

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
            hitPoints = hitPoints * 2;  // Dobra os pontos de vida do inimigo
            baseSpeed = 0.5f;  // Diminui a velocidade base do inimigo
        }
    }
}