using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using System.Diagnostics;
using System.IO;

#pragma warning disable CS8602

namespace TestApiProject.Utilities
{
    public static class ExtentReportManager
    {
        private static ExtentReports _extentReports = new ExtentReports();
        private static ExtentTest _test = null!;
        private static string reportPath = "";

        public static void InitReport()
        {

            string baseDir = "";
            // Combine the base directory with the relative path
            baseDir = AppDomain.CurrentDomain.BaseDirectory;

            // Navigate up to the desired directory
            string projectDir = Directory.GetParent(baseDir).Parent.Parent.Parent.FullName;

            reportPath = Path.Combine(projectDir, "TestResults", "ExtentReport.html");

            var sparkReporter = new ExtentSparkReporter(reportPath);
            _extentReports.AttachReporter(sparkReporter);
           

        }

        public static void CreateTest(string testName)
        {
            _test = _extentReports.CreateTest(testName);
        }

        public static void LogInfo(string message)
        {
            _test.Log(Status.Info, message);
        }

        public static void LogPass(string message)
        {
            _test.Log(Status.Pass, message);
        }

        public static void LogFail(string message)
        {
            _test.Log(Status.Fail, message);
        }

        public static void FlushReport()
        {
            _extentReports.Flush();
        }
    }
}
