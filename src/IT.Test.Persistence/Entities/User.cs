// Тестовое задание https://github.com/boiledgas/IT.Test

namespace IT.Test.Persistence.Entities
{
    public class User
    {
        public int Id { get; protected set; }
        public string Name { get; protected set; }
        public string Surname { get; protected set; }
        public string Patronymic { get; protected set; }
        public string Number { get; protected set; }
        public string Email { get; protected set; }
        public int? OrganizationId { get; protected set; }
        public Organization Organization { get; protected set; }

        protected User()
        {
        }

        public User(int id, string name, string surname, string patronymic, string number, string email)
            : this(name, surname, patronymic, number, email)
        {
            Id = id;
        }
        public User(string name, string surname, string patronymic, string number, string email)
        {
            Name = name;
            Surname = surname;
            Patronymic = patronymic;
            Number = number;
            Email = email;
        }

        public void SetOrganization(Organization organization)
        {
            Organization = organization;
        }
    }
}
