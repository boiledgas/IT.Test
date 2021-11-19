// Тестовое задание https://github.com/boiledgas/IT.Test

using System;
using System.Linq;
using System.Threading.Tasks;
using IT.Test.Api.Controllers;
using IT.Test.Persistence;
using IT.Test.Persistence.Entities;
using IT.Test.StorageService.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;
using ApiUser = IT.Test.StorageService.Models.User;
using DbUser = IT.Test.Persistence.Entities.User;

namespace IT.Test.StorageService
{
    public class UsersControllerTests : IClassFixture<ServiceProviderFixture>
    {
        readonly ServiceProviderFixture _sp;
        public UsersControllerTests(ServiceProviderFixture sp)
            => _sp = sp;

        [Fact]
        public async Task GetList_Filter_Organization()
        {
            PersistenceContext context = _sp.Get<PersistenceContext>();
            var organization = new Organization(Guid.NewGuid().ToString("N"));
            var user = new DbUser(
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString());
            context.Organizations.Add(organization);
            context.Users.Add(user);
            user.SetOrganization(organization);
            await context.SaveChangesAsync();

            UsersController controller = _sp.Controller<UsersController>();
            PaginationResponse<ApiUser> result = await controller.List(new ListRequest { Count = 2, Organization = organization.Name.ToLower() });
            Assert.Equal(1, result.Count);

            ApiUser resultUser = result.Data.Single();
            Assert.Equal(user.Name, resultUser.Name);
            Assert.Equal(user.Surname, resultUser.Surname);
            Assert.Equal(user.Patronymic, resultUser.Patronymic);
            Assert.Equal(user.Number, resultUser.Number);
            Assert.Equal(user.Email, resultUser.Email);
            Assert.Equal(user.Organization.Name, resultUser.Organization);
        }

        [Fact]
        public async Task SetOrganization_Organization_Exist()
        {
            PersistenceContext context = _sp.Get<PersistenceContext>();
            var organization = new Organization(Guid.NewGuid().ToString("N"));
            var user = new DbUser(
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString());
            context.Organizations.Add(organization);
            context.Users.Add(user);
            await context.SaveChangesAsync();

            UsersController controller = _sp.Controller<UsersController>();
            await controller.SetOrganization(new SetOrganizationRequest { Email = user.Email, OrganizationName = organization.Name });

            user = await context.Users.SingleOrDefaultAsync(u => u.Id == user.Id);
            Assert.Equal(organization.Id, user.OrganizationId);
        }

        [Fact]
        public async Task SetOrganization_Organization_Created()
        {
            PersistenceContext context = _sp.Get<PersistenceContext>();
            var organization = new Organization(Guid.NewGuid().ToString("N"));
            var user = new DbUser(
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString());
            context.Users.Add(user);
            await context.SaveChangesAsync();

            UsersController controller = _sp.Controller<UsersController>();
            await controller.SetOrganization(new SetOrganizationRequest { Email = user.Email, OrganizationName = organization.Name });

            user = await context.Users.SingleOrDefaultAsync(u => u.Id == user.Id);
            Assert.NotNull(user.OrganizationId);
        }
    }
}
