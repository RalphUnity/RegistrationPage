using System;
using UnityEngine;

namespace RegModel
{
    public class RegistrationModel
    {
        /// <summary>
        /// Wrap this into async task if using server call, put a try catch so that we can handle the error exception
        /// incase the server fails/shut down
        /// </summary>
        /// <returns></returns>
        public string GenerateOTPCode()
        {
            // TO DO: Generate OTP from the server 
            //try
            //{
            //    // function for api call from the server 
            //    // make sure it's not null before parsing it to string 

            //  return string
            //}
            //catch(Exception ex)
            //{
            //    Debug.LogException(ex);
            //}


            // temporary code to simulate otp generation
            System.Random rand = new System.Random();
            int randNum = rand.Next(1000000);

            return randNum.ToString("D6");

        }

        /// <summary>
        /// Wrap this into async task if using server call, put a try catch so that we can handle the error exception
        /// incase the server fails/shut down
        /// </summary>
        /// <returns></returns>
        public bool UserDetailsValidation()
        {
            bool isValid = true;
            // TO DO: Check user details 
            //try
            //{
            //    // function for api call from the server 
            //    // make sure it's not null before parsing it to bool

            // return bool

            //}
            //catch(Exception ex)
            //{
            //    Debug.LogException(ex);
            //}

            return isValid;
        }

    }
}
