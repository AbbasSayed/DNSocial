using DNSocial.Domain.Exceptions.UserProfileExceptions;
using DNSocial.Domain.Validations.UserProfileValidations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNSocial.Domain.Aggregates.UserProfileAggregate
{
    public class BasicInfo
    {
        private BasicInfo()
        {

        }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public string EmailAddress { get; private set; }

        public string Phone { get; private set; }
        public DateTime DateOfBirth { get; private set; }

        public string CurrentCity { get; private set; }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="emailAddress"></param>
        /// <param name="phone"></param>
        /// <param name="dateOfBirth"></param>
        /// <param name="currentCity"></param>
        /// <returns><see cref="BasicInfo"/></returns>
        /// <exception cref="UserProfileNotValidException"></exception>
        public static BasicInfo CreateBasicInfo(string firstName, string lastName, 
            string emailAddress, string phone, DateTime dateOfBirth, string currentCity)
        {
            var validator = new BasicInfoValidator();
            var basicInfo = new BasicInfo()
            {
                FirstName = firstName,
                LastName = lastName,
                EmailAddress = emailAddress,
                Phone = phone,
                DateOfBirth = dateOfBirth,
                CurrentCity = currentCity
            };

            var validationResult = validator.Validate(basicInfo);

            // case valid
            if (validationResult.IsValid)
            {
                return basicInfo;
            }

            // case invalid : throw UserProfileNotValidException

            var notValidException = new UserProfileNotValidException("The user Profile is not valid");
            validationResult.Errors.ForEach(e => notValidException.ValidationErrors.Add(e.ErrorMessage));
            throw notValidException;

        }


    }
}
