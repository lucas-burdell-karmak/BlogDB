using System;

namespace The_Intern_MVC.Models
{
    public class ErrorPageModel
    {
        public string Title {get; set;}
        public string Message {get; set;}

        public ErrorPageModel()
        {
            Title = "Error Occured";
            Message = "An error has occured.";
        }

        public ErrorPageModel(string ErrorTitle, string ErrorMessage) {
            this.Title = ErrorTitle;
            this.Message = ErrorMessage;
        }
    }
}