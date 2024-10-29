using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Classe FireEnemy representa um inimigo que herda caracter�sticas e comportamento de EnemyMovement
public class FireEnemy : EnemyMovement
{
    // Atualiza o comportamento do inimigo a cada frame
    private void Update()
    {
        MeiaVida(); // Checa se a habilidade "Meia Vida" deve ser ativada
        Updt();     // Executa l�gica de movimento herdada
    }

    // Atualiza a f�sica do movimento do inimigo a cada frame fixo
    private void FixedUpdate()
    {
        FxdUpdate(); // Executa a atualiza��o de movimento herdada em f�sica
    }

    // Sobrescreve o m�todo MeiaVida para definir um comportamento �nico de "habilidade ativa" do FireEnemy
    public override void MeiaVida()
    {
        // Se os pontos de vida forem igual a metadeDaVida e a habilidade ainda n�o estiver ativa
        if (hitPoints == metadeDaVida && habilidadeAtiva == false)
        {
            habilidadeAtiva = true; // Ativa a habilidade

            hitPoints = hitPoints * 2; // Dobra os pontos de vida do inimigo
            baseSpeed = 0.5f;          // Reduz a velocidade base do inimigo
        }
    }
}
