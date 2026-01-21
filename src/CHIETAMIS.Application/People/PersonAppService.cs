using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using CHIETAMIS.People.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Configuration;
using Abp.Zero.Configuration;

namespace CHIETAMIS.People
{
    public class PersonAppService : CHIETAMISAppServiceBase
    {
        private readonly IRepository<Person> _personRepository;
        private readonly IRepository<PersonPhysicalAddress> _physicalAddressRepository;
        private readonly IRepository<PersonPostalAddress> _postalAddressRepository;

        public PersonAppService(IRepository<Person> personRepository,
                                 IRepository<PersonPhysicalAddress> physicalAddressRepository,
                                 IRepository<PersonPostalAddress> postAddressRepository)
        {
            _personRepository = personRepository;
            _physicalAddressRepository = physicalAddressRepository;
            _postalAddressRepository = postAddressRepository;
        }
        public async Task SavePerson(PersonDto input)
        {
            var cPerson = _personRepository.GetAll().Where(a => a.Email == input.Email);
            if (cPerson.Count() == 0)
            {
                var cPers = ObjectMapper.Map<Person>(input);
                await _personRepository.InsertAsync(cPers);
            } else {
                var cPers = await _personRepository.FirstOrDefaultAsync(cPerson.First().Id);
                cPers.Title = input.Title;
                cPers.Designation = input.Designation;
                cPers.Firstname = input.Firstname;
                cPers.Middlenames = input.Middlenames;
                cPers.Lastname = input.Lastname;
                cPers.Idtype = input.Idtype;
                cPers.Saidnumber = input.Saidnumber;
                cPers.Otheriddetails = input.Otheriddetails;
                cPers.Dob = input.Dob;
                cPers.Phone = input.Phone;
                cPers.Cellphone = input.Cellphone;
                cPers.Email = input.Email;
                cPers.Gender = input.Gender;
                cPers.Equity = input.Equity;
                cPers.Language = input.Language;
                cPers.Nationality = input.Nationality;
                cPers.Citizenship = input.Citizenship;
                await _personRepository.UpdateAsync(cPers);
            }
        }

        public async Task SavePhysicalAddress(PersonPhysicalAddressDto input)
        {
            var cPhysicalAddress = _physicalAddressRepository.GetAll().Where(a => a.personId == input.personId);
            if (cPhysicalAddress.Count() == 0)
            {
                var cPhysAddress = ObjectMapper.Map<PersonPhysicalAddress>(input);
                await _physicalAddressRepository.InsertAsync(cPhysAddress);
            } else {
                var cPhyslAddress = await _physicalAddressRepository.FirstOrDefaultAsync(cPhysicalAddress.First().Id);
                cPhyslAddress.addressline1 = input.addressline1;
                cPhyslAddress.addressline2 = input.addressline2;
                cPhyslAddress.area = input.area;
                cPhyslAddress.district = input.district;
                cPhyslAddress.municipality = input.municipality;
                cPhyslAddress.postcode = input.postcode;
                cPhyslAddress.province = input.province;
                cPhyslAddress.suburb = input.suburb;
                await _physicalAddressRepository.UpdateAsync(cPhyslAddress);
            }
        }

        public async Task SavePostalAddress(PersonPostalAddressDto input)
        {
            var cPostalAddress = _postalAddressRepository.GetAll().Where(a => a.personId == input.personId);
            if (cPostalAddress.Count() == 0)
            {
                var cPosAddress = ObjectMapper.Map<PersonPostalAddress>(input);
                await _postalAddressRepository.InsertAsync(cPosAddress);
            } else {
                var cPoslAddress = await _postalAddressRepository.FirstOrDefaultAsync(cPostalAddress.First().Id);
                cPoslAddress.addressline1 = input.addressline1;
                cPoslAddress.addressline2 = input.addressline2;
                cPoslAddress.area = input.area;
                cPoslAddress.district = input.district;
                cPoslAddress.municipality = input.municipality;
                cPoslAddress.postcode = input.postcode;
                cPoslAddress.province = input.province;
                cPoslAddress.suburb = input.suburb;
                await _postalAddressRepository.UpdateAsync(cPoslAddress);
            }
        }

        public async Task ChangePostalIndicator(int Id, int userId, bool indicator)
        {
            var cPostalAddress = _postalAddressRepository.GetAll().Where(a => a.personId == Id);
            if (cPostalAddress.Count() == 0 && indicator == true)
            {
                var postAddr = new PersonPostalAddress();
                postAddr.personId = Id;
                postAddr.userId = userId;
                postAddr.sameasphysical = indicator;
                await _postalAddressRepository.InsertAsync(postAddr);
            } else {
                if (indicator == true)
                {
                    var cPoslAddress = await _postalAddressRepository.FirstOrDefaultAsync(cPostalAddress.First().Id);
                    cPoslAddress.sameasphysical = indicator;
                    cPoslAddress.addressline1 = null;
                    cPoslAddress.addressline2 = null;
                    cPoslAddress.area = null;
                    cPoslAddress.district = null;
                    cPoslAddress.municipality = null;
                    cPoslAddress.postcode = null;
                    cPoslAddress.province = null;
                    cPoslAddress.suburb = null;

                    await _postalAddressRepository.UpdateAsync(cPoslAddress);
                } else
                {
                    var cPoslAddress = await _postalAddressRepository.FirstOrDefaultAsync(cPostalAddress.First().Id);
                    cPoslAddress.sameasphysical = indicator;
                    await _postalAddressRepository.UpdateAsync(cPoslAddress);
                }
            }
        }

        public async Task<PersonForViewDto> GetBiodataForView(int personid)
        {
            var biodata = await _personRepository.GetAsync(personid);

            var output = new PersonForViewDto { Person = ObjectMapper.Map<PersonDto>(biodata) };

            return output;
        }


        public async Task<PersonForViewDto> Get(int id)
        {
            var biodata = _personRepository.GetAll().Where(a => a.Id == id).FirstOrDefault();

            var output = new PersonForViewDto { Person = ObjectMapper.Map<PersonDto>(biodata) };

            return output;
        }

        public async Task<PersonForViewDto> GetPersonByUserId(int userid)
        {
            var biodata = _personRepository.GetAll().Where(a=>a.Userid == userid).FirstOrDefault();

            var output = new PersonForViewDto { Person = ObjectMapper.Map<PersonDto>(biodata) };

            return output;
        }

        public async Task<PersonPhysicalAddressForViewDto> GetPersonPhysAddress(int personId)
        {
            var address = _physicalAddressRepository.GetAll().Where(a => a.personId == personId).FirstOrDefault();

            var output = new PersonPhysicalAddressForViewDto { PersonPhysicalAddress = ObjectMapper.Map<PersonPhysicalAddressDto>(address) };

            return output;
        }

        public async Task<PersonPostalAddressForViewDto> GetPersonPostAddress(int personId)
        {
            var address = _postalAddressRepository.GetAll().Where(a => a.personId == personId).FirstOrDefault();

            var output = new PersonPostalAddressForViewDto { PersonPostalAddress = ObjectMapper.Map<PersonPostalAddressDto>(address) };

            return output;
        }

    }
}
