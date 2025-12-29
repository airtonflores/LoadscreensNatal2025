using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;
using System.Collections;

/*Esse script encerra o jogo quando houver alguma ação 
de mouse, keyboard ou xbox controller

Instant Loading Screen
*/

public class QuitOnAnyInput : MonoBehaviour
{
    void Update()
    {
        // 1. Verifica Teclado
        if (Keyboard.current != null && Keyboard.current.anyKey.wasPressedThisFrame)
        {
            QuitGame();
        }

        // 2. Verifica Mouse/Pointer
        if (Pointer.current != null && Pointer.current.press.wasPressedThisFrame)
        {
            QuitGame();
        }

        // 3. Verifica Gamepad (Joystick) - Agora com LINQ funcionando
        if (Gamepad.current != null)
        {
            // O erro acontecia aqui porque o .Any() não era reconhecido
            if (Gamepad.current.allControls.Any(x => x is UnityEngine.InputSystem.Controls.ButtonControl button && button.wasPressedThisFrame))
            {
                QuitGame();
            }
        }
    }

    public void QuitGame()
    {
        Debug.Log("Fechando o jogo...");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}