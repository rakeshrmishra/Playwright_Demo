using Utils;
using System.Threading.Tasks;
using Microsoft.Playwright;
using System.Reflection.Emit;
using System.Linq;
using System.IO;
using System;

public static class FandOBookingPage{

static string EnquiryNumber="";
static string FandOEnquiryNumber="";
    
    public static async Task SelectFandOApp(IPage page, string CRMAppName, string AppName)
    {
        await FandOCommon.ClickOnFandOApp(page, CRMAppName, AppName);
    }

    public static async Task FandOApp(IPage page, string CRMAppName, string AppName)
    {
        await FandOCommon.ClickOnFandOsubApp(page, CRMAppName, AppName);
    }
    
    public static async Task SearchBookingNumber(IPage page, string BookingNO)
    {
        await FandOCommon.ApplyFilter(page, BookingNO);
    }

    public static async Task<string> CaptureFandOEnquiryNumber(IPage page, string locator)
    {
             var text = await Utilities.CaptureText(page,locator);
            //  var enqNumber = await page.WaitForSelectorAsync(Helper.GetID(locator));
            //  FandOEnquiryNumber = await enqNumber.InnerTextAsync();
             return text;
            //  BasePlaywrightTest.test.Log(AventStack.ExtentReports.Status.Pass,"Text Captured as : "+FandOEnquiryNumber);
    }

    public static async Task EnquiryNumberValidation(IPage page, string EnquiryNumber, string FandOEnquiryNumber)
    {
             await Utilities.ValidationforequalComparision(page, EnquiryNumber, FandOEnquiryNumber);
    }
}