using Spellweaver.Data;

namespace Spellweaver.Services
{
    public interface IInputOutputHandler<T> where T : ExportableModel
    {
        public T? ImportSingle();
        public List<T>? ImportMultiple();
        public void ExportSingle(T content);
        public void ExportMultiple(List<T> content);
    }
}