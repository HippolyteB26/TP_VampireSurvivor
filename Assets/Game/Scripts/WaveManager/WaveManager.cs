using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    // Structure pour configurer chaque vague dans l'Inspecteur
    [System.Serializable]
    public struct Wave
    {
        public string waveName;
        public GameObject[] enemyPrefabs;
        public int enemyCount;
        public float spawnRate;
    }

    [Header("Configuration des Vagues")]
    public Wave[] waves;
    public Transform[] spawnPoints;
    public float timeBetweenWaves = 5f;

    private int currentWaveIndex = 0;
    private int enemiesAlive = 0;
    private bool isSpawning = false;

    void Start()
    {
        if (waves.Length == 0)
        {
            Debug.LogError("Aucune vague n'a été configurée dans l'inspecteur !");
            return;
        }

        // Lance la première vague
        StartCoroutine(StartNextWave());
    }

    IEnumerator StartNextWave()
    {
        isSpawning = true;
        Debug.Log($"Début de la vague : {waves[currentWaveIndex].waveName}");

        Wave currentWave = waves[currentWaveIndex];

        // Apparition des ennemis un par un
        for (int i = 0; i < currentWave.enemyCount; i++)
        {
            SpawnEnemy(currentWave.enemyPrefabs);
            yield return new WaitForSeconds(1f / currentWave.spawnRate);
        }

        isSpawning = false;
    }

    void SpawnEnemy(GameObject[] prefabs)
    {
        // Choisit un ennemi au hasard parmi la liste disponible pour cette vague
        GameObject randomPrefab = prefabs[Random.Range(0, prefabs.Length)];

        // Choisit un point d'apparition au hasard
        Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        // Instanciation de l'ennemi
        GameObject enemy = Instantiate(randomPrefab, randomSpawnPoint.position, randomSpawnPoint.rotation);

        // S'abonne à l'événement de destruction pour savoir quand l'ennemi meurt
        enemiesAlive++;
        enemy.GetComponent<EnemyHealth>().OnEnemyDestroyed += EnemyKilled;
    }

    void EnemyKilled()
    {
        enemiesAlive--;

        // Si tous les ennemis sont morts et que le spawner a fini son travail
        if (enemiesAlive <= 0 && !isSpawning)
        {
            currentWaveIndex++;

            // Vérifie s'il reste des vagues (ex: s'arrêter après la 8ème vague)
            if (currentWaveIndex < waves.Length)
            {
                StartCoroutine(WaveClearDelay());
            }
            else
            {
                Debug.Log("Félicitations ! Vous avez survécu aux 8 vagues !");
                // Insérez ici votre logique de victoire (UI, changement de scène...)
            }
        }
    }

    IEnumerator WaveClearDelay()
    {
        Debug.Log($"Vague terminée ! Prochaine vague dans {timeBetweenWaves} secondes.");
        yield return new WaitForSeconds(timeBetweenWaves);
        StartCoroutine(StartNextWave());
    }
}