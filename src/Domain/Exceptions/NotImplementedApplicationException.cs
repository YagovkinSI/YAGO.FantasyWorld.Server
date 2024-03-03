namespace YAGO.FantasyWorld.Server.Domain.Exceptions
{
    public class NotImplementedApplicationException : YagoException
    {
        public NotImplementedApplicationException()
            : base("Данный функционал находится в разработке.", 501)
        {
        }
    }
}
