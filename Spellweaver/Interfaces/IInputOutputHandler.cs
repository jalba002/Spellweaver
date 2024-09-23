using Spellweaver.Backend;

namespace Spellweaver.Interfaces
{
    public interface IInputOutputHandler<T>
    {
        public T? ImportSingle();
        public List<T>? ImportMultiple();
        public void ExportSingle(T content, ExportSettings exportSettings);
        public void ExportMultiple(List<T> content, ExportSettings exportSettings);
    }
}