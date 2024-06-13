using UnityEngine;

public class RotateCube : MonoBehaviour
{
    private MyQuaternion currentRotation = new MyQuaternion(1f, 0f, 1f, 0f); // Quaternion de rotation initial
    public RaycastHit ray;
    void OnMouseDown()
    {
        // Lorsque l'utilisateur clique sur le cube, mettre à jour la rotation
        currentRotation *= new MyQuaternion(0.707f, 0.707f, 0f, 0f); // Exemple d'incrémentation de rotation
        transform.rotation = currentRotation.ToMatrix().rotation;
    }
}