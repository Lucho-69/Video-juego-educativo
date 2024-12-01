using System;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class ProfileStorage
{
    public static ProfileData s_currentProfile;
    private static string s_indexPath = Application.persistentDataPath + "/Profiles/_ProfileIndex_.xml";

    // Método para crear un nuevo perfil
    public static void CreateNewGame(string profileName)
    {
        s_currentProfile = new ProfileData(profileName, true, 1, 0); // Nuevo perfil en nivel 1 con puntaje 0
        string path = Application.persistentDataPath + "/Profiles/" + s_currentProfile.fileName;
        EnsureDirectoryExists(path);
        SaveFile(path, s_currentProfile);

        var index = GetProfileIndex();
        index.profileFileNames.Add(s_currentProfile.fileName);

        SaveFile(s_indexPath, index);
    }

    // Método para cargar un perfil existente
    public static void LoadProfile(string fileName)
    {
        string path = Application.persistentDataPath + "/Profiles/" + fileName;
        s_currentProfile = LoadFile<ProfileData>(path);
    }

    // Método para guardar el progreso del jugador
    public static void StorePlayerProgress(int currentLevel, int score)
    {
        s_currentProfile.currentLevel = currentLevel;
        s_currentProfile.score = score;
        s_currentProfile.newGame = false;

        string path = Application.persistentDataPath + "/Profiles/" + s_currentProfile.fileName;
        SaveFile(path, s_currentProfile);
    }

    // Método para eliminar un perfil
    public static void DeleteProfile(string filename)
    {
        string path = Application.persistentDataPath + "/Profiles/" + filename;

        if (File.Exists(path))
        {
            File.Delete(path);
        }
        else
        {
            Debug.LogWarning($"El archivo {filename} no existe y no se puede eliminar.");
        }

        var index = GetProfileIndex();
        if (index.profileFileNames.Contains(filename))
        {
            index.profileFileNames.Remove(filename);
            SaveFile(s_indexPath, index);
        }
    }

    // Método para obtener el índice de perfiles
    public static ProfileIndex GetProfileIndex()
    {
        if (!File.Exists(s_indexPath) || new FileInfo(s_indexPath).Length == 0)
        {
            Debug.LogWarning("Índice de perfiles no encontrado. Creando uno nuevo.");
            var newIndex = new ProfileIndex();
            SaveFile(s_indexPath, newIndex);
            return newIndex;
        }
        return LoadFile<ProfileIndex>(s_indexPath);
    }

    // Métodos genéricos para manejar archivos XML
    private static void SaveFile<T>(string path, T data)
    {
        try
        {
            EnsureDirectoryExists(path);
            using (var writer = new StreamWriter(path))
            {
                var serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(writer, data);
            }
        }
        catch (IOException e)
        {
            Debug.LogError($"Error al guardar el archivo: {e.Message}");
        }
    }

    private static T LoadFile<T>(string path)
    {
        try
        {
            if (!File.Exists(path))
            {
                Debug.LogWarning($"Archivo no encontrado: {path}");
                return default(T);
            }

            using (var reader = new StreamReader(path))
            {
                var serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(reader);
            }
        }
        catch (IOException e)
        {
            Debug.LogError($"Error al cargar el archivo: {e.Message}");
            return default(T);
        }
    }

    private static void EnsureDirectoryExists(string path)
    {
        string directory = Path.GetDirectoryName(path);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
    }
}
