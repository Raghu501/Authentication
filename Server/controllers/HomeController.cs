using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
public class HomeController : Controller
{

public IActionResult Index()
{
return View();
}

[Authorize]
public IActionResult Secret()
{
    return View();

}

[AllowAnonymous]
public IActionResult Authenticate()
{
    var claims = new []{
        new Claim(JwtRegisteredClaimNames.Sub,"Raghu"),
        new Claim("granny","cookie")
    };

    var bytes = Encoding.UTF8.GetBytes(Constants.secret);
    var skey = new SymmetricSecurityKey(bytes);
    var algorithm = SecurityAlgorithms.HmacSha256;
    var signedCredentials= new SigningCredentials(skey, algorithm);

    var token = new JwtSecurityToken(
       Constants.Issuer,
       Constants.Audience,
       claims,
       notBefore:DateTime.Now,
       expires:DateTime.Now.AddHours(1),
       signedCredentials

     );
//serialize
    var jsonToken =new  JwtSecurityTokenHandler().WriteToken(token);

     return Ok( new {access_token =jsonToken});
     //RedirectToAction("index");
    }
}