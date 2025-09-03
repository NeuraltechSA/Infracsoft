
using Microsoft.EntityFrameworkCore;
using SharedKernel.Domain.Contracts;
using SharedKernel.Domain.Entities;
using SharedKernel.Domain.Criteria;
using SharedKernel.Infraestructure.Persistence.EFCore.Services;
using SharedKernel.Infraestructure.Persistence.EFCore.Models;
using SharedKernel.Infraestructure.Persistence.Contracts;

namespace SharedKernel.Infraestructure.Persistence.EFCore.Repositories
{
    /// <summary>
    /// Repositorio base para Entity Framework Core que implementa operaciones CRUD básicas.
    /// Proporciona una implementación genérica que separa las entidades de dominio de los modelos de persistencia.
    /// </summary>
    /// <typeparam name="TEntity">Entidad de dominio</typeparam>
    /// <typeparam name="TId">Tipo del identificador de la entidad</typeparam>
    /// <typeparam name="TModel">Modelo de persistencia de EF Core</typeparam>
    /// <typeparam name="TCriteria">Criterio de búsqueda</typeparam>
    public abstract class BaseEFCoreRepository<TEntity, TId, TModel, TCriteria> : IRepository<TEntity, TId, TCriteria>
        where TEntity : Entity
        where TId : ValueObject<string>
        where TModel : BaseEFCoreModel
        where TCriteria : BaseCriteria
    {
        /// <summary>
        /// Contexto de Entity Framework Core
        /// </summary>
        protected readonly DbContext _context;
        
        /// <summary>
        /// DbSet para operaciones sobre el modelo de persistencia
        /// </summary>
        protected readonly DbSet<TModel> _dbSet;
        
        /// <summary>
        /// Convertidor de criterios de dominio a consultas de EF Core
        /// </summary>
        protected readonly EFCoreCriteriaConverter _efCoreCriteriaConverter;
        
        /// <summary>
        /// Parser para conversión entre entidades de dominio y modelos de persistencia
        /// </summary>
        protected readonly IModelParser<TEntity, TModel> _modelParser;

        /// <summary>
        /// Constructor del repositorio base
        /// </summary>
        /// <param name="context">Contexto de Entity Framework Core</param>
        /// <param name="efCoreCriteriaConverter">Convertidor de criterios</param>
        /// <param name="modelParser">Parser de modelos</param>
        public BaseEFCoreRepository(DbContext context, EFCoreCriteriaConverter efCoreCriteriaConverter, IModelParser<TEntity, TModel> modelParser)
        {
            _context = context;
            _dbSet = _context.Set<TModel>();
            _efCoreCriteriaConverter = efCoreCriteriaConverter;
            _modelParser = modelParser;
        }
        
        /// <summary>
        /// Crea una nueva entidad en la base de datos.
        /// La entidad se agrega al contexto con tracking habilitado para permitir el seguimiento de cambios.
        /// </summary>
        /// <param name="entity">Entidad de dominio a crear</param>
        public async Task Create(TEntity entity){
            await _dbSet.AddAsync(_modelParser.ParseToModel(entity));
        }

        /// <summary>
        /// Actualiza una entidad existente en la base de datos.
        /// La entidad se marca como modificada con tracking habilitado para permitir el seguimiento de cambios.
        /// </summary>
        /// <param name="entity">Entidad de dominio a actualizar</param>
        public Task Update(TEntity entity){
            _dbSet.Update(_modelParser.ParseToModel(entity));
            return Task.CompletedTask;
        }

        /// <summary>
        /// Elimina una entidad de la base de datos.
        /// La entidad se marca para eliminación con tracking habilitado.
        /// </summary>
        /// <param name="entity">Entidad de dominio a eliminar</param>
        public Task Delete(TEntity entity){
            _dbSet.Remove(_modelParser.ParseToModel(entity));
            return Task.CompletedTask;
        }

        /// <summary>
        /// Busca una entidad por su identificador.
        /// Utiliza AsNoTracking() para optimizar rendimiento al no requerir seguimiento de cambios
        /// ya que es una operación de solo lectura.
        /// </summary>
        /// <param name="id">Identificador de la entidad</param>
        /// <returns>Entidad encontrada o null si no existe</returns>
        public async Task<TEntity?> Find(TId id){
            // AsNoTracking() mejora rendimiento y reduce uso de memoria al no rastrear cambios
            var result = await _dbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Id.ToString() == id.Value);
            return result == null ? null : _modelParser.ParseToEntity(result);
        }

        /// <summary>
        /// Busca entidades que cumplan con el criterio especificado.
        /// Utiliza AsNoTracking() para optimizar rendimiento al ser una operación de solo lectura.
        /// </summary>
        /// <param name="criteria">Criterio de búsqueda</param>
        /// <returns>Colección de entidades que cumplen el criterio</returns>
        public async Task<IEnumerable<TEntity>> Find(TCriteria criteria){
            // AsNoTracking() mejora rendimiento al no necesitar seguimiento de cambios para consultas de lectura
            var items = await _efCoreCriteriaConverter.Apply(criteria, _dbSet).AsNoTracking().ToListAsync();
            return items.Select(_modelParser.ParseToEntity);
        }
    }
}
