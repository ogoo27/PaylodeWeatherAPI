using PaylodeWeatherAPI.Credential_Auth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaylodeWeatherAPI.Contracts
{
    public interface IAuthentication
    {
        Task<string> Login(LoginModel user);
        Task<string> SignUp(SignUpDto signUpDto);

    }

}

