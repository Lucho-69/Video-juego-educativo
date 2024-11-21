using System;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public static class ProfileStorage
{
    public static ProfileData s_currentProfile;
    private static string s_indexPath = Application.streamingAssetsPath + "/Profiles/_ProfileIndex_.xml";

    // M�todo para crear un nuevo perfil de juego
    public static void CreateNewGame(string profileName)
    {
        s_currentProfile = new ProfileData(profileName, true, 0, 0);
        string path = Application.streamingAssetsPath + "/Profiles/" + s_currentProfile.fileName;
        SaveFile<ProfileData>(path, s_currentProfile);

        var index = GetProfileIndex();
        index.profileFileNames.Add(s_currentProfile.fileName);

        SaveFile<ProfileIndex>(s_indexPath, index);
    }

    // M�todo para obtener el �ndice de perfiles
    public static ProfileIndex GetProfileIndex()
    {
        // Verificaci�n si el archivo de �ndice existe y no est� vac�o
        if (File.Exists(s_indexPath) == false || new FileInfo(s_indexPath).Length == 0)
        {
            Debug.LogWarning("Archivo de �ndice no encontrado o est� vac�o. Creando nuevo �ndice.");
            var newProfileIndex = new ProfileIndex();
            SaveFile<ProfileIndex>(s_indexPath, newProfileIndex);  // Crear un archivo de �ndice nuevo si no existe o est� vac�o
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

    // M�todo para guardar un archivo XML
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
    // M�todo para cargar un archivo XML
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
            Debug.LogError($"Error de deserializaci�n. Aseg�rate de que el archivo XML est� bien formado. Error: {e.Message}");
            return default(T);
        }
    }
}
