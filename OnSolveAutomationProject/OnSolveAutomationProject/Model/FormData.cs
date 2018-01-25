using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace OnSolveAutomationProject.Model
{    
    public class FormData
    {
        private string _firstName;
        private string _lastName;
        private string _streetAddress;
        private string _addressLine2;
        private string _city;
        private string _state;
        private string _zipCode;
        private string _phoneNumber;
        private string _emailAddress;
        private string _date;


        public string FirstName
        {
            get
            {
                return _firstName;
            }

            set
            {
                _firstName = value;
            }
        }

        public string LastName
        {
            get
            {
                return _lastName;
            }

            set
            {
                _lastName = value;
            }
        }

        public string StreetAddress
        {
            get
            {
                return _streetAddress;
            }

            set
            {
                _streetAddress = value;
            }
        }

        public string AddressLine2
        {
            get
            {
                return _addressLine2;
            }

            set
            {
                _addressLine2 = value;
            }
        }

        public string City
        {
            get
            {
                return _city;
            }

            set
            {
                _city = value;
            }
        }

        public string State
        {
            get
            {
                return _state;
            }

            set
            {
                _state = value;
            }
        }

        public string ZipCode
        {
            get
            {
                return _zipCode;
            }

            set
            {
                _zipCode = value;
            }
        }

        public string PhoneNumber
        {
            get
            {
                return _phoneNumber;
            }

            set
            {
                _phoneNumber = value;
            }
        }

        public string EmailAddress
        {
            get
            {
                return _emailAddress;
            }

            set
            {
                _emailAddress = value;
            }
        }

        public string Date
        {
            get
            {
                return _date;
            }

            set
            {
                _date = value;
            }
        }

        public FormData()
        {

        }

        public bool Compare(FormData fData)
        {            
            if (_firstName == fData.FirstName && _lastName == fData.LastName 
                && _streetAddress == fData.StreetAddress && _addressLine2 == fData.AddressLine2 
                && _city == fData.City && _state == fData.State
                && _zipCode == fData.ZipCode && _phoneNumber == fData.PhoneNumber
                && _emailAddress == fData.EmailAddress && _date == fData.Date)                
            {
                return true;
            }
            return false;
        }
    }
}
