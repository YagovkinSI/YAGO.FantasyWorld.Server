namespace YAGO.FantasyWorld.Server.Domain.Enums
{
    /// <summary>
    /// Типы результатов квеста
    /// </summary>
    public enum QuestOptionResultType
    {
        /// <summary>
        /// Неизвестный тип
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Критический успех
        /// </summary>
        CriticalSuccess = 1,

        /// <summary>
        /// Успех
        /// </summary>
        Success = 2,

        /// <summary>
        /// Нейтральный результат
        /// </summary>
        Neitral = 3,

        /// <summary>
        /// Неудача
        /// </summary>
        Fail = 4,

        /// <summary>
        /// Критическая неудача
        /// </summary>
        CriticalFail = 5
    }
}
