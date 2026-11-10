using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class Player_Movement : MonoBehaviour
{
    public InputActionReference moveAction;  
    public float speed = 5f;

    private CharacterController controller;
    
    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Vector2 Input = moveAction.action.ReadValue<Vector2>();
        Vector3 direction = new Vector3(Input.x, 0f, Input.y);

        Vector3 velocity = direction * speed;
        controller.Move(velocity * Time.deltaTime);
    }
}