namespace YAGO.FantasyWorld.Domain.HistoryEvents.Enums
{
    public enum HistoryEventType
    {
        Unknown = 0,

        //1.000.000-1.999.999 Quests 
        //1.000.100-1.000.199 BaseQuestOptionType
        //1.000.110-1.000.119 BaseQuestOptionType - Neitral
        BaseQuest_Neitral_CriticalSuccess = 1000111,
        BaseQuest_Neitral_Success = 1000112,
        BaseQuest_Neitral_Neitral = 1000113,
        BaseQuest_Neitral_Fail = 1000114,
        BaseQuest_Neitral_CriticalFail = 1000115,
        //1.000.120-1.000.129 BaseQuestOptionType - Friendly
        BaseQuest_Friendly_CriticalSuccess = 1000121,
        BaseQuest_Friendly_Success = 1000122,
        BaseQuest_Friendly_Neitral = 1000123,
        BaseQuest_Friendly_Fail = 1000124,
        BaseQuest_Friendly_CriticalFail = 1000125,
        //1.000.130-1.000.139 BaseQuestOptionType - Agressive
        BaseQuest_Agressive_CriticalSuccess = 1000131,
        BaseQuest_Agressive_Success = 1000132,
        BaseQuest_Agressive_Neitral = 1000133,
        BaseQuest_Agressive_Fail = 1000134,
        BaseQuest_Agressive_CriticalFail = 1000135,
    }
}
