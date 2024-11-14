using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Classe BossEnemy que herda da classe EnemyMovement
public class BossEnemy : EnemyMovement, IMeiaVida
{
    // M�todo chamado a cada frame
    private void Update()
    {
        MeiaVida(); // Verifica se o boss atingiu metade da vida para ativar habilidade
        Updt();     // Chama o m�todo Update da classe base
    }

    // M�todo chamado em intervalos fixos (ideal para f�sica e movimento)
    private void FixedUpdate()
    {
        FxdUpdate(); // Chama o m�todo FixedUpdate da classe base
    }

    // Sobrescrita do m�todo MeiaVida da classe base
    public virtual void MeiaVida()
    {
        // Condicional para verificar se os pontos de vida atingiram metade e se a habilidade ainda n�o est� ativa
        if (hitPoints == metadeDaVida && habilidadeAtiva == false)
        {
            habilidadeAtiva = true; // Ativa a habilidade quando condi��o � atendida
        }
    }
}
