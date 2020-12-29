namespace Tasks.Domain
{
    public static class TasksStartup
    {
        public static string Secret { get; private set; }

        public static void Configure(
            string secret  
        ) {
            Secret = secret;
        }
    }
}
