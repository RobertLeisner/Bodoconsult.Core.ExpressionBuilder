using System.IO;

namespace Bodoconsult.Core.ExpressionBuilder.Test.Helpers
{
    public static class TestHelper
    {

        public const string OutputPath = @"D.\TEMP\";


        /// <summary>
        /// Create the output path for test outputs
        /// </summary>
        public static void CreateOutputPath()
        {
            if (!Directory.Exists(OutputPath)) Directory.CreateDirectory(OutputPath);
        }
    }
}