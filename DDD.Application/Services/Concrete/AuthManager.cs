using System.Security.Claims;
using DDD.Application.Dtos.AuthDtos;
using DDD.Application.Services.Abstract;
using DDD.Application.ViewModels.RoleVM;
using DDD.Domain.Entities;
using DDD.Domain.Entities.EntityFramework.AppUser;
using DDD.Infrastructure.EntityFramework.Context.Mssql;
using DDD.Shared.Helpers;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MimeKit;

namespace DDD.Application.Services.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        readonly IHttpContextAccessor _httpContextAccessor;
        readonly ApplicationDbContext _context;
        public AuthManager(RoleManager<Role> roleManager, UserManager<User> userManager, SignInManager<User> signInManager, IHttpContextAccessor httpContextAccessor, ApplicationDbContext context)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }
        public async Task<bool> LoginAsync(LoginDto login)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(login.Email);

                if (user == null)
                    throw new ArgumentNullException(nameof(user), "User was null");

                if (user.IsDeleted == false)
                {
                    _httpContextAccessor.HttpContext.Session.SetString("userId", user.Id);
                    var result = await _signInManager.PasswordSignInAsync(user, login.Password, false, true);
                    if (result.Succeeded)
                    {
                        var userSession = new UserSession
                        {
                            UserId = user.Id,
                            Username = user.UserName,
                            LoginDate = DateTime.Now.ToLocalTime(),
                            IsOnline = true
                        };
                        if (userSession != null)
                        {
                            _context.UserSessions.Add(userSession);
                            await _context.SaveChangesAsync();

                            if (string.IsNullOrEmpty(login.ReturnUrl))
                                return true;
                            return false;
                        }
                        return false;
                    }
                    throw new Exception("Login was not successfull.");
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> RegisterAsync(RegisterDto model)
        {
            try
            {
                if (model == null)
                    throw new ArgumentNullException(nameof(model), "model was null");
               
                var nameSurnameParts = model.NameSurname.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (nameSurnameParts.Length < 2)
                {
                    throw new Exception("Please type full name. (For Example: Name Surname).");
                }

                var firstName = ConvertTurkishToEnglishHelper.ConvertTurkishToEnglish(nameSurnameParts[0]);
                var lastName = ConvertTurkishToEnglishHelper.ConvertTurkishToEnglish(nameSurnameParts[nameSurnameParts.Length - 1]);
                var baseUsername = $"{firstName}{lastName}";
                var username = $"User_{baseUsername}";

                int suffix = 1;
                while (await _userManager.FindByNameAsync(username) != null)
                {
                    username = $"User_{baseUsername}{suffix}";
                    suffix++;
                }

                Random random = new Random();
                int code;
                code = random.Next(100000, 1000000);

                var user = new User
                {
                    NameSurname = model.NameSurname,
                    UserName = username,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    ConfirmCode = code,
                    CreatedDate = DateTime.UtcNow
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                //await _userManager.AddToRoleAsync(user, "User");
                //_httpContextAccessor.HttpContext.Session.SetString("email", user.Email);

                if (result.Succeeded)
                {
                    MimeMessage mimeMessage = new MimeMessage();
                    MailboxAddress mailboxAddressFrom = new MailboxAddress("DDD", "registermail.activation@gmail.com");
                    MailboxAddress mailboxAddressTo = new MailboxAddress(user.UserName, user.Email);

                    mimeMessage.From.Add(mailboxAddressFrom);
                    mimeMessage.To.Add(mailboxAddressTo);

                    var bodyBuilder = new BodyBuilder();
                    bodyBuilder.TextBody = "Here your register activation code. Please do not share this code  with someone. This code sent you for you register to system. : " + code;
                    mimeMessage.Body = bodyBuilder.ToMessageBody();
                    mimeMessage.Subject = "DDD Register Confirm Code";

                    SmtpClient client = new SmtpClient();
                    client.Connect("smtp.gmail.com", 587, false);
                    client.Authenticate("registermail.activation@gmail.com", "umlw sexu cbjh gfez");
                    await client.SendAsync(mimeMessage);
                    client.Disconnect(true);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding new user.", ex);
            }
        }

        public async Task<bool> ConfirmMailAsync(ConfirmCodeDto model)
        {
            try
            {
                if (model == null)
                    throw new ArgumentNullException(nameof(model), "model was null");

                if (string.IsNullOrEmpty(model.Email))
                    throw new Exception("Email cannot be empty");

                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user == null)
                    throw new Exception("User not found");

                if (user.ConfirmCode != model.ConfirmCode)
                    throw new Exception("Invalid confirmation code");

                user.EmailConfirmed = true;
                user.ConfirmCode = null;
                await _userManager.UpdateAsync(user);
                await _userManager.AddToRoleAsync(user, "User");
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"An unexpected error occurred while confirming email: {ex.Message}", ex);
            }
        }

        public async Task<bool> AdminRegisterAsync(RegisterDto model)
        {
            try
            {
                if (model == null)
                    throw new ArgumentNullException(nameof(model), "model was null");

                var nameSurnameParts = model.NameSurname.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (nameSurnameParts.Length < 2)
                {
                    throw new Exception("Please, type your full name. (For Example: Name Surname).");
                }

                var firstName = ConvertTurkishToEnglishHelper.ConvertTurkishToEnglish(nameSurnameParts[0]);
                var lastName = ConvertTurkishToEnglishHelper.ConvertTurkishToEnglish(nameSurnameParts[nameSurnameParts.Length - 1]);
                var baseUsername = $"{firstName}{lastName}";
                var username = $"admin_{baseUsername}";

                int suffix = 1;
                while (await _userManager.FindByNameAsync(username) != null)
                {
                    username = $"admin_{baseUsername}{suffix}";
                    suffix++;
                }

                var user = new User
                {
                    NameSurname = model.NameSurname,
                    UserName = username,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    CreatedDate = DateTime.UtcNow
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding new admin.", ex);
            }
        }

        public async Task<List<RoleAssignVM>> GetRoleAssingAsync(string id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "Id was null");

                User user = await _userManager.FindByIdAsync(id);
                List<Role> allRoles = _roleManager.Roles.ToList();
                List<string>? userRoles = await _userManager.GetRolesAsync(user) as List<string>;
                List<RoleAssignVM> assignRoles = new List<RoleAssignVM>();
                allRoles.ForEach(role => assignRoles.Add(new RoleAssignVM
                {
                    HasAssign = userRoles.Contains(role.Name),
                    RoleId = role.Id,
                    RoleName = role.Name
                }));
                return assignRoles;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred.", ex);
            }
        }

        public async Task<bool> RoleAssignAsync(List<RoleAssignVM> modelList, string id)
        {
            try
            {
                User user = await _userManager.FindByIdAsync(id);
                foreach (RoleAssignVM role in modelList)
                {
                    if (role.HasAssign)
                        await _userManager.AddToRoleAsync(user, role.RoleName);
                    else
                        await _userManager.RemoveFromRoleAsync(user, role.RoleName);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while setting admin role.", ex);
            }
        }

        public async Task<bool> LogoutAsync()
        {
            try
            {
                var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var activeSession = await _context.UserSessions
                    .Where(s => s.UserId == userId)
                    .OrderByDescending(s => s.LoginDate).FirstOrDefaultAsync();

                if (activeSession != null)
                {
                    try
                    {
                        activeSession.LogoutDate = DateTime.Now.ToLocalTime();
                        activeSession.IsOnline = false;
                        await _context.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("An unexpected error occurred while doing logout.", ex);
                    }

                    _httpContextAccessor.HttpContext.Session.Clear();
                    await _signInManager.SignOutAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while doing logout.", ex);
            }
        }
    }
}
