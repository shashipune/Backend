using HomeAssignment.Core;
using HomeAssignment.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace HomeAssignment.Services
{
    public class ContactService : IContactService
    {
        private readonly IRepository<Contact> _contact;

        public ContactService(IRepository<Contact> contact)
        {
            _contact = contact;
        }
        public async Task Add(Contact contact)
        {
            await _contact.AddAsync(contact);
        }

        public async Task Delete(int id)
        {
            await _contact.DeleteAsync(id);
        }

        public async Task<IEnumerable<Contact>> GetAll()
        {

            return await _contact.GetAllAsync();
        }

        public async Task<Contact> GetById(int id)
        {
            return await _contact.GetByIdAsync(id);
        }

        public async Task Update(Contact contact)
        {
            await _contact.UpdateAsync(contact);
        }
    }
}
