namespace YAGO.FantasyWorld.Domain.Exceptions
{
    public class YagoNotImplementedException : YagoException
    {
        public YagoNotImplementedException()
            : base("Данный функционал находится в разработке.", 501)
        {
        }
    }
}
