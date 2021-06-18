using Microsoft.EntityFrameworkCore;
using NejinhoWebApp.Model.API;
using NejinhoWebApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NejinhoWebApp.Controls
{
    public class DataTableRequestControl
    {
        private readonly NejinhoDbContext _context;
        public DataTableRequestControl(NejinhoDbContext context)
        {
            _context = context;
        }
        public static string CriaJsonResultado<T>(IEnumerable<T> list, int iDisplayStart, int iDisplayLength, string sEcho)
        {
            if (list != null)
            {
                var totalRecords = list.Count();
                var displayResult = list.Skip(iDisplayStart).Take(iDisplayLength).ToList();

                object objeto = new
                {
                    sEcho,
                    iTotalRecords = totalRecords,
                    iTotalDisplayRecords = totalRecords,
                    aaData = displayResult
                };
                string json = JsonConvert.SerializeObject(objeto, Formatting.None,
                    new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });

                return json;
            }
            return null;
        }
        public static string CriaJsonResultadoCriaJsonResultado<T>(IQueryable<T> list, int iDisplayStart, int iDisplayLength, string sEcho)
        {
            if (list != null)
            {
                var totalRecords = list.Count();
                var displayResult = list.Skip(iDisplayStart).Take(iDisplayLength).ToList();

                object objeto = new
                {
                    sEcho,
                    iTotalRecords = totalRecords,
                    iTotalDisplayRecords = totalRecords,
                    aaData = displayResult
                };
                string json = JsonConvert.SerializeObject(objeto, Formatting.None,
                    new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });

                return json;
            }
            return null;
        }
        public static void OrderResultado<T, R>(ref IEnumerable<T> list, string dir, Func<T, R> orderFunc)
        {
            if (list != null)
            {
                list = dir == "asc" ? list.OrderBy(orderFunc) : list.OrderByDescending(orderFunc);
            }
        }
        public static void OrderResultado<T, R>(ref IQueryable<T> list, string dir, Func<T, R> orderFunc)
        {
            if (list != null)
            {
                list = (dir == "asc" ? list.OrderBy(orderFunc).AsQueryable() : list.OrderByDescending(orderFunc).AsQueryable());
            }
        }
        

        /// <summary>
        /// Retorna um json para ser consumido pelo JqueryDataTable
        /// </summary>
        /// <typeparam name="T">Classe do Objeto / Model</typeparam>
        /// <param name="param">JqueryDatatableParam / parâmetros da tabela </param>
        /// <param name="entity"> String com o nome da tabela no banco de dados </param>
        /// <param name="columnsSearch">Nome da colunas que podem ser pesquisadas</param>
        /// <param name="columnOrder">Nome da coluna que vai ser ordenada. Asc </param>
        /// <returns></returns>
        /// 
        public Task<string> Recupera<T>(JqueryDatatableParam param, string entity, string[] columnsSearch, string columnOrder)
        {
            bool search(T elem)
            {
                bool result = false;

                foreach (string col in columnsSearch)
                {
                    var value = typeof(T).GetProperty(col).GetValue(elem);

                    if (value == null) continue;

                    result = result ||
                        value
                        .ToString()
                        .ToLower()
                        .Contains(param.sSearch.ToLower());
                }

                return result;
            }

            IEnumerable<T> enumerable = (IEnumerable<T>)typeof(NejinhoDbContext).GetProperty(entity).GetValue(_context);
            enumerable = enumerable.ToList();

            if (!string.IsNullOrEmpty(param.sSearch))
            {
                enumerable = enumerable.Where(search);
            }

            OrderResultado(ref enumerable, param.sSortDir_0, e => typeof(T).GetProperty(columnOrder).GetValue(e));

            string json = CriaJsonResultado(enumerable, param.iDisplayStart, param.iDisplayLength, param.sEcho);
            return Task.FromResult(json);
        }

        /// <summary>
        /// Retorna um json para ser consumido pelo JqueryDataTable
        /// </summary>
        /// <typeparam name="T">Classe do Objeto / Model</typeparam>
        /// <param name="param">JqueryDatatableParam / parâmetros da tabela </param>
        /// <param name="entity"> String com o nome da tabela no banco de dados </param>
        /// <param name="columnsSearch">Nome da colunas que podem ser pesquisadas</param>
        /// <param name="columnOrder">Nome da coluna que vai ser ordenada. Asc </param>
        /// <param name="preSearch">Função de busca intermediaria</param>
        /// <returns></returns>
        /// 
        public Task<string> Recupera<T>(JqueryDatatableParam param, string entity, string[] columnsSearch, string columnOrder, Func<T, bool> preSearch)
        {
            bool search(T elem)
            {
                bool result = false;

                foreach (string col in columnsSearch)
                {
                    var value = typeof(T).GetProperty(col).GetValue(elem);

                    if (value == null) continue;

                    result = result ||
                        value
                        .ToString()
                        .ToLower()
                        .Contains(param.sSearch.ToLower());
                }
                return result;
            }

            IEnumerable<T> enumerable = (IEnumerable<T>)typeof(NejinhoDbContext).GetProperty(entity).GetValue(_context);
            enumerable = enumerable.ToList();
            // Pesquisa costumizada
            enumerable = enumerable.Where(preSearch);

            if (!string.IsNullOrEmpty(param.sSearch))
            {
                enumerable = enumerable.Where(search);
            }

            OrderResultado(ref enumerable, param.sSortDir_0, e => typeof(T).GetProperty(columnOrder).GetValue(e));

            string json = CriaJsonResultado(enumerable, param.iDisplayStart, param.iDisplayLength, param.sEcho);
            return Task.FromResult(json);
        }

       

        
        /// <summary>
        /// Retorna um json para ser consumido pelo JqueryDataTable
        /// </summary>
        /// <typeparam name="T">Classe do Objeto / Model</typeparam>
        /// <param name="param">JqueryDatatableParam / parâmetros da tabela </param>
        /// <param name="customQuery"> Query personalizada para retornar o objeto em questão </param>
        /// <param name="columns">Nome das Colunas</param>
        /// <param name="columnsSearch">Nome da colunas que podem ser pesquisadas</param>
        /// <returns></returns>
        /// 
        public static Task<string> Serialize<T>(JqueryDatatableParam param, IQueryable<T> customQuery, string[] columns, bool[] columnsSearch)
        {
            bool search<T>(T elem)
            {
                bool result = false;
                int i = 0;

                foreach (bool col in columnsSearch)
                {
                    if (col)
                    {
                        var value = typeof(T).GetProperty(columns[i]).GetValue(elem);

                        if (value == null) continue;

                        result = result ||
                            value.ToString()
                            .ToLower()
                            .Contains(param.sSearch.ToLower());
                    }

                    ++i;
                }

                return result;
            }

            if (!string.IsNullOrEmpty(param.sSearch))
            {
                customQuery = customQuery.Where(search).AsQueryable();
            }

            string columnOrder = columns[param.iSortCol_0];
            OrderResultado(ref customQuery, param.sSortDir_0, e => typeof(T).GetProperty(columnOrder).GetValue(e));
            string json = CriaJsonResultado(customQuery, param.iDisplayStart, param.iDisplayLength, param.sEcho);
            return Task.FromResult(json);
        }
    }
}
