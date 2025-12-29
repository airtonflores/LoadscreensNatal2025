using UnityEngine;
using Steamworks;

/*Este é o Steam Manager com o ID da DLC, utilize 480 como teste
*/

public class SteamManager : MonoBehaviour
{
    void Awake()
    {
        try
        {
            // Substitua pelo seu ID real ou deixe o steam_appid.txt resolver
            SteamClient.Init(4286890);
            Debug.Log("Steam Initialized: " + SteamClient.Name);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Não foi possível conectar à Steam: " + e.Message);
        }
    }

    void OnApplicationQuit()
    {
        SteamClient.Shutdown();
    }
}