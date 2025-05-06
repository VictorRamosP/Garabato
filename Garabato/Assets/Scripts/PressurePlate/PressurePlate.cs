using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public Door door; 
    public bool keepOpen = true; // Para indicar si la puerta se queda abierta al pasar una vez o hay que estar encima de la placa para que se abra

    private bool isPlayerOnPlate = false;
    private bool hasActivated = false; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Box"))
        {
            if (keepOpen && !hasActivated)
            {
                door.Open();
                hasActivated = true;
            }
            else if (!keepOpen && !isPlayerOnPlate)
            {
                door.Open();
                isPlayerOnPlate = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !keepOpen && isPlayerOnPlate)
        {
            door.Close();
            isPlayerOnPlate = false;
        }
    }
}
