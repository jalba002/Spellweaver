namespace Spellweaver.ViewModel.Items
{
    public class ExportableTypeItemViewModel
    {
        public ExportationType ExportationType { get; set; }
    }

    public enum ExportationType
    {
        Spellweaver,
        Open5e,
        EditionSpellbook5th,
    }
}
