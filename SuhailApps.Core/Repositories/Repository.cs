using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DotFramework.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using SuhailApps.Core.Data;
using SuhailApps.Core.Enums;
using SuhailApps.Core.Interfaces;

namespace SuhailApps.Core.Repositories
{
    public class Repository : IRepository
    {

        private readonly ApplicationDbContext _applicationDbContext;
        //private readonly ILogAuditService _logAuditService;

        public Repository(ApplicationDbContext applicationDbContext/*, ILogAuditService logAuditService*/)
        {
            _applicationDbContext = applicationDbContext;
            //_logAuditService = logAuditService;
        }

        public IQueryable<T> GetQuery<T>() where T : class, IModel
        {
            return _applicationDbContext.Set<T>().AsQueryable();
        }

        public IEnumerable<T> GetAll<T>(params Expression<Func<T, object>>[] includeExpressions) where T : class, IModel
        {
            IQueryable<T> query = _applicationDbContext.Set<T>();
            foreach (var include in includeExpressions)
                query = query.Include(include);
            return query.ToList();
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>(params Expression<Func<T, object>>[] includeExpressions) where T : class, IModel
        {
            IQueryable<T> query = _applicationDbContext.Set<T>();
            foreach (var include in includeExpressions)
                query = query.Include(include);
            return await query.ToListAsync();
        }

        public IEnumerable<T> FindAll<T>(Expression<Func<T, bool>> predicate = null, Expression<Func<T, object>> orderBy = null, OrderByDirection? orderDirection = null,
        int? top = null, params Expression<Func<T, object>>[] includeExpressions) where T : class, IModel
        {
            IQueryable<T> query = _applicationDbContext.Set<T>();
            if (predicate != null)
                query = query.Where(predicate);
            if (orderBy != null)
                if (orderDirection == OrderByDirection.Descending)
                    query = query.OrderByDescending(orderBy);
                else
                    query = query.OrderBy(orderBy);
            if (top.HasValue)
                query = query.Take(top.Value);
            foreach (var include in includeExpressions)
                query = query.Include(include);

            return query;
        }

        public IEnumerable<T> FindAll<T>(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeExpressions) where T : class, IModel
        {
            return FindAll<T>(predicate, orderBy: null, orderDirection: null, top: null, includeExpressions: includeExpressions);
        }

        public IEnumerable<T> FindAll<T>(Expression<Func<T, bool>> predicate = null, string orderBy = null, OrderByDirection? orderDirection = null,
        int? pageNumber = null, int? pageSize = null, params Expression<Func<T, object>>[] includeExpressions) where T : class, IModel
        {
            IQueryable<T> query = _applicationDbContext.Set<T>();
            if (predicate != null)
                query = query.Where(predicate);


            if (!string.IsNullOrEmpty(orderBy))
                query = orderDirection == OrderByDirection.Descending
                ? query.OrderBy(orderBy + " descending")
                : query.OrderBy(orderBy);

            if (pageNumber.HasValue && pageSize.HasValue)
                query = query.Skip((pageNumber.Value - 1) * pageSize.Value).Take(pageSize.Value);

            foreach (var include in includeExpressions)
                query = query.Include(include);

            return query;
        }

        public async Task<IEnumerable<T>> FindAllAsync<T>(Expression<Func<T, bool>> predicate = null, Expression<Func<T, object>> orderBy = null, OrderByDirection? orderDirection = null,
        int? top = null, params Expression<Func<T, object>>[] includeExpressions) where T : class, IModel
        {
            IQueryable<T> query = _applicationDbContext.Set<T>();
            if (predicate != null)
                query = query.Where(predicate);
            if (orderBy != null)
                if (orderDirection == OrderByDirection.Descending)
                    query = query.OrderByDescending(orderBy);
                else
                    query = query.OrderBy(orderBy);
            if (top.HasValue)
                query = query.Take(top.Value);
            foreach (var include in includeExpressions)
                query = query.Include(include);

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<T>> FindAllAsync<T>(Expression<Func<T, bool>> predicate = null, Expression<Func<T, object>> orderBy = null, OrderByDirection? orderDirection = null, int? pageNumber = null, int? pageSize = null, params Expression<Func<T, object>>[] includeExpressions) where T : class, IModel
        {
            IQueryable<T> query = _applicationDbContext.Set<T>();
            if (predicate != null)
                query = query.Where(predicate);
            if (orderBy != null)
                if (orderDirection == OrderByDirection.Descending)
                    query = query.OrderByDescending(orderBy);
                else
                    query = query.OrderBy(orderBy);

            if (pageNumber.HasValue && pageSize.HasValue)
                query = query.Skip((pageNumber.Value - 1) * pageSize.Value).Take(pageSize.Value);

            foreach (var include in includeExpressions)
                query = query.Include(include);

            return await query.ToListAsync();
        }


        public async Task<IEnumerable<T>> FindAllAsync<T>(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeExpressions) where T : class, IModel
        {
            return await FindAllAsync<T>(predicate, orderBy: null, orderDirection: null, top: null, includeExpressions: includeExpressions);
        }

        public T Find<T>(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeExpressions) where T : class, IModel
        {
            IQueryable<T> query = _applicationDbContext.Set<T>();
            foreach (var include in includeExpressions)
                query = query.Include(include);
            if (predicate != null)
                return query.FirstOrDefault(predicate);
            return query.FirstOrDefault();
        }

        public async Task<T> FindAsync<T>(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeExpressions) where T : class, IModel
        {
            IQueryable<T> query = _applicationDbContext.Set<T>();

            if (includeExpressions != null)
                foreach (var include in includeExpressions)
                    query = query.Include(include);
            if (predicate != null)
                query = query.Where(predicate);
            return await query.FirstOrDefaultAsync();
        }

        public Task<T> FindAsync<T>(Expression<Func<T, bool>> predicate = null, string[] includeExpressions = null) where T : class, IModel
        {
            IQueryable<T> query = _applicationDbContext.Set<T>();

            if (includeExpressions != null)
                foreach (var include in includeExpressions)
                    query = query.Include(include);
            if (predicate != null)
                query = query.Where(predicate);
            return query.FirstOrDefaultAsync();
        }



        public async Task<T> FindByFuncAsync<T>(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IQueryable<T>> func = null) where T : class, IModel
        {
            IQueryable<T> query = _applicationDbContext.Set<T>();
            //IQueryable<T> resultWithEagerLoading;
            if (func != null) //resultWithEagerLoading
                query = func(query);

            if (predicate != null)
                return await query.FirstOrDefaultAsync(predicate);

            return await query.FirstOrDefaultAsync();
        }

        public T FindByFunc<T>(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IQueryable<T>> func = null) where T : class, IModel
        {
            IQueryable<T> query = _applicationDbContext.Set<T>();

            if (func != null) //resultWithEagerLoading
                query = func(query);

            if (predicate != null)
                return query.FirstOrDefault(predicate);

            return query.FirstOrDefault();
        }

      
        public dynamic[] FindAllByCoulmns<T>(string columns, string predicate = null, object[] predicteValues = null,
        string orderBy = null, OrderByDirection? orderDirection = null, int? pageNumber = null, int? pageSize = null,
        string[] includeExpressions = null) where T : class, IModel
        {
            IQueryable<T> query = _applicationDbContext.Set<T>().AsQueryable();

            if (!string.IsNullOrEmpty(predicate) && predicteValues != null)
                query = query.Where(predicate, predicteValues);

            if (includeExpressions != null)
            {
                query = includeExpressions.Aggregate(query, (current, str) => current.Include(str));
            }

            var select = query.Select(string.Format("new ({0})", columns));

            if (!string.IsNullOrEmpty(orderBy))
                select = orderDirection == OrderByDirection.Descending
                ? select.OrderBy(orderBy + " descending")
                : select.OrderBy(orderBy);

            if (pageNumber.HasValue && pageSize.HasValue)
                select = select.Skip((pageNumber.Value - 1) * pageSize.Value).Take(pageSize.Value);


            if (string.IsNullOrEmpty(columns) == false) return select.ToDynamicArray();
            return select.ToDynamicArray();
        }


        public int GetCount<T>(string predicate = null, object[] predicteValues = null, string[] includeExpressions = null) where T : class, IModel
        {
            IQueryable<T> query = _applicationDbContext.Set<T>();

            if (includeExpressions != null)
            {
                query = includeExpressions.Aggregate(query, (current, str) => current.Include(str));
            }
            if (!string.IsNullOrEmpty(predicate) && predicteValues != null)
                return query.AsQueryable().Count(predicate, predicteValues);
            return query.AsQueryable().Count();
        }

        public T Add<T>(T entity) where T : class, IModel
        {
            return _applicationDbContext.Set<T>().Add(entity).Entity;
        }

        public void AddRange<T>(List<T> entities) where T : class, IModel
        {
            _applicationDbContext.Set<T>().AddRange(entities);
        }

        public T Update<T>(T entity) where T : class, IModel
        {
            return _applicationDbContext.Update(entity).Entity;
        }

        public void UpdateRange<T>(List<T> entities) where T : class, IModel
        {
            _applicationDbContext.UpdateRange(entities);
        }

        public void Delete<T>(T entity) where T : class, IModel
        {
            _applicationDbContext.Set<T>().Remove(entity);
        }

        public void DeleteRange<T>(List<T> entities) where T : class, IModel
        {
            _applicationDbContext.Set<T>().RemoveRange(entities);
        }

    

     
        public IDbContextTransaction BeginTransaction()
        {
            return _applicationDbContext.Database.BeginTransaction();
        }

        public EntityState State<T>(T entity) where T : class, IModel
        {
            return _applicationDbContext.Entry(entity).State;
        }

        public void SetState<T>(T entity, EntityState newState) where T : class, IModel
        {
            _applicationDbContext.Entry(entity).State = newState;
        }

        public bool CanSave()
        {
            return _applicationDbContext.ChangeTracker.Entries().Any(e => e.State != EntityState.Unchanged);
        }

        public List<T> FindAllByFunc<T>(Expression<Func<T, bool>> predicate = null, Expression<Func<T, object>> orderBy = null, OrderByDirection? orderDirection = null,
        int? pageNumber = null, int? pageSize = null, Func<IQueryable<T>, IQueryable<T>> func = null) where T : class, IModel
        {
            IQueryable<T> query = _applicationDbContext.Set<T>();

            if (func != null)
                query = func(query);

            if (predicate != null)
                query = query.Where(predicate);

            if (orderBy != null)
            {
                if (orderDirection == OrderByDirection.Descending)
                    query = query.OrderByDescending(orderBy);
                else
                    query = query.OrderBy(orderBy);
            }

            if (pageNumber.HasValue && pageSize.HasValue)
                query = query.Skip((pageNumber.Value - 1) * pageSize.Value).Take(pageSize.Value);

            return query.ToList();
        }

        public dynamic[] FindAllByColumnsByFunc<T>(string columns, Expression<Func<T, bool>> predicate = null, string orderBy = null,
        OrderByDirection? orderDirection = null, int? pageNumber = null, int? pageSize = null,
        string[] includeExpressions = null) where T : class, IModel
        {
            IQueryable<T> query = _applicationDbContext.Set<T>().AsQueryable();

            if (predicate != null)
                query = query.Where(predicate);


            if (includeExpressions != null)
            {
                query = includeExpressions.Aggregate(query, (current, str) => current.Include(str));
            }

            var select = query.Select(string.Format("new ({0})", columns));

            if (!string.IsNullOrEmpty(orderBy))
            {
                try
                {
                    select = orderDirection == OrderByDirection.Descending
                    ? select.OrderBy(orderBy + " descending")
                    : select.OrderBy(orderBy);

                }
                catch (Exception ex)
                {
                    //
                }
            }


            if (pageNumber.HasValue && pageSize.HasValue)
                select = select.Skip((pageNumber.Value - 1) * pageSize.Value).Take(pageSize.Value);


            if (string.IsNullOrEmpty(columns) == false) return select.ToDynamicArray();
            return select.ToDynamicArray();
        }

        public async Task<int> SaveChangesAsync(bool logAudits = false)
        {
            Guid guid = Guid.NewGuid();
            try
            {

                int n = await _applicationDbContext.SaveChangesAsync();
                return n;

            }
            catch (Exception e)
            {

                throw;
            }
        }




    }
}