using UnityEngine;
using TMPro;

/*Mais utilizado para testes visuais durante
o desenvolvimento.
*/

public class UnderlayController : MonoBehaviour
{
    private TextMeshProUGUI timeText;
    private Material textMaterial;

    [Header("Ajustes de Underlay")]
    [Range(-1f, 1f)] public float offsetX = 0.5f;
    [Range(-1f, 1f)] public float offsetY = -0.5f;
    [Range(0f, 1f)] public float softness = 0.1f;
    [Range(0f, 1f)] public float dilate = 0.1f;
    public Color shadowColor = new Color(0, 0, 0, 0.5f);

    void Start()
    {
        timeText = GetComponent<TextMeshProUGUI>();

        // Criada uma instancia do material para nao afetar outros textos que usem a mesma fonte
        textMaterial = timeText.fontMaterial;

        // Ativa o Underlay no Material
        textMaterial.EnableKeyword("UNDERLAY_ON");
    }

    void Update()
    {
        if (textMaterial == null) return;

        // Atualiza os valores do Material conforme os Sliders do Inspector
        textMaterial.SetColor("_UnderlayColor", shadowColor);
        textMaterial.SetFloat("_UnderlayOffsetX", offsetX);
        textMaterial.SetFloat("_UnderlayOffsetY", offsetY);
        textMaterial.SetFloat("_UnderlayDilate", dilate);
        textMaterial.SetFloat("_UnderlaySoftness", softness);
    }
}