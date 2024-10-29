using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Classe FireEnemy representa um inimigo que herda características e comportamento de EnemyMovement
public class FireEnemy : EnemyMovement
{
    // Atualiza o comportamento do inimigo a cada frame
    private void Update()
    {
        MeiaVida(); // Checa se a habilidade "Meia Vida" deve ser ativada
        Updt();     // Executa lógica de movimento herdada
    }

    // Atualiza a física do movimento do inimigo a cada frame fixo
    private void FixedUpdate()
    {
        FxdUpdate(); // Executa a atualização de movimento herdada em física
    }

    // Sobrescreve o método MeiaVida para definir um comportamento único de "habilidade ativa" do FireEnemy
    public override void MeiaVida()
    {
        // Se os pontos de vida forem igual a metadeDaVida e a habilidade ainda não estiver ativa
        if (hitPoints == metadeDaVida && habilidadeAtiva == false)
        {
            habilidadeAtiva = true; // Ativa a habilidade

            hitPoints = hitPoints * 2; // Dobra os pontos de vida do inimigo
            baseSpeed = 0.5f;          // Reduz a velocidade base do inimigo
        }
    }
}
