using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class APIClient : MonoBehaviour
{
    public string apiUrl = "http://192.168.0.10:5145/api/scores";

    public void EnviarDados(string nome, int pontuacao)
    {
        StartCoroutine(PostScore(nome, pontuacao));
    }


    IEnumerator PostScore(string nome, int pontuacao)
    {
        Score score = new Score { nome = nome, pontuacao = pontuacao };
        string json = JsonUtility.ToJson(score);

        UnityWebRequest www = new UnityWebRequest(apiUrl, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
        www.uploadHandler = new UploadHandlerRaw(bodyRaw);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Erro ao enviar score: " + www.error);
        }
        else
        {
            Debug.Log("Score enviado com sucesso!");
        }
    }

    public void BuscarTop10(System.Action<RankingEntry[]> callback)
    {
        StartCoroutine(GetTop10(callback));
    }

    IEnumerator GetTop10(System.Action<RankingEntry[]> callback)
    {
        UnityWebRequest www = UnityWebRequest.Get(apiUrl);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Erro ao buscar ranking: " + www.error);
        }
        else
        {
            string json = "{\"scores\":" + www.downloadHandler.text + "}";
            RankingList ranking = JsonUtility.FromJson<RankingList>(json);
            callback?.Invoke(ranking.scores);
        }
    }
}
