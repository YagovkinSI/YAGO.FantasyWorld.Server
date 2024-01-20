namespace YAGO.FantasyWorld.Server.Domain.Exceptions
{
    public class NotAuthorizedApplicationException : ApplicationException
    {
        public NotAuthorizedApplicationException()
            : base("Необходимо авторизоваться.", 401)
        {
        }
    }
}
