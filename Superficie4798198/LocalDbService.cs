using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Superficie4798198
{
    public class LocalDbService
    {
        private const string DB_NAME = "demo_triangulo.db3";
        private readonly SQLiteAsyncConnection _connection;

        public LocalDbService()
        {
            _connection = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, DB_NAME));
            //Le indica al sistema qe cree la tabla de nuestro contexto
            _connection.CreateTableAsync<Triángulo>();
        }
        //Método para listar los registros de nuestra tabla

        public async Task<List<Triángulo>> GetResultado()
        {
            return await _connection.Table<Triángulo>().ToListAsync();
        }

        //Método para crear registros 
        public async Task Create(Triángulo triangulo)
        {
            await _connection.InsertAsync(triangulo);
        }
        //Método para actualizar
        public async Task Update(Triángulo triangulo)
        {
            await _connection.UpdateAsync(triangulo);
        }
        //Método para eliminar
        public async Task Delete(Triángulo triangulo)
        {
            await _connection.DeleteAsync(triangulo);
        }
    }
}
