using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using RegController;

public class PhoneNumberView : MonoBehaviour
{
    [SerializeField] private RegistrationController regController;

    #region InputField
    [SerializeField] private TMP_InputField mobileNumber;
    [SerializeField] private TMP_InputField codeInputField;
    #endregion

    #region DropDowns
    [SerializeField] private TMP_Dropdown countryCodeDropDown;
    #endregion

    #region Buttons
    [SerializeField] private Button resendCodeButton;
    [SerializeField] private Button backButton;
    #endregion

    #region Images
    [SerializeField] private Image codePanel;
    #endregion

    #region RectTransforms
    [SerializeField] private RectTransform registrationForm;
    [SerializeField] private RectTransform registerAgainForm;
    #endregion

    #region TMP UGUIs
    [SerializeField] private TextMeshProUGUI codeMessage;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI gemText;
    #endregion

    private int _timer = 30;

    private string _code;

    private IEnumerator timerCoroutine;
    
    private void Start()
    {
        codeInputField.onValueChanged.AddListener(delegate
        {
            OnCodeInputChanded(codeInputField.text);
        });

        resendCodeButton.onClick.AddListener(delegate
        {
            ResendCode();
        });

        backButton.onClick.AddListener(delegate
        {
            BackButton();
        });
    }

    /// <summary>
    ///  Assign the previous state of phone number
    /// </summary>
    /// <param name="countryCode"></param>
    /// <param name="mobileNumberInputField"></param>
    public void Initialize(int countryCode, TMP_InputField mobileNumberInputField)
    {
        gameObject.SetActive(true);

        countryCodeDropDown.value = countryCode;
        codeInputField.text = "";
        codeInputField.interactable = true;
        mobileNumber.characterLimit = mobileNumberInputField.characterLimit;
        mobileNumber.text = mobileNumberInputField.text;

        timerCoroutine = CodeTimer();
        StartCoroutine(timerCoroutine);
    }

    public IEnumerator ShowCodeMessage(string otpCode)
    {
        mobileNumber.interactable = false;
        resendCodeButton.interactable = false;

        codeMessage.text = otpCode;
        _code = otpCode;

        codePanel.gameObject.SetActive(true);

        yield return new WaitForSeconds(10f);

        codePanel.gameObject.SetActive(false);
    }

    private IEnumerator CodeTimer()
    {
        timerText.gameObject.SetActive(true);
        int counter = _timer;
        while(counter > 0)
        {
            int minutes = Mathf.FloorToInt(counter / 60);
            int seconds = Mathf.FloorToInt(counter % 60);

            yield return new WaitForSeconds(1);

            counter--;
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        
        mobileNumber.interactable = true;
        resendCodeButton.interactable = true;
    }

    private IEnumerator GemParticleActivity()
    {
        gemText.gameObject.SetActive(true);
        registerAgainForm.gameObject.SetActive(true);

        yield return new WaitForSeconds(3f);

        gemText.gameObject.SetActive(false);
    }

    private void BackButton()
    {
        gameObject.SetActive(false);
        registrationForm.gameObject.SetActive(true);
        registerAgainForm.gameObject.SetActive(false);
    }

    private void OnCodeInputChanded(string code)
    {
        if(code == _code)
        {
            StopCoroutine(timerCoroutine);
            timerText.gameObject.SetActive(false);

            codeInputField.text = "";
            codeInputField.interactable = false;

            //show gem animation
            StartCoroutine(GemParticleActivity());
        }
    }
    
    private void ResendCode()
    {
        regController.GenerateCode(0, null, false);
        StartCoroutine(timerCoroutine);
    }
}
 