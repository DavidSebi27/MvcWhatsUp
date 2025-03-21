using Azure.Core;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using MvcWhatsUp.Models;
using MvcWhatsUp.Repositories;

public abstract class BaseController : Controller
{
    protected readonly IUsersRepository _usersRepository;

    public BaseController(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        base.OnActionExecuting(context);

        // Set the username in ViewData for every action
        string? userIdStr = Request.Cookies["UserId"];

        if (!string.IsNullOrEmpty(userIdStr) && int.TryParse(userIdStr, out int userId))
        {
            User? user = _usersRepository.GetById(userId);
            if (user != null)
            {
                ViewData["UserName"] = user.UserName;
                ViewData["UserId"] = userId;
            }
        }
    }
}