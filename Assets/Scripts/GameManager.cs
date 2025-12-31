using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    [Header("Spawn")]
    [SerializeField] private Transform enemySpawn;
    [SerializeField] private GameObject enemyNormalPrefab;
    [SerializeField] private GameObject enemyStrongPrefab;
    [SerializeField] private Button restartButton;


    [Header("References")]
    [SerializeField] private Health playerHealth;
    [SerializeField] private TMP_Text resultText;

    [SerializeField] private PlayerPunch playerPunch;
    [SerializeField] private PlayerMovement playerMovement;



    private Health enemyHealth;
    private bool finished;
    private GameObject enemyObj;


    void Start()
    {
        // Spawn enemy (normal or strong)
        if (enemySpawn != null && enemyNormalPrefab != null && enemyStrongPrefab != null)
        {
            GameObject chosen = (Random.value < 0.5f) ? enemyNormalPrefab : enemyStrongPrefab;

            // IMPORTANT: assign to the FIELD (no "GameObject" in front) and instantiate only once
            enemyObj = Instantiate(chosen, enemySpawn.position, Quaternion.identity);

            enemyHealth = enemyObj.GetComponent<Health>();

            var playerMovement = playerHealth.GetComponent<PlayerMovement>();
            if (playerMovement != null)
                playerMovement.SetEnemy(enemyObj.transform);

            var ai = enemyObj.GetComponent<EnemyAI>();
            if (ai != null && playerHealth != null)
                ai.SetPlayer(playerHealth.transform);   // use the method, no SendMessage

            if (restartButton != null)
                restartButton.gameObject.SetActive(false);
        }


        if (resultText != null) resultText.text = "";

        if (playerPunch == null && playerHealth != null)
            playerPunch = playerHealth.GetComponent<PlayerPunch>();
        if (playerMovement == null && playerHealth != null)
            playerMovement = playerHealth.GetComponent<PlayerMovement>();


    }

    void Update()
    {
        if (finished) return;
        if (playerHealth == null || resultText == null) return;

        if(enemyHealth.IsDead || playerHealth.IsDead)
        {
            if (playerPunch != null)
                playerPunch.enabled = false;

            if (playerMovement != null)
                playerMovement.enabled = false;

            var rb = playerHealth.GetComponent<Rigidbody2D>();
            if (rb != null)
                rb.linearVelocity = Vector2.zero;

            var anim = playerHealth.GetComponentInChildren<Animator>();
            if (anim != null) anim.SetBool("Walk", false);
        }

        if (playerHealth.IsDead)
        {
            finished = true;
            resultText.text = "You Lose";

            var enemyAI = enemyHealth.GetComponent<EnemyAI>();
            if (enemyAI != null)
                enemyAI.enabled = false;

            if (restartButton != null)
                restartButton.gameObject.SetActive(true);
        }
        else if (enemyObj == null)
        {
            finished = true;
            resultText.text = "You Win";

            if (restartButton != null)
                restartButton.gameObject.SetActive(true);
        }
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}

