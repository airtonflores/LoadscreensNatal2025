using UnityEngine;
using UnityEngine.UI;

/*Adicione outline ao objeto Raw Image se for criar uma 
borda. E este adiciona efeito a borda pelo outline, 
adicionar ao Raw Image que contém o vídeo.
Ele encontra-se atrás da borda PNG do vídeo.
*/

[RequireComponent(typeof(Outline))]
public class VideoEffectsSFX : MonoBehaviour
{
    [Header("Configurações de Transparência")]
    public Color corDaBorda = Color.blue;
    public float velocidadePulso = 1.5f; // Diminuído para ser mais lento
    [Range(0.1f, 1f)] public float opacidadeMinima = 0.5f; // Aumentado para não sumir tanto

    [Header("Configurações de Tamanho")]
    public float espessuraMinima = 5f;
    public float espessuraMaxima = 10f; // Diferença menor entre min e max para ser mais sutil

    private Outline _outline;

    void Start()
    {
        _outline = GetComponent<Outline>();
    }

    void Update()
    {
        // Usamos um tempo mais cadenciado para o efeito "lento"
        float seno = (Mathf.Sin(Time.time * velocidadePulso) + 1.0f) / 2.0f;

        // 1. Efeito de Transparência (Alpha)
        float alpha = Mathf.Lerp(opacidadeMinima, 1f, seno);
        Color novaCor = corDaBorda;
        novaCor.a = alpha;
        _outline.effectColor = novaCor;

        // 2. Efeito de Espessura Suave
        // Para "suavizar" as pontas no Outline nativo, evite valores ímpares e muito altos
        float tamanho = Mathf.Lerp(espessuraMinima, espessuraMaxima, seno);
        _outline.effectDistance = new Vector2(tamanho, -tamanho);
    }
}