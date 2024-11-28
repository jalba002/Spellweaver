using System.ComponentModel;

namespace Spellweaver.Other
{
    [System.Serializable]
    public class SortingEntry
    {
        public SortingEntry(string name, SortDescription sortingMethod)
        {
            Name = name;
            SortingMethod = sortingMethod;
        }

        public string Name { get; set; }
        public SortDescription SortingMethod { get; set; }
    }
}
