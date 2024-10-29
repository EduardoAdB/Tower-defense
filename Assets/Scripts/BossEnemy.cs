using System.Collections; // Importa a biblioteca para uso de cole��es
using System.Collections.Generic; // Importa a biblioteca para cole��es gen�ricas
using UnityEngine; // Importa a biblioteca principal do Unity

// Classe BossEnemy que herda da classe EnemyMovement
public class BossEnemy : EnemyMovement
{
    // M�todo chamado uma vez por quadro, onde a l�gica do jogo � atualizada
    private void Update()
    {
        MeiaVida(); // Chama o m�todo MeiaVida para verificar se a habilidade deve ser ativada
        Updt(); // Chama o m�todo Updt da classe pai para atualizar o movimento do inimigo
    }

    // M�todo chamado em intervalos fixos para atualiza��es f�sicas
    private void FixedUpdate()
    {
        FxdUpdate(); // Chama o m�todo FxdUpdate da classe pai para atualizar a f�sica do movimento
    }

    // Sobrescreve o m�todo MeiaVida da classe pai
    public override void MeiaVida()
    {
        // Verifica se os pontos de vida do inimigo est�o iguais � metade da vida total
        // e se a habilidade ainda n�o foi ativada
        if (hitPoints == metadeDaVida && habilidadeAtiva == false)
        {
            habilidadeAtiva = true; // Ativa a habilidade do BossEnemy
            // Aqui pode-se adicionar l�gica para aumentar a dificuldade ou alterar comportamento
        }
    }
}