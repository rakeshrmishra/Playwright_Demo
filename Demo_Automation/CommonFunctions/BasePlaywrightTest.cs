using System.Configuration.Assemblies;
using System.ComponentModel.Design.Serialization;
using System.Diagnostics.Tracing;
using System.Net.Http;
using System.Reflection;
using System.Runtime;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.IO.Pipes;
using System.ComponentModel;
using System.Transactions;
using System.Security.Cryptography;
using System.Reflection.Emit;
using System.Linq;
using System.Globalization;
using Microsoft.VisualBasic.CompilerServices;
using System.Threading;
using System.Net;
using System.IO;
using Utils;
using Microsoft.Playwright;
using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using System.Text.Json;
using System.Diagnostics;
[TestClass]
public class BasePlaywrightTest{
 
    public static ExtentReports extent;
    public static ExtentTest test;
    public static IPage page;
    public static IBrowser browser;
    public static IBrowserContext context;
    public static DEMOConfig appConfig;
    public static string CRMUrl;

    public static string FandOUrl;
    public static string SalesManagerID;
    public static string SalesManagerPwd;
 
    [AssemblyInitialize]
    public static void  TestSuiteInit(TestContext testContext)
    {
      Console.WriteLine("Starting TestSuite");
      killAllChromeProcess();
      LoadConfig();      
      StartExtentReports();
      // InitAssemblyParams(testContext);

    }


  static void killAllChromeProcess()
  {
    Process[] ps = Process.GetProcessesByName("chrome");
    foreach (Process p in ps)
    p.Kill();
  }
  public static TestContext ClassTextContext;
    // private static void InitAssemblyParams(TestContext testContext)
    // {
    //   Console.WriteLine("Initialising Extent Reports of BasePage");
    //   // BasePage.extent = extent;
    // }

    private static void LoadConfig()
    {
        //Read Config file to get BrowserTypeLaunchOptions,BrowserNewContextOptions,Tracing Options.
        String s = File.ReadAllText("playwright.config");
        appConfig = JsonSerializer.Deserialize<DEMOConfig>(s,new JsonSerializerOptions 
        {
            PropertyNameCaseInsensitive = true
        } );
    }

    private static void CloseChrome()
    {
      Process [] chromeInstances = Process.GetProcessesByName("chrome");

      foreach(Process p in chromeInstances)
      p.Kill();
    }

    private static void StartExtentReports()
    {
      extent = new ExtentReports();
    var path = System.Reflection.Assembly.GetCallingAssembly().Location;

    var actualPath = path.Substring(0, path.LastIndexOf("bin"));
    var projectPath = new Uri(actualPath).LocalPath;
    Directory.CreateDirectory(projectPath.ToString() + "Reports");
    String timeStamp =  DateTime.Now.ToLongTimeString();
    reportPath = projectPath + "Reports\\index.html";  
    var htmlReporter = new ExtentHtmlReporter(reportPath);
            
    extent.AttachReporter(htmlReporter);
    }
static string reportPath ;
    [AssemblyCleanup]
    public static void TestSuiteEnd()
    {
        extent.Flush();//
        String timeStamp =  DateTime.Now.ToString("hhmmss_ddMMMyyyy");
        
        string newReportName = reportPath.Replace("index","TestReport_"+timeStamp);
        File.WriteAllText("text.txt",newReportName);
        File.Move(reportPath,newReportName);
        
    }

    

    private TestContext testContextInstance;
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }
   
    [TestInitialize]
    public void TestInitialise()
    {
      string testname = TestContext.TestName;
      test = extent.CreateTest(TestContext.TestName);
      test.Debug("Starting Test :::" + TestContext.TestName);
      TestContext.WriteLine("Starting test:::"+TestContext.TestName);

       context.ClearCookiesAsync();
    }

    
    public  static async Task ClassInit(TestContext testContext)
    {
    Console.WriteLine("Starting ClassInitialise");
    File.WriteAllText("ClassInit.txt","TestInit getting called");
    ClassTextContext =testContext;
    var driver = await Playwright.CreateAsync();
    
    /*await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions {
      Headless = false,
    });*/
   
    browser = await driver.Chromium.LaunchAsync(appConfig.BrowserOptions);
    // browser = await driver.Firefox.LaunchAsync(appConfig.BrowserOptions);

    context = await browser.NewContextAsync(appConfig.BrowserContextOptions);

    //  await context.Tracing.StartAsync(appConfig.TracingStartOptions);
    await context.Tracing.StartAsync(new TracingStartOptions {
      Screenshots = true,
        Snapshots = true
    });
     page = await context.NewPageAsync();
     //await page.SetViewportSizeAsync(1920,1080);
     SalesManagerID = testContext.Properties["SalesManagerID"].ToString();
     SalesManagerPwd =  testContext.Properties["SalesManagerPwd"].ToString();
  }

    [TestCleanup]
    public static async Task TestCleanup(TestContext testContext)
    {
    /*await context.Tracing.StopAsync(new TracingStopOptions {
      Path = "trace.zip"
    });*/
    await context.Tracing.StopAsync(appConfig.TracingStopOptions);
    await page.PdfAsync(new Microsoft.Playwright.PagePdfOptions { Path = "D:\\Playwright_Demo\\Playwright_Demo\\Demo_Automation\\page.pdf", Format = "Ledger" });
    Console.WriteLine("Test Completed successfully");
    await browser.CloseAsync();
    await context.ClearCookiesAsync();
    if(testContext.CurrentTestOutcome == UnitTestOutcome.Failed)

    test.Log(Status.Fail,"test completed");
    else 
    test.Log(Status.Pass,"test completed");
    }

    public static async Task<IPage> CreateNewPage()
    {
      return await context.NewPageAsync();
    }

}