using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] PlayerController PlayerController;
    public float interactionDistance;
    public Transform rayPos;
    public Text interactionText;
    public GameObject interactionHoldGO;
    public Image interactionHoldProgress;

    void Update()
    {
        bool successfulHit = false;
        Debug.DrawRay(transform.position, PlayerController.rayDir * interactionDistance, Color.red);
        RaycastHit2D rayHit = Physics2D.Raycast(transform.position, PlayerController.rayDir, interactionDistance, LayerMask.GetMask("Interact"));

        if (rayHit.collider != null)
        {
            Interactable interactable = rayHit.collider.GetComponent<Interactable>();

            if (interactable != null)
            {
                HandleInteraction(interactable);
                interactionText.text = interactable.GetDescription();
                successfulHit = true;

                interactionHoldGO.SetActive(interactable.interactionType == Interactable.InteractionType.Hold);
            }
        }

        if (!successfulHit)
        {
            interactionText.text = "";
            interactionHoldGO.SetActive(false);
        }
    }

    void HandleInteraction(Interactable interactable)
    {
        KeyCode key = KeyCode.E;
        switch (interactable.interactionType)
        {
            case Interactable.InteractionType.Click:
                if (Input.GetKeyDown(key))
                {
                    interactable.Interact();
                }
                break;
            case Interactable.InteractionType.Hold:
                if (Input.GetKey(key))
                {
                    interactable.IncreaseHoldTime();
                    if (interactable.GetHoldTime() > 1f)
                    {
                        interactable.Interact();
                        interactable.ResetHoldTime();
                    }
                }
                else
                {
                    interactable.ResetHoldTime();
                }
                interactionHoldProgress.fillAmount = interactable.GetHoldTime();
                break;
            default:
                throw new System.Exception("Unsupported type of interactable.");
        }
    }
}
