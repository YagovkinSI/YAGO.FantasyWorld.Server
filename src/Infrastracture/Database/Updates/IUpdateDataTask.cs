namespace YAGO.FantasyWorld.Server.Infrastracture.Database.Updates
{
    internal interface IUpdateDataTask
    {
        public void Execute(DatabaseContext databaseContext);
    }
}
