using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Pontuação e Dificuldade")]
    public int score = 0;
    private int errors = 0;
    public int maxErrors = 3;
    public float difficultyMultiplier = 0.5f;
    private bool increasedAt10 = false;
    private bool increasedAt20 = false;

    [Header("UI")]
    public TMP_Text scoreText;
    public GameObject gameOverPanel;
    public TMP_Text gameOverScoreText;
    public TMP_InputField jogadorInputField;

    private APIClient apiClient;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        apiClient = FindObjectOfType<APIClient>();
        if (apiClient == null)
            Debug.LogError("APIClient não encontrado na cena!");
        else
            Debug.Log("APIClient encontrado com sucesso.");
    }

    public void AddPoint()
    {
        score += 1;
        scoreText.text = "Pontos: " + score;

        if (score >= 5 && !increasedAt10)
        {
            IncreaseTrashSpeed();
            increasedAt10 = true;
        }

        if (score >= 10 && !increasedAt20)
        {
            IncreaseTrashSpeed();
            increasedAt20 = true;
        }
    }

    private void IncreaseTrashSpeed()
    {
        ScriptTrash[] trashList = FindObjectsOfType<ScriptTrash>();
        foreach (var trash in trashList)
        {
            trash.fallSpeed += difficultyMultiplier;
        }
    }

    public void AddError()
    {
        errors++;

        if (errors >= maxErrors)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        gameOverPanel.SetActive(true);
        gameOverScoreText.text = "PONTUAÇÃO FINAL: " + score;
    }

    public void SalvarScore()
    {
        if (jogadorInputField == null)
        {
            Debug.LogError("Campo jogadorInputField não está atribuído no Inspector!");
            return;
        }

        string nomeJogador = jogadorInputField.text;

        if (!string.IsNullOrWhiteSpace(nomeJogador))
        {
            if (apiClient != null)
            {
                apiClient.EnviarDados(nomeJogador, score);
                Debug.Log("Score enviado para a API: " + nomeJogador + " - " + score);
            }
            else
            {
                Debug.LogError("APIClient ainda é nulo em SalvarScore.");
            }
        }
        else
        {
            Debug.LogWarning("Nome do jogador não preenchido.");
        }
    }
}
