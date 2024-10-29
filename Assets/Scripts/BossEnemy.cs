using System.Collections; // Importa a biblioteca para uso de coleções
using System.Collections.Generic; // Importa a biblioteca para coleções genéricas
using UnityEngine; // Importa a biblioteca principal do Unity

// Classe BossEnemy que herda da classe EnemyMovement
public class BossEnemy : EnemyMovement
{
    // Método chamado uma vez por quadro, onde a lógica do jogo é atualizada
    private void Update()
    {
        MeiaVida(); // Chama o método MeiaVida para verificar se a habilidade deve ser ativada
        Updt(); // Chama o método Updt da classe pai para atualizar o movimento do inimigo
    }

    // Método chamado em intervalos fixos para atualizações físicas
    private void FixedUpdate()
    {
        FxdUpdate(); // Chama o método FxdUpdate da classe pai para atualizar a física do movimento
    }

    // Sobrescreve o método MeiaVida da classe pai
    public override void MeiaVida()
    {
        // Verifica se os pontos de vida do inimigo estão iguais à metade da vida total
        // e se a habilidade ainda não foi ativada
        if (hitPoints == metadeDaVida && habilidadeAtiva == false)
        {
            habilidadeAtiva = true; // Ativa a habilidade do BossEnemy
            // Aqui pode-se adicionar lógica para aumentar a dificuldade ou alterar comportamento
        }
    }
}