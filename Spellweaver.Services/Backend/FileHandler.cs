namespace Spellweaver.Services
{
    public static class FileHandler
    {
        public static string? ReadFile(string path)
        {
            if (File.Exists(path))
            {
                return File.ReadAllText(path);
            }
            return null;
        }

        public static string? ReadFile(Stream fileStream)
        {
            using (StreamReader reader = new StreamReader(fileStream))
            {
                var result = reader.ReadToEnd();
                if (string.IsNullOrEmpty(result))
                {
                    return null;
                }
                return result;
            }
        }

        public static void WriteFile(string filePath, string value)
        {
            StreamWriter stream = new StreamWriter(filePath);
            stream.Write(value);
            stream.Close();
        }
    }
}
