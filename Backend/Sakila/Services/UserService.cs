using Sakila.Models;
using Sakila.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sakila.Services
{
    public interface IUserService
    {
        string Login(string username, string password);
        string Register(string username, string password);
        string GetUsername(Guid userId);
    }

    public class UserService : IUserService
    {

        private IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public string Login(string username, string password)
        {
            User user = userRepository.Get(username, password);
            if (user == null)
            {
                return userRepository.IsExisted(username) ? "Fail:Sai mật khẩu" : "Fail:Tài khoản không tồn tại";
            }
            return "Success:" + user.Id;
        }

        public string GetUsername(Guid userId)
        {
            User user = userRepository.Get(userId);
            return user == null ? "Fail:Tài khoản không tồn tại" : "Success:" + user.Username;
        }

        public string Register(string username, string password)
        {
            if (userRepository.IsExisted(username))
            {
                return "Fail:Đã tồn tại tên tài khoản";
            }
            User user = userRepository.Create(username, password);
            return user == null ? "Fail:Lỗi hệ thống" : "Success:" + user.Id;
        }
    }

    
}
