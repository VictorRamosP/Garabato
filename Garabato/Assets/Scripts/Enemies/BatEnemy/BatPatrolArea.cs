using UnityEngine;
using System;

public class BatPatrolArea : MonoBehaviour
{
    public event Action<Collider2D> SomethingInArea;
    public event Action<Collider2D> SomethingLeftArea;
    public Vector2 GetNewTargetPosition()
    {
        BoxCollider2D box = GetComponent<BoxCollider2D>();

        // Obtener un punto aleatorio dentro del box en coordenadas locales
        Vector2 localPoint = new Vector2(
            UnityEngine.Random.Range(-0.5f, 0.5f) * box.size.x,
            UnityEngine.Random.Range(-0.5f, 0.5f) * box.size.y
        );

        // Sumar el offset local del collider
        localPoint += box.offset;

        // Convertir ese punto local en global teniendo en cuenta rotación, posición y escala
        Vector2 worldPoint = transform.TransformPoint(localPoint);

        return worldPoint;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        SomethingInArea?.Invoke(collision);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        SomethingLeftArea?.Invoke(collision);
    }
}
