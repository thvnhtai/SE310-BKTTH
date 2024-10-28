using Microsoft.AspNetCore.Mvc;

namespace SE310_BKTTH.Controllers;

public class AuthenticationController : Controller
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<AuthenticationController> _logger;

    public AuthenticationController(HttpClient httpClient, ILogger<AuthenticationController> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }
    
    [HttpPost]
    public async Task<IActionResult> SignIn(string username, string password)
    {
        // Prepare login data from form input
        var loginData = new
        {
            Username = username,
            Password = password
        };

        // Call the authentication API
        var response = await _httpClient.PostAsJsonAsync("http://localhost:5017/api/v1/Auth/login", loginData);
        // Log the response status and content
        _logger.LogInformation("SignIn API Response: {StatusCode}", response.StatusCode);
        var content = await response.Content.ReadAsStringAsync();
        _logger.LogInformation("Response Content: {Content}", content);

        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("Index", "Home");
        }

        // Handle login failure
        ModelState.AddModelError(string.Empty, "Login failed. Please check your credentials.");
        return View();
    }

    public IActionResult SignIn()
    {
        return View();
    }
 
    [HttpPost]
    public async Task<IActionResult> SignUp(string username, string email, string password, string confirmPassword)
    {
        // Validate passwords match
        if (password != confirmPassword)
        {
            ModelState.AddModelError(string.Empty, "Passwords do not match.");
            return View();
        }

        // Prepare the registration data from form input
        var registerData = new
        {
            Username = username,
            Password = password,
            Email = email,
            Role = "User"  // Optional: Customize role as needed
        };

        // Send registration request to the API
        var response = await _httpClient.PostAsJsonAsync("http://localhost:5017/api/v1/Auth/register", registerData);

        if (response.IsSuccessStatusCode)
        {
            // Redirect to login page on successful registration
            return RedirectToAction("SignIn");
        }
    
        // Handle registration failure
        ModelState.AddModelError(string.Empty, "Registration failed. Please try again.");
        return View();
    }

    public IActionResult SignUp()
    {
        return View();
    }

}
