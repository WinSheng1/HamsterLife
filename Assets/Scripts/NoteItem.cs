using UnityEngine;

public class NoteItem : Item
{
    [TextArea(3, 6)]
    [SerializeField] private string noteMessage;

    public override void UseItem()
    {
        if (NoteUIController.Instance != null)
        {
            NoteUIController.Instance.ShowNote(noteMessage);
        }
    }
}
