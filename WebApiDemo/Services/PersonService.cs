using GenFu;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiDemo.Models;

namespace WebApiDemo.Services
{
    public class PersonService : IPersonService
    {
        private List<Person> Persons { get; set; }

        public PersonService()
        {
            var i = 0;
            Persons = A.ListOf<Person>(50);
            Persons.ForEach(person =>
            {
                i++;
                person.Id = i;
            });
        }

        public Task<IEnumerable<Person>> GetAll()
        {
            var result = Persons.Select(x => x);
            return Task.FromResult(result);
        }

        public Task<Person> Get(int id)
        {
            var result = Persons.First(_ => _.Id == id);
            return Task.FromResult(result);
        }

        public Task<Person> Add(Person person)
        {
            var newid = Persons.OrderBy(_ => _.Id).Last().Id + 1;
            person.Id = newid;

            Persons.Add(person);

            return Task.FromResult(person);
        }

        public Task Update(int id, Person person)
        {
            var existing = Persons.First(_ => _.Id == id);
            existing.FirstName = person.FirstName;
            existing.LastName = person.LastName;
            existing.Address = person.Address;
            existing.Age = person.Age;
            existing.City = person.City;
            existing.Email = person.Email;
            existing.Phone = person.Phone;
            existing.Title = person.Title;

            return Task.CompletedTask;
        }

        public Task Delete(int id)
        {
            var existing = Persons.First(_ => _.Id == id);
            Persons.Remove(existing);

            return Task.CompletedTask;
        }
    }

    public interface IPersonService
    {
        Task<IEnumerable<Person>> GetAll();
        Task<Person> Get(int id);
        Task<Person> Add(Person person);
        Task Update(int id, Person person);
        Task Delete(int id);
    }
}
