using UnityEngine;

public class CreateCube : MonoBehaviour
{
    void Start()
    {
        // Créer un nouveau GameObject qui sera notre cube
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        
        // Positionner le cube à une certaine position dans l'espace
        cube.transform.position = new Vector3(0f, 0f, 0f);
        
        // Vous pouvez également ajuster la taille du cube si nécessaire
        cube.transform.localScale = new Vector3(1f, 1f, 1f);
        
        // Vous pouvez aussi ajouter d'autres composants ou modifier ses propriétés ici si nécessaire
        
        // Facultatif : changer la couleur du cube pour le rendre plus visible
        cube.GetComponent<Renderer>().material.color = Color.blue;
        
        // Ajouter un script de rotation au cube en utilisant MyQuaternion
        MyQuaternion rotationQuaternion = new MyQuaternion(1f, 0f, 1f, 0f); // Exemple de quaternion de rotation
        cube.transform.rotation = rotationQuaternion.ToMatrix().rotation;

        // Attacher un script pour faire tourner le cube
        cube.AddComponent<RotateCube>();
    }
}