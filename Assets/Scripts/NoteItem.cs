using UnityEngine;

public class NoteItem : Item
{
    [SerializeField] private string noteMessage;

    public override void UseItem()
    {
        if (NoteUIController.Instance != null)
        {
            NoteUIController.Instance.ShowNote(noteMessage);
        }
    }
}
