// Тестовое задание https://github.com/boiledgas/IT.Test

using MediatR;

namespace IT.Test.Application.User.SetOrganization
{
    public class SetOrganizationCommand : IRequest<Unit>
    {
        public string OrganizationName { get; set; }
        public string Email { get; set; }
    }
}
