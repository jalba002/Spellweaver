﻿namespace Spellweaver.Model.Exportables
{
    public abstract class ExportableModel
    {
        public ExportableModel() { }
        public ExportableModel(Spell? original) { }
        public abstract void TransformInternalToCustomExportable(Spell original);
        public abstract Spell TransformToInternalModel();
    }
}