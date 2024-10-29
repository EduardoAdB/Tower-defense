using System; // Importa classes e estruturas do sistema
using System.Collections; // Importa a biblioteca para uso de cole��es
using System.Collections.Generic; // Importa a biblioteca para cole��es gen�ricas
using UnityEngine; // Importa a biblioteca principal do Unity

public class Bullet : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb; // Refer�ncia ao componente Rigidbody2D para f�sica

    [Header("Attributes")]
    [SerializeField] private float bulletSpeed = 5f; // Velocidade da bala
    [SerializeField] private int bulletDamage = 1; // Dano causado pela bala

    private Transform target; // O alvo que a bala deve seguir

    // M�todo para definir o alvo da bala
    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    // M�todo chamado em intervalos fixos para atualiza��es de f�sica
    private void FixedUpdate()
    {
        // Se n�o houver alvo, n�o faz nada
        if (!target) return;

        // Faz a bala apontar para o alvo
        transform.up = (target.position - transform.position);

        // Define a velocidade da bala na dire��o que est� apontando
        rb.velocity = transform.up * bulletSpeed;
    }

    // M�todo chamado quando a bala colide com outro objeto
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
            Destroy(gameObject); // Destr�i a bala ap�s causar dano
        }
    }

    // M�todo chamado uma vez por quadro
    private void Update()
    {
        BalaPerdida(); // Verifica se a bala saiu da �rea vis�vel da c�mera
    }

    // M�todo para verificar se a bala saiu da tela
    private void BalaPerdida()
    {
        // Converte a posi��o do objeto para coordenadas da viewport
        Vector3 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);

        // Verifica se o objeto est� fora da �rea vis�vel da c�mera
        if (viewportPosition.x < 0 || viewportPosition.x > 1 ||
            viewportPosition.y < 0 || viewportPosition.y > 1)
        {
            Destroy(gameObject); // Destr�i o objeto se estiver fora da tela
        }
    }
}
