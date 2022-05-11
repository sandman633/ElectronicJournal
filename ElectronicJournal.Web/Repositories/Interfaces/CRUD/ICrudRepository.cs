namespace ElectronicJournal.Web.Repositories.Interfaces.CRUD
{
    public interface ICrudRepository<TModel> : ICreatable<TModel>, IDeletable, IGettable<TModel>,IUpdatable<TModel>
    {
    }
}
