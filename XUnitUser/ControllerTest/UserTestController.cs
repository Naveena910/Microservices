using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using Contracts.IRepository;
using Contracts.IServices;
using Entities.Dtos.RequestDto;
using Entities.Dtos.ResponseDto;
using Entities.Model;
using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moq;
using Repository;
using Services;
using Services.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using User_Service.Controllers;

namespace XUnitUser.ControllerTest
{
    public class UserTestController
    {
        private readonly UserController controller;
        private readonly IService _Service;
        private readonly IUserService userService;
        private readonly IUserRepository _userRepository;
        private readonly IAddressRepository addressRepository;
        private readonly IPaymentRepository paymentRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly RepositoryContext _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger<UserService> _logger;
        private readonly ILogger<UserController> _log;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        

        public UserTestController()
        {
            _configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            using ServiceProvider services = new ServiceCollection()
                            .AddSingleton<Microsoft.Extensions.Configuration.IConfiguration>(_configuration)
                           
                            .BuildServiceProvider();
            MapperConfiguration mappingConfig = new MapperConfiguration(a =>
            {
                a.AddProfile(new Services.Helpers.Mappers());
            });
            IMapper _mapper = mappingConfig.CreateMapper();
            mapper = _mapper;
            IHostBuilder hostBuilder = Host.CreateDefaultBuilder().
          ConfigureLogging((builderContext, loggingBuilder) =>
          {
              loggingBuilder.AddConsole((options) =>
              {
                  options.IncludeScopes = true;
              });
          });
            IHost host = hostBuilder.Build();
            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            _logger = host.Services.GetRequiredService<ILogger<UserService>>();
            _log = host.Services.GetRequiredService<ILogger<UserController>>();
            _Service = new Service(mockHttpContextAccessor.Object);
            addressRepository = new AddressRepository(InMemorydataContext.inmemory());
            paymentRepository=new PaymentRepository(InMemorydataContext.inmemory());
            _userRepository = new UserRepository(InMemorydataContext.inmemory());
            _unitOfWork = new UnitOfWork(InMemorydataContext.inmemory(), _userRepository, addressRepository, _httpContextAccessor, paymentRepository);
            userService = new UserService(mapper, _userRepository, _configuration, _Service, _logger, _unitOfWork);
            controller = new UserController(userService,_log, _Service);
            string Id = ("c572c99e-ee1f-4d17-b69c-08dae952ed26");
            Mock<HttpContext> contextMock = new Mock<HttpContext>();
            contextMock.Setup(x => x.User).Returns(new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                                        new Claim(ClaimTypes.NameIdentifier,Id)
                           }, "TestAuthentication")));
            controller.ControllerContext.HttpContext = contextMock.Object;
            
        }
        [Fact]
        public void Login_OK_ObjectResult()
        {
            LoginDto logindto = new LoginDto() { Email = "Naveena@gmail.com", Password = "Navee@2002" };
            IActionResult actionResult=controller.Login(logindto);
            Assert.IsType<OkObjectResult>(actionResult);
        }
        [Fact]
        public void Login_UnauthorizedResult()
        {
            LoginDto logindto = new LoginDto() { Email = "Naveen@gmail.com", Password = "Navee@2002" };
            IActionResult actionResult = controller.Login(logindto);
            Assert.IsType<UnauthorizedObjectResult>(actionResult);
        }
        [Fact]
        public void CreateUser_Ok_ReturnStatus()
        {
            UserForCreatingDto user = new UserForCreatingDto() { 

            Password = "Navee@2002",
            FirstName = "Naveena",
            LastName = "T",
            Email = "Naveen@gmail.com",
            UserType = "Admin",
            Phone=9600334259,
            Address = new List<AddressForCreatingDto>(),
           
        };
            user.Address.Add(new AddressForCreatingDto()
            {
             
                Line1 = "12131",
                Line2 = "street",
                State = "TamilNadu",
                Pincode = 699910,
                City = "Chennai",
                Country = "India",

                Type = "Home"
            });
            IActionResult account = controller.CreateUserAccount(user);
            Assert.IsType<CreatedResult>(account);
        }
        [Fact]
        public void CreateUser_Ok_Conflict()
        {
            UserForCreatingDto user = new UserForCreatingDto()
            {

                Password = "Navee@2002",
                FirstName = "Naveena",
                LastName = "T",
                Email = "Naveena@gmail.com",
                UserType = "Admin",
                Phone = 9600334259,
                Address = new List<AddressForCreatingDto>(),

            };
            user.Address.Add(new AddressForCreatingDto()
            {

                Line1 = "12131",
                Line2 = "street",
                State = "TamilNadu",
                Pincode = 699910,
                City = "Chennai",
                Country = "India",

                Type = "Home"
            });
            IActionResult account = controller.CreateUserAccount(user);
            Assert.IsType<ConflictObjectResult>(account);
        }
        [Fact]
        public void GetUserById_OkObjectResult()
        {
           Guid userId1 = Guid.Parse("c572c99e-ee1f-4d17-b69c-08dae952ed26");
           IActionResult result = controller.GetUserById(userId1) as IActionResult;
           Assert.IsType<OkObjectResult>(result);
           OkObjectResult item = result as OkObjectResult;
           Assert.IsType<UserDto>(item.Value);
        }
        [Fact]
        public void GetUserById_NotFound_ObjectResult()
        {
            Guid userId1 = Guid.Parse("c572c99e-ee1f-4d17-b69c-08dae950ed26");
            IActionResult result = controller.GetUserById(userId1) as IActionResult;
            Assert.IsType<NotFoundObjectResult>(result);
        }
        [Fact]
        public void GetAllUser_Ok_ObjectResult()
        {
            IActionResult actionResult = controller.GetAllUser();
            Assert.IsType<OkObjectResult>(actionResult);
            OkObjectResult item = actionResult as OkObjectResult;
            Assert.IsType<List<UserDto>>(item.Value);
        }
        [Fact]
        public void UpdateUser_Ok_ObjectResult()
        {
            UserUpdateDto user = new UserUpdateDto()
            {

                Password = "Navee@2002",
                FirstName = "Naveena",
                LastName = "T",
                Email = "Naveen@gmail.com",
                UserType = "Admin",
                Phone = 9600334259,
                Address = new List<AddressForCreatingDto>(),

            };
            user.Address.Add(new AddressForCreatingDto()
            {

                Line1 = "12131",
                Line2 = "street",
                State = "TamilNadu",
                Pincode = 699910,
                City = "Chennai",
                Country = "India",

                Type = "Home"
            });
            Guid userId = Guid.Parse("c572c99e-ee1f-4d17-b69c-08dae950ed26");
            var account = controller.UpdateUser(userId, user);
            Assert.IsType<OkObjectResult>(account);

        }
    }
}
