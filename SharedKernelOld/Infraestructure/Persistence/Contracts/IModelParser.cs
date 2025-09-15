namespace SharedKernel.Infraestructure.Persistence.Contracts;

public interface IModelParser<TEntity, TModel>
{
    TEntity ParseToEntity(TModel model);
    TModel ParseToModel(TEntity entity);
}