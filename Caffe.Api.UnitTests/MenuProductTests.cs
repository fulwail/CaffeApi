using AutoMapper;
using Caffe.Api.Application.Profiles;
using Caffe.Api.Application.Services;
using Caffe.Api.Domain;
using Caffe.Api.Domain.Models;
using Caffe.Api.Infrastructure;
using Caffe.Api.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Caffe.Api.UnitTests
{
    public class MenuProductTests
    {
        [Fact]
        public async Task MenuProductsReturnsDuplicateError()
        {
            var duplicateEntity = new MenuProduct()
            {
                Name = "Курица",
                Id = Guid.NewGuid()
            };
            var contextMock = new Mock<ICaffeContext>();
            
            var entities = new List<MenuProduct> {  duplicateEntity,
                new MenuProduct() { Name = "Авокадо", Id = Guid.NewGuid() }}.AsQueryable();
     
            contextMock.Setup(x => x.MenuProducts).ReturnsDbSet(entities);

            var repository = new MenuProductRepository(contextMock.Object);
           
       
            var result = await repository.IsDuplicateName(duplicateEntity.Name);        
            var result2 = await repository.IsDuplicateName("Отсутствующее наименование");
           
            Assert.True(result);
            Assert.False(result2);
        }
    }
}
