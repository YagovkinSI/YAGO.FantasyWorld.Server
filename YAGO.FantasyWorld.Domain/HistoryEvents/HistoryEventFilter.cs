using System;
using YAGO.FantasyWorld.Domain.Entities;

namespace YAGO.FantasyWorld.Domain.HistoryEvents
{
    public class HistoryEventFilter
    {
        public YagoEntity[] Entities { get; set; }
        public DateTimeOffset DateTimeUtcMin { get; set; }
        public int EventCount { get; set; }
        public int PageNum { get; set; }
    }
}
