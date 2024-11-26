﻿using Spellweaver.Data;

namespace Spellweaver.ViewModel
{
    public class SpellItemViewModel : ViewModelBase<Spell>, ICloneable
    {
        public SpellItemViewModel(Spell model) : base(model)
        {

        }

        // Here we add all variables our spell can have
        // This is long for spells...

        public string? Name
        {
            get => _model.Name;
            set
            {
                _model.Name = value;
                RaisePropertyChanged();
            }
        }
        public string? School
        {
            get => _model.School;
            set
            {
                _model.School = value;
                RaisePropertyChanged();
            }
        }
        public string? Level
        {
            get => _model.Level;
            set
            {
                _model.Level = value;
                RaisePropertyChanged();
            }
        }

        public string? CastingTime
        {
            get => _model.CastingTime;
            set
            {
                _model.CastingTime = value;
                RaisePropertyChanged();
            }
        }
        public string? Target
        {
            get => _model.Target;
            set
            {
                _model.Target = value;
                RaisePropertyChanged();
            }
        }
        public string? Range
        {
            get => _model.Range;
            set
            {
                _model.Range = value;
                RaisePropertyChanged();
            }
        }
        public bool IsVocal
        {
            get => _model.IsVocal;
            set
            {
                _model.IsVocal = value;
                RaisePropertyChanged();
            }
        }
        public bool IsSomatic
        {
            get => _model.IsSomatic;
            set
            {
                _model.IsSomatic = value;
                RaisePropertyChanged();
            }
        }
        public bool IsMaterial
        {
            get => _model.IsMaterial;
            set
            {
                _model.IsMaterial = value;
                RaisePropertyChanged();
            }
        }
        public string? DescriptionMaterials
        {
            get => _model.DescriptionMaterials;
            set
            {
                _model.DescriptionMaterials = value;
                RaisePropertyChanged();
            }
        }
        public string? Duration
        {
            get => _model.Duration;
            set
            {
                _model.Duration = value;
                RaisePropertyChanged();
            }
        }
        public string? Description
        {
            get => _model.Description;
            set
            {
                _model.Description = value;
                RaisePropertyChanged();
            }
        }
        public string? UpcastDescription
        {
            get => _model.UpcastDescription;
            set
            {
                _model.UpcastDescription = value;
                RaisePropertyChanged();
            }
        }
        public string? Source
        {
            get => _model.Source;
            set
            {
                _model.Source = value;
                RaisePropertyChanged();
            }
        }
        public string? Classes
        {
            get => _model.Classes;
            set
            {
                _model.Classes = value;
                RaisePropertyChanged();
            }
        }
        public bool IsConcentration
        {
            get => _model.IsConcentration;
            set
            {
                _model.IsConcentration = value;
                RaisePropertyChanged();
            }
        }
        public bool IsRitual
        {
            get => _model.IsRitual;
            set
            {
                _model.IsRitual = value;
                RaisePropertyChanged();
            }
        }

        public string ComponentsString
        {
            get
            {
                string finalString = "";
                finalString += IsVocal ? "V," : "";
                finalString += IsSomatic ? "S," : "";
                finalString += IsMaterial ? $"M" : "";
                if (IsMaterial && !string.IsNullOrEmpty(DescriptionMaterials)) finalString += $" ({DescriptionMaterials})";
                if (!IsMaterial && finalString.Length > 0)
                    finalString = finalString.Remove(finalString.Length - 1, 1);
                if (finalString.Length <= 1) return "";
                return finalString;
            }
        }

        public string LevelFormatted
        {
            get
            {
                return string.IsNullOrEmpty(Level) ? "" : (Level == "0" ? "Cantrip" : $"{Spellweaver.Backend.Utils.ToOrdinal(Level)} Level");
            }
        }

        public object Clone()
        {
            return new SpellItemViewModel(GetModel.Clone() as Spell)
            {
                Name = this.Name,
                Level = this.Level,
                School = this.School,
                CastingTime = this.CastingTime,
                Target = this.Target,
                Range = this.Range,
                IsVocal = this.IsVocal,
                IsSomatic = this.IsSomatic,
                IsMaterial = this.IsMaterial,
                IsConcentration = this.IsConcentration,
                IsRitual = this.IsRitual,
                Description = this.Description,
                DescriptionMaterials = this.DescriptionMaterials,
                Duration = this.Duration,
                UpcastDescription = this.UpcastDescription,
                Source = this.Source,
                Classes = this.Classes
            };
        }
    }
}