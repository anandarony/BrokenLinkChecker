using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace BrokenLinkChecker
{
    class BrokenLinkChecker
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Please enter the site URL to check for broken links:");
            string siteUrl = Console.ReadLine();

            if (string.IsNullOrEmpty(siteUrl))
            {
                Console.WriteLine("Invalid URL. Please restart the application and provide a valid URL.");
                return;
            }

            ChromeDriverService service = ChromeDriverService.CreateDefaultService();
            service.SuppressInitialDiagnosticInformation = true;

            service.SuppressInitialDiagnosticInformation = true;
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--log-level=3");  // Only show errors
            options.AddExcludedArgument("enable-logging");
            

            // Set up the Selenium WebDriver
            IWebDriver driver = new ChromeDriver(service, options);
            driver.Navigate().GoToUrl(siteUrl);

            Console.WriteLine("Start testing on: "+siteUrl);

            driver.Manage().Window.Maximize();

            // Find all anchor (<a>) tags on the page
            var links = driver.FindElements(By.TagName("a"));
            var linkUrls = new List<string>();

            // Collect all href attributes
            foreach (var link in links)
            {
                var url = link.GetAttribute("href");
                if (!string.IsNullOrEmpty(url) && (url.StartsWith("http") || url.StartsWith("https")))
                {
                    linkUrls.Add(url);
                }
            }

            // Check each link for broken status
            foreach (var url in linkUrls)
            {
                var isBroken = await IsBrokenLink(url);
                Console.WriteLine($"{url} - {(isBroken ? "Broken" : "Working")}");
            }

            // Close the browser
            driver.Quit();
        }

        // Method to check if the URL returns a valid status code
        private static async Task<bool> IsBrokenLink(string url)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.GetAsync(url);
                    return !response.IsSuccessStatusCode;
                }
            }
            catch (Exception)
            {
                return true; // Consider link broken if an exception is thrown
            }
        }
    }
}

