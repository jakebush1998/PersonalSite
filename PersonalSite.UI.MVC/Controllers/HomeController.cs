using Microsoft.AspNetCore.Mvc;
using PersonalSite.UI.MVC.Models;
using System.Diagnostics;

//Email - step 2
using Microsoft.Extensions.Configuration;

using MimeKit;
using MailKit.Net.Smtp;

namespace PersonalSite.UI.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        //email - step 3
        private readonly IConfiguration _config;

        //email - step 4
        public HomeController(ILogger<HomeController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        public IActionResult Contact()
        {
            return View();

        }

        [HttpPost]
        public IActionResult Contact(ContactViewModel cvm)
        {
            if (!ModelState.IsValid)
            {
                return View(cvm);
            }

            //Variable for the body of the email 
            string message = $"Hello! You have received a new email from your website's contact form! <br />" +
                $"Sender: {cvm.Name}<br />Email: {cvm.Email}<br />Subject: {cvm.Subject}<br />Message: {cvm.Message}<br /><br />" +
                $"<strong> DO NOT REPLY TO THIS EMAIL. Send replies to {cvm.Email}</strong>";

            //Created an instance of MimeMessage that will store all of the email object's info
            var msg = new MimeMessage();

            //Assign credentials to send the email 
            msg.From.Add(new MailboxAddress("Sender", _config.GetValue<string>("Credentials:Email:User")));

            msg.To.Add(new MailboxAddress("Personal", _config.GetValue<string>("Credentials:Email:Recipient")));

            msg.Subject = cvm.Subject;

            msg.Body = new TextPart("HTML") { Text = message };

            msg.Priority = MessagePriority.Urgent;


            //Attempt to send the email 
            using (var client = new SmtpClient())
            {
                //Connect to the mail server 
                client.Connect(_config.GetValue<string>("Credentials:Email:Client"));

                //Authenticate our email user
                client.Authenticate(
                    //Username 
                    _config.GetValue<string>("Credentials:Email:User"),

                    //Password
                    _config.GetValue<string>("Credentials:Email:Password")
                    );

                try
                {
                    client.Send(msg);
                }
                catch (Exception ex)
                {
                    //If for any reason the client.Send(msg) fails, this code will execute and allow us to exit gracefully
                    //without causing a Runtime Exception 
                    ViewBag.ErrorMessage = $"There was an error processing your request. Please try again later.<br />" +
                        $"Error Message: {ex.StackTrace}";

                    return View(cvm);
                }

            }

            //If all goes well, return a View that displays a confirmation of the message being sent 
            return View("EmailConfirmation", cvm);

            //Email - Step 10 
            //Add the EmailConfirmation view
            // - Right click here in the Action -> Add View 
            // - Select Razor View - Empty 
            // - Name: EmailConfirmation 
            // - Add the @model decleration to the View 
            // - Code the view's HTML 

        }


        public IActionResult Index()
        {
            return View();
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}