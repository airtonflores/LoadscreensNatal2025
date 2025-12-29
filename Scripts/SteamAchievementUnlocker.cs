using UnityEngine;
using System.Collections;
using Steamworks;
using Steamworks.Data;

public class SteamAchievementUnlocker : MonoBehaviour
{
    [Header("Configurações da Conquista")]
    [SerializeField] private string achievementID = "KakauNavidad2025";
    [SerializeField] private float delayBeforeUnlock = 1.0f;

    private IEnumerator Start()
    {
        // Aguarda o tempo definido para garantir que a Steam API carregou os dados do usuário
        yield return new WaitForSeconds(delayBeforeUnlock);

        UnlockAchievement();
    }

    private void UnlockAchievement()
    {
        try 
        {
            // 1. Verifica se o cliente Steam está ativo
            if (!SteamClient.IsValid)
            {
                Debug.LogWarning("Steam não está inicializado. Não é possível verificar conquistas.");
                return;
            }

            // 2. Cria a referência para a conquista
            var ach = new Achievement(achievementID);

            // 3. Verifica se o jogador JÁ possui a conquista
            if (ach.State)
            {
                Debug.Log($"O jogador já possui a conquista '{achievementID}'. Nenhuma ação necessária.");
            }
            else
            {
                // 4. Se não tiver, desbloqueia agora
                ach.Trigger();
                Debug.Log($"Conquista '{achievementID}' desbloqueada com sucesso!");
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Erro ao processar conquista: {e.Message}");
        }
    }
}