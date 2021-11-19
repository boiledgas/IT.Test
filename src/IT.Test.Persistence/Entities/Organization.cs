// Тестовое задание https://github.com/boiledgas/IT.Test

using System.Collections.Generic;

namespace IT.Test.Persistence.Entities
{
    public class Organization
    {
        public int Id { get; protected set; }
        public string Name { get; protected set; }
        public IList<User> Users { get; protected set; }

        protected Organization()
        {
        }
        internal Organization(int id, string name)
        {
            Id = id;
            Name = name;
        }
        public Organization(string name)
        {
            Name = name;
        }

    }
}
