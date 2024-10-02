namespace Spellweaver.Data
{
    public abstract class ExportableSpellbook
    {
        public abstract void Export(IList<ExportableModel> spells);
    }
}