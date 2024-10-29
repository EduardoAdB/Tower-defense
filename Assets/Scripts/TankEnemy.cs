using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Classe TankEnemy que herda da classe EnemyMovement
public class TankEnemy : EnemyMovement
{
    // M�todo chamado a cada frame
    private void Update()
    {
        MeiaVida(); // Verifica condi��o para ativa��o de habilidade ao atingir metade da vida
        Updt();     // Chama o m�todo Update da classe base
    }

    // M�todo chamado em intervalos fixos, ideal para f�sica e movimenta��o
    private void FixedUpdate()
    {
        FxdUpdate(); // Chama o m�todo FixedUpdate da classe base
    }

    // Sobrescrita do m�todo MeiaVida da classe base, adaptado para o TankEnemy
    public override void MeiaVida()
    {
        // Verifica se os pontos de vida atingiram metade e se a habilidade ainda n�o est� ativa
        if (hitPoints == metadeDaVida && habilidadeAtiva == false)
        {
            habilidadeAtiva = true;     // Ativa a habilidade ao atingir metade da vida
            hitPoints = hitPoints * 4;  // Aumenta os pontos de vida 4 vezes
        }
    }
}
