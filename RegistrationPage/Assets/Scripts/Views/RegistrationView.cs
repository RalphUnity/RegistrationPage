using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using RegController;

namespace RegView
{
    public class RegistrationView : MonoBehaviour
    {
        [SerializeField] private RegistrationController regController;

        #region Dropdowns
        [SerializeField] private TMP_Dropdown monthDropdown;
        [SerializeField] private TMP_Dropdown dayDropdown;
        [SerializeField] private TMP_Dropdown yearDropdown;
        [SerializeField] private TMP_Dropdown phoneNumberFormatDropdown;
        #endregion

        #region TMP UGUIs
        [SerializeField] private TextMeshProUGUI bdayErrorMessage;
        [SerializeField] private TextMeshProUGUI emailErrorMessage;
        [SerializeField] private TextMeshProUGUI supportText;
        #endregion

        #region InputFields
        [SerializeField] private TMP_InputField firstNameField;
        [SerializeField] private TMP_InputField lastNameField;
        [SerializeField] private TMP_InputField zipCode;
        [SerializeField] private TMP_InputField emailField;
        [SerializeField] private TMP_InputField phoneNumberField;
        #endregion

        #region Buttons
        [SerializeField] private Button sendCodeButton;
        #endregion

        private string[] _numberFormat = { "+972", "+63" };

        private bool _isEmailValid = false;
        private bool _isBirthdayValid = true;
        private bool _isMobileNumValid = false;

        private const string NOVOSGG_SITE = "https://www.novos.gg/";

        // Start is called before the first frame update
        void Start()
        {
            // clear the old options of the dropdown menus
            monthDropdown.ClearOptions();
            dayDropdown.ClearOptions();
            yearDropdown.ClearOptions();
            phoneNumberFormatDropdown.ClearOptions();

            // populate birthday dropdowns
            monthDropdown.AddOptions(regController.GetCalendarOption(Calendar.Month));
            dayDropdown.AddOptions(regController.GetCalendarOption(Calendar.Day));
            yearDropdown.AddOptions(regController.GetCalendarOption(Calendar.Year));
            phoneNumberFormatDropdown.AddOptions(_numberFormat.ToList());

            // Add client side validation for the inputfields and buttons 
            yearDropdown.onValueChanged.AddListener(delegate
            {
                CheckAge(yearDropdown.options[yearDropdown.value].text);
            });

            emailField.onValueChanged.AddListener(CheckEmail);

            phoneNumberField.onValueChanged.AddListener(CheckPhoneNumber);

            phoneNumberFormatDropdown.onValueChanged.AddListener(delegate
            {
                ChangeNumberFormat(phoneNumberFormatDropdown.options[phoneNumberFormatDropdown.value].text);
            });

            sendCodeButton.onClick.AddListener(SendCode);
        }

        // Update is called once per frame
        void Update()
        {
            // Allow generateCode if all fields have been filled up and in correct format.
            if (!string.IsNullOrEmpty(firstNameField.text) && !string.IsNullOrEmpty(lastNameField.text) &&
                !string.IsNullOrEmpty(emailField.text) && !string.IsNullOrEmpty(zipCode.text) && !string.IsNullOrEmpty(phoneNumberField.text))
            {
                sendCodeButton.interactable = (_isEmailValid && _isBirthdayValid && _isMobileNumValid) ? true : false;
            }
            else
            {
                sendCodeButton.interactable = false;
            }
        }

        private void CheckAge(string selectedYear)
        {
            bool isValid = regController.IsBirthdateValid(selectedYear);
            bdayErrorMessage.gameObject.SetActive(!isValid);
            _isBirthdayValid = isValid;
        }

        private void CheckEmail(string emailText)
        {
            bool isValid = regController.IsEmailValid(emailText);
            emailErrorMessage.gameObject.SetActive(!isValid);
            _isEmailValid = isValid;
        }

        private void CheckPhoneNumber(string number)
        {
            _isMobileNumValid = (phoneNumberField.text.Length == phoneNumberField.characterLimit) ? true : false;
        }

        private void ChangeNumberFormat(string numberFormat)
        {
            phoneNumberField.text = "";

            if (numberFormat == "+63")
                phoneNumberField.characterLimit = 10;
            else if (numberFormat == "+972")
                phoneNumberField.characterLimit = 8;
        }

        private void SendCode()
        {
            gameObject.SetActive(false);
            regController.GenerateCode(phoneNumberFormatDropdown.value, phoneNumberField);
        }

        public void Support()
        {
            Application.OpenURL(NOVOSGG_SITE);
        }
    }
}
