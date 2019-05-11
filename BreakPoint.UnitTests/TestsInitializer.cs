namespace BreakPoint.UnitTests
{
    public class TestsInitializer
    {
        private static bool testInitialized = false;

        public static void Initizlize()
        {
            if (!testInitialized)
            {
                // TO DO: Initializing of Automaper and other logik here!

                testInitialized = true;
            }
        }
    }
}
