using Spellweaver.Data;
using Spellweaver.Interfaces;

namespace Spellweaver.Providers
{
    public class LevelDataProvider : IDataProvider<Level>
    {
        public async Task<IEnumerable<Level>?> GetAllAsync()
        {
            await Task.Delay(10);
            return new List<Level>
            {
                new Level { LevelInt = 0 , LevelString = "Cantrip"},
                new Level { LevelInt = 1 , LevelString = "1st Level"},
                new Level { LevelInt = 2 , LevelString = "2nd Level"},
                new Level { LevelInt = 3 , LevelString = "3rd Level"},
                new Level { LevelInt = 4 , LevelString = "4th Level"},
                new Level { LevelInt = 5 , LevelString = "5th Level"},
                new Level { LevelInt = 6 , LevelString = "6th Level"},
                new Level { LevelInt = 7 , LevelString = "7th Level"},
                new Level { LevelInt = 8 , LevelString = "8th Level"},
                new Level { LevelInt = 9 , LevelString = "9th Level"},
            };
        }
    }
}