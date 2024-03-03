namespace YAGO.FantasyWorld.Server.Infrastracture.Identity
{
    internal class KnownError
    {
        public KnownError(string message, int code)
        {
            Message = message;
            Code = code;
        }

        public string Message { get; }
        public int Code { get; }
    }
}
