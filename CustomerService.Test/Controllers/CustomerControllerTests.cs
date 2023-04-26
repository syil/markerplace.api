using CustomerService.Contracts;
using CustomerService.Controllers;
using CustomerService.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerService.Test.Controllers;

public class CustomerControllerTests
{
    [Fact]
    public async Task Get_WhenGivenCustomerId_ShouldReturnCustomer()
    {
        var mockMediator = new Mock<IMediator>();
        mockMediator.Setup(setup => setup.Send(It.IsAny<GetCustomerQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => new Customer
            {
                CustomerId = new Guid("8c083514-225b-4af5-ab71-28289f664334"),
                Name = "Test",
                Lastname = "User"
            });

        var customerController = new CustomersController(mockMediator.Object);
        var result = await customerController.Get(new Guid("8c083514-225b-4af5-ab71-28289f664334"), CancellationToken.None);

        var actionResult = Assert.IsType<OkObjectResult>(result);
        var resultObject = Assert.IsAssignableFrom<Customer>(actionResult.Value);

        Assert.Equal("Test", resultObject.Name);
    }
}
