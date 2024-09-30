using Spellweaver.Backend;
using System.Windows;
using Spellweaver.Data;

namespace Spellweaver.Providers
{
    public class OnlineDatabaseProvider : DNDDatabase
    {
        public override async Task<List<CastingTime>> GetCastingTimesAsync()
        {
            try
            {
                var result = await new CastingTimeDataProvider().GetAllAsync();
                return result.ToList();
            }
            catch (Exception ex) 
            { 
                MessageBox.Show(ex.Message, "Error Boludo");
            }
            return new List<CastingTime>();
        }

        public override async Task<List<Level>> GetLevelsAsync()
        {
            try
            {
                var result = await new LevelDataProvider().GetAllAsync();
                return result.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Boludo");
            }
            return new List<Level>();
        }

        public override async Task<List<School>> GetSchoolsAsync()
        {
            try
            {
                var result = await new SchoolDataProvider().GetAllAsync();
                return result.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Boludo");
            }
            return new List<School>();
        }

        public override async Task<List<Spell>> GetSpellsAsync()
        {
            try
            {
                var result = await new Open5eSpellDataProvider().GetAllAsync();
                return result.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Boludo");
            }
            return new List<Spell>();
        }

        public async Task<List<Spell>> GetALLSpellsAsync()
        {
            try
            {
                var result = await new Open5eSpellDataProvider().GetAllDatabase();
                return result.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Boludo");
            }
            return new List<Spell>();
        }
    }
}