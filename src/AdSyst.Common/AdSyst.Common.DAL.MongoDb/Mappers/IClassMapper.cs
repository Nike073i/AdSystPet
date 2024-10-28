namespace AdSyst.Common.DAL.MongoDb.Mappers
{
    public interface IClassMapper<TClassMapper>
        where TClassMapper : new()
    {
        void Register();
    }
}
