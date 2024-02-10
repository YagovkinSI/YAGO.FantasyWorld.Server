namespace YAGO.FantasyWorld.Server.Domain.Enums
{
    /// <summary>
    /// Статус квеста
    /// </summary>
    public enum QuestResult
    {
        /// <summary>
        /// Неизвестно
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Полный провал
        /// </summary>
        CriticalFail = 1,

        /// <summary>
        /// Провал
        /// </summary>
        Fail = 2,

        /// <summary>
        /// Нейтральный результат
        /// </summary>
        Neitral = 2,

        /// <summary>
        /// Успех
        /// </summary>
        Success = 2,

        /// <summary>
        /// Полный успех
        /// </summary>
        CriticalSuccess = 2,
    }
}
