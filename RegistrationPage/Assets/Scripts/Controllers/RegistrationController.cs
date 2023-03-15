using System;
using TMPro;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using RegModel;
using RegView;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace RegController
{
    public enum Calendar
    {
        Month,
        Day,
        Year,
    }

    public class RegistrationController : MonoBehaviour
    {
        [SerializeField] private PhoneNumberView validation;
        [SerializeField] private RegistrationView registrationView;

        private RegistrationModel _registrationModel;

        private void Start() =>  _registrationModel = new RegistrationModel();

        public List<string> GetCalendarOption(Calendar calendarOption)
        {
            List<string> result = new List<string>();

            switch (calendarOption)
            {
                case Calendar.Month:
                    for (int i = 1; i <= 12; i++)
                    {
                        result.Add(i.ToString());
                    }
                    break;
                case Calendar.Day:
                    for (int i = 1; i <= 31; i++)
                    {
                        result.Add(i.ToString());
                    }
                    break;
                case Calendar.Year:
                    for (int i = 1937; i <= 2023; i++)
                    {
                        result.Add(i.ToString());
                    }
                    break;
            }

            return result;
        }
        
        public bool IsBirthdateValid(string selectedYear)
        {
            var today = DateTime.Today;
            var age = today.Year - int.Parse(selectedYear);

            if (age > 18)
            {
                return true;
            }

            return false;
        }

        public bool IsEmailValid(string email)
        {
            Regex emailRegex = new(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", RegexOptions.IgnoreCase);

            if (emailRegex.IsMatch(email))
                return true;

            return false;
        }

        public void GenerateCode(int selectedCountryCode, TMP_InputField phoneNumberField = null, bool isInitializing = true)
        {
            if (isInitializing)
                validation.Initialize(selectedCountryCode, phoneNumberField);

            // Check if details is valid in the server
            //_registrationModel.UserDetailsValidation();

            string otpCode = _registrationModel.GenerateOTPCode();
            StartCoroutine(validation.ShowCodeMessage(otpCode));
        }
    }
}
