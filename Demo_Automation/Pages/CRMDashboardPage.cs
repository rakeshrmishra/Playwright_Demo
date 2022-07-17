using Utils;
using System.Threading.Tasks;
using Microsoft.Playwright;
using System.Reflection.Emit;
using System.Linq;
using System.IO;
using System;

public static class CRMDashboardPage{

    public static async Task ClickOnEnquiries(IPage page)
    {
        await page.RunAndWaitForNavigationAsync(async () => {
        await Utilities.PerformClickAction(page, "DashBoardEnquiryButton", "DashBoard Enquiry Button");
        });
        await page.RunAndWaitForNavigationAsync(async () => {
        await Utilities.PerformClickAction(page, "DashBoardNewEnquiryButton", "DashBoard New Enquiry Button");
        });
    }
}