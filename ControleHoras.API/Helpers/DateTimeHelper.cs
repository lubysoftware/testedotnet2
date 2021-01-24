using System;

namespace ControleHoras.API.Helpers
{
    public static class DateTimeHelper
    {
        public static bool IsEndGreaterThanStart(DateTime start, DateTime end)
        {
            return end > start;
        }
    }
}
