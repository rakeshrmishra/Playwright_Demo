
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Playwright;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using System;
using System.IO;
using System.Threading.Tasks;




    [TestClass]
    public class Extent 
    {
        public static ExtentReports extent;
        public static ExtentTest test;

        [ClassInitialize]
        public static  void ClassInit(TestContext testContext)
        {
            Console.WriteLine("test");
            extent = new ExtentReports();
            var path = System.Reflection.Assembly.GetCallingAssembly().Location;
            var actualPath = path.Substring(0, path.LastIndexOf("bin"));
            var projectPath = new Uri(actualPath).LocalPath;
            Directory.CreateDirectory(projectPath.ToString() + "Reports");
 
            Console.WriteLine(projectPath.ToString());
            var reportPath = projectPath + "Reports\\Index.html";
            Console.WriteLine(reportPath);
            /* For Version 3 */
            /* var htmlReporter = new ExtentV3HtmlReporter(reportPath); */
            /* s version 4 --> Creates Index.html */
            var htmlReporter = new ExtentHtmlReporter(reportPath);
            
            extent.AttachReporter(htmlReporter);
           // extent.AddSystemInfo("Host Name", "Cloud-based Selenium Grid on LambdaTest");
           // extent.AddSystemInfo("Environment", "Test Environment");
           // extent.AddSystemInfo("UserName", "Himanshu Sheth");
            
        }


       // [TestMethod]
        public  async Task TestMethod1()
        {
            test = extent.CreateTest("TestMethod1");
            test.Log(Status.Pass,"Entering");
             using var playwright = await Playwright.CreateAsync();
        await using var browser = await playwright.Chromium.LaunchAsync();
        var page = await browser.NewPageAsync();
        await page.GotoAsync("https://playwright.dev/dotnet");
        await page.ScreenshotAsync(new PageScreenshotOptions { Path = "screenshot.png" });
            
        }

        [ClassCleanup]
        public static void clean()
        {
            extent.Flush();
        }
    }

