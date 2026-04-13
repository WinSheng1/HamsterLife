using UnityEngine;
using UnityEngine.Playables;

public class InteractableGiftbox : InteractableDrawer
{
    [SerializeField] private PlayableDirector cutsceneDirector;

    public override void Interact()
    {
        if (!CanInteract())
        {
            return;
        }

        SetInteracted(true);
        PlayCutscene();
    }

    private void PlayCutscene()
    {
        if (cutsceneDirector != null)
        {
            cutsceneDirector.Play();
        }
    }
}
