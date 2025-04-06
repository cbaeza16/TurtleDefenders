using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class EnemySpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private GameObject restartCanvas; // Asigna el Canvas desde el Inspector

    [Header("Atributes")]
    [SerializeField] private int baseEnemies = 8;
    [SerializeField] private float enemiesPerSecond = 0.5f;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private float difficultyScalingFactor = 0.75f;

    [SerializeField] private int maxEnemiesToSpawnForLevel = 10;

    [SerializeField] private GameObject winCanvas; // Asignar en Inspector para Level 3

    private int totalEnemiesSpawned = 0; // <- Contador global
    private bool levelComplete = false;

    

    [Header("Events")]
    public static UnityEvent onEnemyDestroy = new UnityEvent();

    private int currentWave = 1;
    private float timeSinceLastSpawn;
    private int enemiesAlive;
    private int enemiesLeftToSpawn;
    private bool isSpawning = false;

    private int enemiesReachedEnd = 0;

    private void Awake()
    {
        onEnemyDestroy.AddListener(EnemyDestroyed);

        if (restartCanvas != null)
        {
            restartCanvas.SetActive(false);
        }
    }

    void Start()
    {
        StartCoroutine(StartWave());
    }

    void Update()
    {
        if (!isSpawning){

            if (levelComplete)
            {
                LoadNextLevel();
            }
            return;
        } 

        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= (1f / enemiesPerSecond) && enemiesLeftToSpawn > 0)
        {
            SpawnEnemy();
            enemiesLeftToSpawn--;
            enemiesAlive++;
            timeSinceLastSpawn = 0f;
        }

        if (enemiesAlive == 0 && enemiesLeftToSpawn == 0)
        {
            Debug.Log("Condición de fin de ola alcanzada");
            Debug.Log($"enemiesAlive: {enemiesAlive}, maxEnemiesToSpawnForLevel: {maxEnemiesToSpawnForLevel}, levelComplete: {levelComplete}");
            if (levelComplete)
            {
                LoadNextLevel();
            }
            else
            {
                EndWave();
            }
        }
    }

    private void EnemyDestroyed()
    {
        enemiesAlive--;
        // Aquí ya no usamos esto para "game over"
        // Ahora solo se reduce la cuenta de enemigos vivos
    }

    /// <summary>
    /// Llama este método cuando un enemigo llega al final del camino.
    /// </summary>
    public void NotifyEnemyReachedEnd()
    {
        enemiesAlive--;
        enemiesReachedEnd++;

        Debug.Log("Enemigos que llegaron al final: " + enemiesReachedEnd);

        if (enemiesReachedEnd >= 5)
        {
            isSpawning = false;
            Debug.Log("Game Over!");

            if (restartCanvas != null)
            {
                restartCanvas.SetActive(true);
            }
        }
    }

    private IEnumerator StartWave()
    {
        if (currentWave > 1) yield return new WaitForSeconds(timeBetweenWaves);
        isSpawning = true;
        enemiesLeftToSpawn = EnemiesPerWave();
    }

    private void EndWave()
    {
        isSpawning = false;
        timeSinceLastSpawn = 0f;
        currentWave++;
        StartCoroutine(StartWave());
    }

    private void SpawnEnemy()
    {
        if (enemyPrefabs.Length == 0 || totalEnemiesSpawned >= maxEnemiesToSpawnForLevel) return;

        Instantiate(enemyPrefabs[0], LevelManager.main.StartPoint.position, Quaternion.identity);

        totalEnemiesSpawned++;

        if (totalEnemiesSpawned >= maxEnemiesToSpawnForLevel)
        {
            isSpawning = false;
            levelComplete = true;
        }
    }

    private int EnemiesPerWave()
    {
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, difficultyScalingFactor));
    }

   private void LoadNextLevel()
    {
        Debug.Log("Nivel completado, cargando siguiente nivel...");

        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextSceneIndex >= SceneManager.sceneCountInBuildSettings)
        {
            // Ya no hay más escenas: ganaste el juego
            Debug.Log("¡Juego completado!");

            if (winCanvas != null)
            {
                winCanvas.SetActive(true);
            }
        }
        else
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
    }

}
