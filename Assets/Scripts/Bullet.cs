using System; // Importa classes e estruturas do sistema
using System.Collections; // Importa a biblioteca para uso de coleções
using System.Collections.Generic; // Importa a biblioteca para coleções genéricas
using UnityEngine; // Importa a biblioteca principal do Unity

public class Bullet : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb; // Referência ao componente Rigidbody2D para física

    [Header("Attributes")]
    [SerializeField] private float bulletSpeed = 5f; // Velocidade da bala
    [SerializeField] private int bulletDamage = 1; // Dano causado pela bala

    private Transform target; // O alvo que a bala deve seguir

    // Método para definir o alvo da bala
    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    // Método chamado em intervalos fixos para atualizações de física
    private void FixedUpdate()
    {
        // Se não houver alvo, não faz nada
        if (!target) return;

        // Faz a bala apontar para o alvo
        transform.up = (target.position - transform.position);

        // Define a velocidade da bala na direção que está apontando
        rb.velocity = transform.up * bulletSpeed;
    }

    // Método chamado quando a bala colide com outro objeto
    private void OnTriggerEnter2D(Collider2D collider)
    {
        // Tenta obter o componente 'EnemyMovement' do objeto colidido
        EnemyMovement healthComponent = collider.gameObject.GetComponent<EnemyMovement>();

        // Verifica se o objeto tem o componente 'EnemyMovement'
        if (healthComponent != null)
        {
            // Se tem, aplica o dano
            healthComponent.TakeDamage(bulletDamage);
            Debug.Log("Aplicou dano e destruiu a bala"); // Log de dano aplicado
            Destroy(gameObject); // Destrói a bala após causar dano
        }
    }

    // Método chamado uma vez por quadro
    private void Update()
    {
        BalaPerdida(); // Verifica se a bala saiu da área visível da câmera
    }

    // Método para verificar se a bala saiu da tela
    private void BalaPerdida()
    {
        // Converte a posição do objeto para coordenadas da viewport
        Vector3 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);

        // Verifica se o objeto está fora da área visível da câmera
        if (viewportPosition.x < 0 || viewportPosition.x > 1 ||
            viewportPosition.y < 0 || viewportPosition.y > 1)
        {
            Destroy(gameObject); // Destrói o objeto se estiver fora da tela
        }
    }
}
