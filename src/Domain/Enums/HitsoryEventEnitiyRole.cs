namespace YAGO.FantasyWorld.Server.Domain.Enums
{
    /// <summary>
    /// Роль сущности в историческом событии
    /// </summary>
    public enum HitsoryEventEnitiyRole
    {
        /// <summary>
        /// Неизвестно
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Инициатор события
        /// </summary>
        Initiator = 1,

        /// <summary>
        /// Цель основного действия события
        /// </summary>
        Target = 2,
    }
}
