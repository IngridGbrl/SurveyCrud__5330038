using _5330038.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5330038.Data
{

    public class SurveyDatabase
    {
      
        SQLiteAsyncConnection DataBase;
        public SurveyDatabase()
        {

        }
        //se inicializa DataBase usando Init la cual es una funcion asincronica para inicializar el campo
        async Task Init()
        {
            if (DataBase is not null)
                return;

            DataBase = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);

            var result = await DataBase.CreateTableAsync<Survey>();
        }

        public async Task<List<Survey>> GetTodoItemsAsync()
        {

            await Init();
            return await DataBase.Table<Survey>().ToListAsync();
        }

        public async Task<int> SaveItemAsync(Survey item)
        {

            await Init();
            if(item.Id != 0)
            {
                return await DataBase.UpdateAsync(item);
            }
            else
            {
                //si el item es nuevo entonces se guardará
                return await DataBase.InsertAsync(item);
            }
        }

        public async Task<int> DeleteItemAsync(Survey item)
        {
            //Con este metodo se manda a llamar Init y el item se eliminará de la bd
            await Init();
            return await DataBase.DeleteAsync(item);
        }
    }
}
