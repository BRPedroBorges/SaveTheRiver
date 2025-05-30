[System.Serializable]
public class RankingEntry
{
    public string nome;
    public int pontuacao;
}

[System.Serializable]
public class RankingList
{
    public RankingEntry[] scores;
}
