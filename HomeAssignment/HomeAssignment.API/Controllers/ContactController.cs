using AutoMapper;
using HomeAssignment.API.Models;
using HomeAssignment.Core;
using HomeAssignment.Data;
using HomeAssignment.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace HomeAssignment.API.Controllers
{

    [ApiController]
    public class ContactController : Controller
    {
        private readonly ILogger<ContactController> _logger;
        private readonly IContactService _contact;
        private IMapper _mapper;

        public ContactController(ILogger<ContactController> logger,
            IContactService contact,IMapper mapper)
        {
            _logger = logger;
            _contact = contact;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("api/V1/contact")]
        public async Task<IActionResult> Get()
        {
            var result = await _contact.GetAll();
            if (result != null && result.Count()==0)
            {
                return NotFound(); // Return 404 if contact not found
            }
            var contacts = _mapper.Map<List<ContactVM>>(result);
            return Ok(contacts);
        }

        [HttpGet]
        [Route("api/V1/GetContactById/{id}")]
        public async Task<IActionResult> GetById([Required]int id)
        {
            var result = await _contact.GetById(id);
            if (result == null)
            {
                return NotFound(); // Return 404 if contact not found
            }
            var contact = _mapper.Map<ContactVM>(result);
            return Ok(contact);
        }

        [HttpPost]
        [Route("api/V1/contact")]
        public async Task<IActionResult> Add(ContactVM contact)
        {
            await _contact.Add(_mapper.Map<Contact>(contact));
            var result = await _contact.GetAll();
            var contacts = _mapper.Map<List<ContactVM>>(result);
            return new JsonResult(contacts)
            {
                StatusCode = StatusCodes.Status201Created
            };
        }

        [HttpPut]
        [Route("api/V1/contact")]
        public async Task<IActionResult> Update(ContactVM contact)
        {
            await _contact.Update(_mapper.Map<Contact>(contact));
            var result = await _contact.GetAll();
            var contacts = _mapper.Map<List<ContactVM>>(result);
            return Ok(contacts);
        }

        [HttpDelete]
        [Route("api/V1/contact/{id}")]
        public async Task<IActionResult> Delete([Required]int id)
        {
            await _contact.Delete(id);
            var result = await _contact.GetAll();
            return Ok(result);
        }
    }
}
