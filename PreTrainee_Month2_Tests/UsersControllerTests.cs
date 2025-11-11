using Moq;
using PreTrainee_Month2.ApplicationLayer.ServiceInterfaces;
using PreTrainee_Month2.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PreTrainee_Month2_Tests
{
    public class UsersControllerTests
    {
        [Fact]
        public void TestGetUsers()
        {
            var mock = new Mock<IUserService>();//создаём "заглушку"
            mock.Setup(x=>x.Get)
            UsersController usersController = new UsersController();
        }
    }
}
