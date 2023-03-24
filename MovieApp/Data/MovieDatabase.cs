using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using MovieApp.Models;

namespace MovieApp.Data
{

    class MovieDatabase
    {
        SQLiteAsyncConnection _Database;
        async Task Init()
        {
            if (_Database is not null) return;
            _Database = new SQLiteAsyncConnection( Constants.DatabasePath, Constants.Flags);
            var result = await _Database.CreateTableAsync<Movie>();
        }

        public async Task<List<Movie>> GetMoviesAsync()
        {
            await Init();
            return await _Database.Table<Movie>().ToListAsync();
        }
        public async Task<Movie> GetMovieAsync(int id)
        {
            await Init();
            return await _Database.Table<Movie>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public async Task<int> SaveMoiveAsync(Movie item)
        {
            await Init();
            if (item.Id != 0) return await _Database.UpdateAsync(item);
            else return await _Database.InsertAsync(item);
        }
        public async Task<int> DeleteMovieAsync(Movie item)
        {
            await Init();
            return await _Database.DeleteAsync(item);
        }

    }
}
