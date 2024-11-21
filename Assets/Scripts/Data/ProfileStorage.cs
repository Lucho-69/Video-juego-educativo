using System;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public static class ProfileStorage
{
    public static ProfileData s_currentProfile;
    private static string s_indexPath = Application.streamingAssetsPath + "/Profiles/_ProfileIndex_.xml";

    // Método para crear un nuevo perfil de juego
    public static void CreateNewGame(string profileName)
    {
        s_currentProfile = new ProfileData(profileName, true, 0, 0);
        string path = Application.streamingAssetsPath + "/Profiles/" + s_currentProfile.fileName;
        SaveFile<ProfileData>(path, s_currentProfile);

        var index = GetProfileIndex();
        index.profileFileNames.Add(s_currentProfile.fileName);

        SaveFile<ProfileIndex>(s_indexPath, index);
    }

    // Método para obtener el índice de perfiles
    public static ProfileIndex GetProfileIndex()
    {
        // Verificación si el archivo de índice existe y no está vacío
        if (File.Exists(s_indexPath) == false || new FileInfo(s_indexPath).Length == 0)
        {
            Debug.LogWarning("Archivo de índice no encontrado o está vacío. Creando nuevo índice.");
            var newProfileIndex = new ProfileIndex();
            SaveFile<ProfileIndex>(s_indexPath, newProfileIndex);  // Crear un archivo de índice nuevo si no existe o está vacío
            return newProfileIndex;
        }
        return LoadFile<ProfileIndex>(s_indexPath);
    }


    public static void loadProfile(string fileName)
    {
        string path = Application.streamingAssetsPath + "/Profiles/" + fileName;
        s_currentProfile = LoadFile<ProfileData>(path);
    }

    public static void StorePlayerProfile(GameObject player)
    {
        s_currentProfile.x = player.transform.position.x;
        s_currentProfile.y = player.transform.position.y;
        s_currentProfile.newGame = false;

        var path = Application.streamingAssetsPath + "/Profiles/" + s_currentProfile.fileName;
        SaveFile<ProfileData>(path, s_currentProfile);
    }

    // Método para guardar un archivo XML
    static void SaveFile<T>(string path, T data)
    {
        try
        {
            var profileWriter = new StreamWriter(path);
            var profileSerializer = new XmlSerializer(typeof(T));
            profileSerializer.Serialize(profileWriter, data);
            profileWriter.Dispose();
        }
        catch (IOException e)
        {
            Debug.LogError($"Error al guardar el archivo: {e.Message}");
        }
    }


    public static void DeleteProfile(string filename)
    {
        var path = Application.streamingAssetsPath + "/Profiles/" + filename;
        File.Delete(path);

        var index = LoadFile<ProfileIndex>(s_indexPath);
        index.profileFileNames.Remove(filename);

        SaveFile<ProfileIndex>(s_indexPath, index);
    }
    // Método para cargar un archivo XML
    static T LoadFile<T>(string path)
    {
        try
        {
            var profileReader = new StreamReader(path);
            var serializer = new XmlSerializer(typeof(T));
            var obj = (T)serializer.Deserialize(profileReader);
            profileReader.Dispose();
            return obj;
        }
        catch (FileNotFoundException e)
        {
            Debug.LogError($"Archivo no encontrado: {path}. Error: {e.Message}");
            return default(T);
        }
        catch (InvalidOperationException e)
        {
            Debug.LogError($"Error de deserialización. Asegúrate de que el archivo XML esté bien formado. Error: {e.Message}");
            return default(T);
        }
    }
}
