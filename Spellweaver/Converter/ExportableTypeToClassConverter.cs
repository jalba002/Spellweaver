using Spellweaver.Model;
using Spellweaver.Model.Exportables;
using Spellweaver.ViewModel.Items;

namespace Spellweaver.Converter
{
    public class ExportableTypeToClassConverter
    {
        public static Type Convert(object value)
        {
            // Here we make a switch and depending on the entry we return something else.
            if (value == null) return null;
            ExportableTypeItemViewModel viewModel = (ExportableTypeItemViewModel)value;
            switch (viewModel.ExportationType)
            {
                case ExportationType.Spellweaver:
                    return typeof(Spell);
                case ExportationType.Open5e:
                    return typeof(Open5eSpellExportable);
                case ExportationType.EditionSpellbook5th:
                    return typeof(Spellbook5eExportable);
            }
            return null;
        }
    }
}
