using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public InputActionReference moveAction;


    private Animator AttackAnim;
    void Awake()
    {
        AttackAnim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (moveAction.action.IsPressed())
        {
            AttackAnim.SetTrigger("Attack");
        }
    }

}
