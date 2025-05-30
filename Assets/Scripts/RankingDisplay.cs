using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using System.Collections;

public class RankingDisplay : MonoBehaviour
{
    public TMP_Text rankingText;
    public string apiUrl = "http://192.168.0.10:5145/api/scores";

    void Start()
    {
        StartCoroutine(LoadRanking());
    }

    IEnumerator LoadRanking()
    {
        UnityWebRequest www = UnityWebRequest.Get(apiUrl);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            rankingText.text = "Erro: " + www.error;
        }
        else
        {
            string json = www.downloadHandler.text;
            RankingEntry[] scores = JsonHelper.FromJson<RankingEntry>(json);

            rankingText.text = "🏆 TOP 10 RANKING:\n\n";
            for (int i = 0; i < scores.Length; i++)
            {
                var medalha = i switch
                {
                    0 => "🥇 ",
                    1 => "🥈 ",
                    2 => "🥉 ",
                    _ => ""
                };

                rankingText.text += $"{medalha}{i + 1}. {scores[i].nome} - {scores[i].pontuacao}\n";
            }
        }
    }
}
