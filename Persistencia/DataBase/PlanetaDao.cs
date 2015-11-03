using Persistencia.Models;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.DataBase
{
    public class PlanetaDao
    {

        SQLiteConnection con;

        public PlanetaDao(SQLiteConnection con) {
            this.con = con;
            string sql = "CREATE TABLE IF NOT EXISTS planeta (id INTEGER PRIMARY KEY AUTOINCREMENT, nombre TEXT, gravedad FLOAT)";
            using (var statement =con.Prepare(sql)) {
                statement.Step();
            }
        }

        public void insertPlaneta(Planeta planeta) {
            string sql = "INSERT INTO planeta (nombre, gravedad) VALUES(?,?)";
            using (var statement = con.Prepare(sql)) {
                statement.Bind(1, planeta.Nombre);
                statement.Bind(2, planeta.Gravity);
                statement.Step();
            }

        }

        public void updatePlaneta(Planeta planeta) {
            string sql = "UPDATE planeta SET nombre=?, gravedad = ? WHERE id=?";
            using (var statement = con.Prepare(sql))
            {
                statement.Bind(1, planeta.Nombre);
                statement.Bind(2, planeta.Gravity);
                statement.Bind(3, planeta.Id);
                statement.Step();
            }

        }

        public void deletePlaneta(long id) {
            string sql = "DELETE planeta WHERE id=?";
            using (var statement = con.Prepare(sql))
            {
                statement.Bind(1, id);
                statement.Step();
            }
        }

        public Planeta getById(long id) {
            Planeta planeta = null;

            string sql = "SELECT * FROM planeta WHERE id=?";
            using (var statement = con.Prepare(sql))
            {
                statement.Bind(1, id);
                if (statement.Step()==SQLiteResult.ROW) {
                    planeta = getPlanetaWithStatement(statement);
                }
            }

            return planeta;
        }

        public List<Planeta> getAll() {
            string sql = "SELECT * FROM planeta";
            return getListWithSql(sql);    
        }

        public List<Planeta> getAllByNombre(string nombre) {
            string sql = "SELECT * FROM planeta WHERE nombre LIKE '%"+nombre+"%'";
            return getListWithSql(sql);
        }

        private Planeta getPlanetaWithStatement(ISQLiteStatement statement) {
            Planeta planeta = new Planeta();
            planeta.Id = (long)statement[0];
            planeta.Nombre = (string)statement[1];
            planeta.Gravity = (float)statement[2];
            return planeta;
        }

        private List<Planeta> getListWithSql(string sql) {

            List<Planeta> planetas = new List<Planeta>();
            using (var statement = con.Prepare(sql)) {
                while (statement.Step() == SQLiteResult.ROW) {
                    planetas.Add(getPlanetaWithStatement(statement));
                }
            }

            return planetas;
        }

    }
}

