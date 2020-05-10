using SDSD.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDSD.Bootstrap
{
    public class SDSDItemDatabase
    {
        static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        });

        static SQLiteAsyncConnection Database => lazyInitializer.Value;
        static bool initialized = false;

        public SDSDItemDatabase()
        {
            InitializeAsync().SafeFireAndForget(false);
        }

        async Task InitializeAsync()
        {
            if (!initialized)
            {
                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(Vessel).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(Vessel)).ConfigureAwait(false);
                    initialized = true;
                }
                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(Crew).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(Crew)).ConfigureAwait(false);
                    initialized = true;
                }
            }
        }
        #region Vessel Data Access Layer
        public Task<List<Vessel>> GetItemsAsync()
        {
            return Database.Table<Vessel>().ToListAsync();
        }

        public Task<List<Vessel>> GetItemsNotDoneAsync()
        {
            // SQL queries are also possible
            return Database.QueryAsync<Vessel>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
        }

        public Task<Vessel> GetItemAsync(int id)
        {
            return Database.Table<Vessel>().Where(i => i.vesselId == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(Vessel item)
        {
            if (item.vesselId > 0)
            {
                return Database.UpdateAsync(item);
            }
            else
            {
                return Database.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(Vessel item)
        {
            return Database.DeleteAsync(item);
        }

        #endregion


        #region Crew Data Access Layer
        public Task<List<Crew>> GetCrewItemsAsync()
        {
            return Database.Table<Crew>().ToListAsync();
        }

        public Task<List<Crew>> GetCrewItemsNotDoneAsync()
        {
            // SQL queries are also possible
            return Database.QueryAsync<Crew>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
        }

        public Task<Crew> GetCrewItemAsync(int id)
        {
            return Database.Table<Crew>().Where(i => i.crewId == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveCrewItemAsync(Crew item)
        {
            if (item.crewId > 0)
            {
                return Database.UpdateAsync(item);
            }
            else
            {
                return Database.InsertAsync(item);
            }
        }

        public Task<int> DeleteCrewItemAsync(Crew item)
        {
            return Database.DeleteAsync(item);
        }

        #endregion
    }
}

