using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage;
using SuhailApps.Core.Enums;

namespace SuhailApps.Core.Interfaces
{
    public interface IRepository
    {

        IQueryable<T> GetQuery<T>() where T : class,IModel;
        IEnumerable<T> GetAll<T>(params Expression<Func<T, object>>[] includeExpressions) where T : class, IModel;
        Task<IEnumerable<T>> GetAllAsync<T>(params Expression<Func<T, object>>[] includeExpressions) where T : class, IModel;
        IEnumerable<T> FindAll<T>(Expression<Func<T, bool>> predicate = null, Expression<Func<T, object>> orderBy = null, OrderByDirection? orderDirection = null, int? top = null, params Expression<Func<T, object>>[] includeExpressions) where T : class, IModel;
        IEnumerable<T> FindAll<T>(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeExpressions) where T : class, IModel;
        IEnumerable<T> FindAll<T>(Expression<Func<T, bool>> predicate = null, string orderBy = null, OrderByDirection? orderDirection = null, int? pageNumber = null, int? pageSize = null, params Expression<Func<T, object>>[] includeExpressions) where T : class, IModel;
        Task<IEnumerable<T>> FindAllAsync<T>(Expression<Func<T, bool>> predicate = null, Expression<Func<T, object>> orderBy = null, OrderByDirection? orderDirection = null, int? top = null, params Expression<Func<T, object>>[] includeExpressions) where T : class, IModel;
        Task<IEnumerable<T>> FindAllAsync<T>(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeExpressions) where T : class, IModel;
        T Find<T>(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeExpressions) where T : class, IModel;
        Task<T> FindAsync<T>(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeExpressions) where T : class, IModel;
        Task<T> FindAsync<T>(Expression<Func<T, bool>> predicate = null, string[] includeExpressions = null) where T : class, IModel;
        Task<T> FindByFuncAsync<T>(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IQueryable<T>> func = null) where T : class, IModel;

        T FindByFunc<T>(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IQueryable<T>> func = null)
        where T : class, IModel;


        dynamic[] FindAllByCoulmns<T>(string columns, string predicate = null, object[] predicteValues = null, string orderBy = null,
        OrderByDirection? orderDirection = null,
        int? pageNumber = null, int? pageSize = null,
        string[] includeExpressions = null) where T : class, IModel;

        int GetCount<T>(string predicate = null, object[] predicteValues = null, string[] includeExpressions = null) where T : class, IModel;
        T Add<T>(T entity) where T : class, IModel;
        void AddRange<T>(List<T> entities) where T : class, IModel;
        T Update<T>(T entity) where T : class, IModel;
        void UpdateRange<T>(List<T> entities) where T : class, IModel;
        void Delete<T>(T entity) where T : class, IModel;
        void DeleteRange<T>(List<T> entities) where T : class, IModel;

        IDbContextTransaction BeginTransaction();
        EntityState State<T>(T entity) where T : class, IModel;
        void SetState<T>(T entity, EntityState newState) where T : class, IModel;
        bool CanSave();
        List<T> FindAllByFunc<T>(Expression<Func<T, bool>> predicate = null, Expression<Func<T, object>> orderBy = null, OrderByDirection? orderDirection = null, int? pageNumber = null, int? pageSize = null, Func<IQueryable<T>, IQueryable<T>> func = null) where T : class, IModel;
        dynamic[] FindAllByColumnsByFunc<T>(string columns, Expression<Func<T, bool>> predicate = null, string orderBy = null, OrderByDirection? orderDirection = null, int? pageNumber = null, int? pageSize = null, string[] includeExpressions = null) where T : class, IModel;

        Task<int> SaveChangesAsync(bool logAudits = false);

    }
}