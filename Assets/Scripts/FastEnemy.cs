using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

// Classe FastEnemy que herda de EnemyMovement
public class FastEnemy : EnemyMovement
{
    // M�todo chamado a cada frame
    private void Update()
    {
        MeiaVida(); // Verifica condi��o para ativar habilidade ao atingir metade da vida
        Updt();     // Chama o m�todo Update da classe base
    }

    // M�todo chamado em intervalos fixos, ideal para f�sica e movimenta��o
    private void FixedUpdate()
    {
        FxdUpdate(); // Chama o m�todo FixedUpdate da classe base
    }

    // Sobrescrita do m�todo MeiaVida da classe base, adaptado para FastEnemy
    public override void MeiaVida()
    {
        // Verifica se os pontos de vida atingiram metade e se a habilidade ainda n�o est� ativa
        if (hitPoints == metadeDaVida && habilidadeAtiva == false)
        {
            habilidadeAtiva = true; // Ativa a habilidade ao atingir metade da vida
            baseSpeed = 6;          // Aumenta a velocidade base para 6
        }
    }
}
