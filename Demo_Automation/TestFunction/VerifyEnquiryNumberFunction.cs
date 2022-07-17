using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.Globalization;
using Microsoft.Playwright;
using System.Threading.Tasks;
using System.Threading;
using Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AventStack.ExtentReports;

public class VerifyEnquiryNumberFunction
{
    public static async Task VerifyEnquiry(IPage page,ExtentTest test,string CRMUrl, string FandOUrl, string login ,string password, string leadType ="LeadPurchaseTimeFrame_Hot")
    {
        await CRMCommon.Login(page, test, CRMUrl, login , password);
        await Utilities.PageNavigation(page, "RE_CRMUrl", "RE Dealership Sales App Page");
        await page.GotoAsync("https://re.crm8.dynamics.com/main.aspx?appid=b9f43191-e22a-e911-a984-000d3af2837b&forceUCI=1&pagetype=dashboard&type=system&_canOverride=true");
        // await CRMBookingPage.SelectCRMApp(page, "REDealershipSalesApp", "Dealership Sales App");
        await CRMBookingPage.ClickBookingTab(page, "CRMBooking","Booking");
        await CRMBookingPage.SearchBookingID(page, "Quickfind", "BKG006121DE00018", "Booking ID");
        await CRMBookingPage.DblClickonlistrecord(page, "ClickOnSearchRecord", "Search record");
        var EnquiryNumber = await CRMBookingPage.CaptureEnquiryNumber(page, "LeadEnquiryNumber");
        await CRMCommon.LogoutFromCRM(page);
        await FandOCommon.FandOLogin(page, test, FandOUrl, login , password);
        await page.GotoAsync("https://reuat01.sandbox.operations.dynamics.com/?cmp=REIN&mi=PwCSalesWorkspace");
        await page.GotoAsync("https://reuat01.sandbox.operations.dynamics.com/?cmp=REIN&mi=PwcActiveBookings");
        // await FandOBookingPage.SelectFandOApp(page, "FandOSalesApp", "FandO Sales App");
        // await FandOBookingPage.SelectFandOApp(page, "FandOActiveBooking", "FandO Active Booking");
        await FandOBookingPage.SearchBookingNumber(page, "BKG006121DE00018");
        var FandOEnquiryNumber = await FandOBookingPage.CaptureFandOEnquiryNumber(page, "FandOEnquiryNumber");
        await FandOBookingPage.EnquiryNumberValidation(page, EnquiryNumber, FandOEnquiryNumber);
        
    }  
}