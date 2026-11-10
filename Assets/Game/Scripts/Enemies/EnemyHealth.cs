using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    // Événement auquel le WaveManager va s'abonner
    public event Action OnEnemyDestroyed;

    // Appelez cette méthode lorsque l'ennemi n'a plus de vie
    public void Die()
    {
        // Déclenche l'événement s'il y a des abonnés
        OnEnemyDestroyed?.Invoke();

        // Détruit le GameObject de l'ennemi
        Destroy(gameObject);
    }

    // Exemple de test : si vous cliquez sur l'ennemi, il meurt
    private void OnMouseDown()
    {
        Die();
    }
}
