using System.Collections.Generic;
using UnityEngine;

public class RankingList : MonoBehaviour
{
    public Transform profilesHolder; // Contenedor en el UI para los perfiles
    public GameObject rankingBoxUIPrefab; // Prefab para mostrar cada perfil

    void Start()
    {
        // Obtener los nombres de los perfiles desde el índice
        var index = ProfileStorage.GetProfileIndex();
        List<(string name, int score)> profiles = new List<(string, int)>();

        // Simular la carga de perfiles y puntajes
        foreach (var profileName in index.profileFileNames)
        {
            int score = ProfileStorage.GetProfileScore(profileName); // Suponiendo que este método devuelve el puntaje
            profiles.Add((profileName, score));
        }

        // Ordenar perfiles por puntaje de mayor a menor
        profiles.Sort((a, b) => b.score.CompareTo(a.score));

        // Mostrar los perfiles en la interfaz
        int rank = 1;
        foreach (var (name, score) in profiles)
        {
            var go = Instantiate(rankingBoxUIPrefab, profilesHolder);
            var uibox = go.GetComponent<RankingBoxUI>();

            // Configurar texto en el prefab
            uibox.nameLabel.text = $"{rank}° {name}";
            uibox.scoreLabel.text = $"Puntaje: {score}";

            rank++;
        }
    }
}