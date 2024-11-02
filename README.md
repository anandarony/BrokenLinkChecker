Explanation of the Code

    Setting up WebDriver: The code initializes a Chrome WebDriver to navigate to the specified site.
    Collecting Links: It collects all <a> tags on the page and extracts their href attributes.
    Checking Link Status: For each link, it sends an HTTP GET request to verify if the link is accessible (status code 200-299 indicates success).
    Output: It reports each link as "Working" or "Broken" based on the response.
    Console Input for URL: The program now prompts the user to enter the site URL via Console.ReadLine().
    URL Validation: If the user enters an empty or invalid URL, it provides feedback and exits the program.

This way, you can enter the URL directly into the console each time you run the application, allowing for greater flexibility in testing multiple websites without changing the code.

Notes

    Dependencies: Ensure System.Net.Http is included for HTTP requests.
    Error Handling: The code handles exceptions for URLs that may be unreachable due to various network issues.
    Limitations: The code does not handle JavaScript-based navigation or single-page applications, which may require more advanced handling.

