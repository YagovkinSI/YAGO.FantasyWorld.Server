namespace YAGO.FantasyWorld.Domain.Exceptions
{
    public class YagoNotAuthorizedException : YagoException
    {
        public YagoNotAuthorizedException()
            : base("Необходимо авторизоваться.", 401)
        {
        }
    }
}
