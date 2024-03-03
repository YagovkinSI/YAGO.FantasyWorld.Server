namespace YAGO.FantasyWorld.Server.Domain.Exceptions
{
    public class NotAuthorizedApplicationException : YagoException
    {
        public NotAuthorizedApplicationException()
            : base("Необходимо авторизоваться.", 401)
        {
        }
    }
}
