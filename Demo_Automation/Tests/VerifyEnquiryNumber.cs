using System.Runtime.CompilerServices;
using System.Reflection.Metadata.Ecma335;
using System.Net;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Utils;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace DemoTests
{
    [TestClass]
    
    public class VerifyEnquiryNumber : BasePlaywrightTest
    {
        
        [ClassInitialize]
        public static  async Task ClassInitialize(TestContext Context)
        {
            Console.WriteLine("Class Initialise started");
            //testContext = Context;
            await ClassInit(Context);
        }
        
        [TestMethod]
        public async Task EnquiryNumber()
        {
            try{
            await VerifyEnquiryNumberFunction.VerifyEnquiry(page,test,CRMUrl,FandOUrl,SalesManagerID,SalesManagerPwd);
            }catch(Exception ex)
            {
                test.Log(AventStack.ExtentReports.Status.Fail,ex.ToString());
                Assert.Fail(ex.ToString());
            }
            await context.Tracing.StopAsync(appConfig.TracingStopOptions);
            await page.PdfAsync(new Microsoft.Playwright.PagePdfOptions { Path = "D:\\Playwright_Demo\\Playwright_Demo\\Demo_Automation\\page.pdf", Format = "Ledger", PageRanges = "1-4" });
        }
    }
}