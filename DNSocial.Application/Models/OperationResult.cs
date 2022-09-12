using DNSocial.Application.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNSocial.Application.Models
{
    public class OperationResult<T>
    {
        public T Payload { get; set; }

        public bool IsError { get; private set; }

        public List<Error> Errors { get; private set; } = new List<Error>();


        /// <summary>
        /// Adds an Error to the list and set the true flag to true
        /// </summary>
        /// <param name="code"></param>
        /// <param name="massage"></param>
        public void AddError(ErrorCode code, string massage)
        {
            HandleError(code, massage);
        }

        
        /// <summary>
        /// Add a defult Error with UnknownError
        /// </summary>
        /// <param name="message"></param>
        public void AddDefaultError(string message)
        {
            HandleError(ErrorCode.UnknownError, message);
        }



        private void HandleError(ErrorCode code, string massage)
        {
            IsError = true;
            var error = new Error() { Message = massage, Code = code };
            Errors.Add(error);
        }
    }
}
