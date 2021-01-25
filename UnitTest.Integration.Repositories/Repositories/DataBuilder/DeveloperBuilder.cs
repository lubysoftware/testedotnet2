using Domain.Entities;
using System;
using System.Collections.Generic;

namespace UnitTest.Integration.Repositories.Repositories.DataBuilder
{
    public class DeveloperBuilder
    {
        private Developer developer;
        private List<Developer> developerList;

        public DeveloperBuilder()
        {
        }

        public Developer CreateDeveloper()
        {
            developer = new Developer() {
                UpdatedAt = DateTime.Now,
                CreatedAt = DateTime.Now,
            };
            return developer;
        }

        public List<Developer> CreateDeveloperList(int amount)
        {
            developerList = new List<Developer>();
            for (int i = 0; i < amount; i++)
            {
                developerList.Add(CreateDeveloper());
            }

            return developerList;
        }
    }
}
