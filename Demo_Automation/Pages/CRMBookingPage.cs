using Utils;
using System.Threading.Tasks;
using Microsoft.Playwright;
using System.Reflection.Emit;
using System.Linq;
using System.IO;
using System;

public static class CRMBookingPage{

static string EnquiryNumber="";
    
    public static async Task SelectCRMApp(IPage page, string CRMAppName, string AppName)
    {
        await CRMCommon.ClickOnCRMApp(page, CRMAppName, AppName);
    }

    public static async Task ClickBookingTab(IPage page, string sideMenuName, string sidemenu)
    {
        await CRMCommon.SelectCRMSideMenu(page,sideMenuName,sidemenu);
    }

    public static async Task SearchBookingID(IPage page, string locator, string SearchID, string searchText)
    {
        await CRMCommon.QuickFind(page, locator, SearchID, searchText);
    }

    public static async Task DblClickonlistrecord(IPage page, string locator, string searchsection)
    {
        await CRMCommon.DblClickonSearchrecord(page, locator, searchsection);
    }

    public static async Task<string> CaptureEnquiryNumber(IPage page, string locator)
    {
             var enqNumber = await page.WaitForSelectorAsync(Helper.GetID("LeadEnquiryNumber"));
             EnquiryNumber = await enqNumber.InnerTextAsync();
             return EnquiryNumber;
             BasePlaywrightTest.test.Log(AventStack.ExtentReports.Status.Pass,"Text Captured as : "+EnquiryNumber);
    }
}