using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Amazon.Lambda.Core;
using Amazon.Lambda.LexEvents;
using SplitWiseBot.Libraries;
using SplitWiseBot.Slots;

namespace SplitWiseBot
{
    public class BasicUserAdditionIntentProcessor : AbstractIntentProcessor
    {
        #region variable declarations

        public UserInfo _userInfoObject;

        #endregion
        public BasicUserAdditionIntentProcessor()
        {

            _userInfoObject = new UserInfo();

        }
        public override LexResponse Process(LexEvent lexEvent, ILambdaContext context)
        {
            var slots = lexEvent.CurrentIntent.Slots;

            var sessionAttributes = lexEvent.SessionAttributes ?? new Dictionary<string, string>();
            string confirmationStaus = lexEvent.CurrentIntent.ConfirmationStatus;
            Dictionary<string, object> _lstUserInfoObject = slots.ToDictionary(pair => pair.Key, pair => (object)pair.Value);
            UserInfo userInfoObject = new UserInfo
            {
                Name = slots.ContainsKey("Name") ? slots["Name"] : string.Empty,
                EmailId = slots.ContainsKey("EmailId") ? slots["EmailId"] : string.Empty,
                PhoneNumber = slots.ContainsKey("PhoneNumber") ? slots["PhoneNumber"] : string.Empty,

            };

            try
            {
                ValidationResult validObject = Validate(_lstUserInfoObject);
                if (validObject.IsValid)
                {
                    return Delegate(sessionAttributes, slots);
                }
                else
                {
                    return ElicitSlot(sessionAttributes, lexEvent.CurrentIntent.Name, slots, validObject.ViolationSlot, new LexResponse.LexMessage
                    {
                        ContentType = MESSAGE_CONTENT_TYPE,
                        Content = validObject.Message.Content.ToString()
                    });

                }
            }
            catch (Exception ex)
            {
                return ElicitSlot(sessionAttributes, lexEvent.CurrentIntent.Name, slots, "OrderNumber", new LexResponse.LexMessage
                {
                    ContentType = MESSAGE_CONTENT_TYPE,
                    Content = String.Format(ex.Message)
                });

            }
        }
        /// <summary>
        /// Verifies that any values for slots in the intent are valid.
        /// </summary>
        /// <param name="user addition"></param>
        /// <returns></returns>
        private ValidationResult Validate(IDictionary<string, object> _DictList)
        {
            try
            {
                if (_DictList == null)
                {
                    _DictList = new Dictionary<string, object>();
                }
                
                 UserInfo userInfoObject = (UserInfo)SimpleObjectDictionaryMapper<UserInfo>.GetObject(_DictList);
                _DictList = CommonFunctions.IsAnyNullOrEmpty<UserInfo>(userInfoObject);
                if (_DictList != null)
                {
                    if (_DictList.Keys.Count != 0)
                    {

                        if (_DictList.ContainsKey("Name"))
                        {
                            return new ValidationResult(false, BasicUserAdditionSlots.Name,
                     $"Please enter name ");
                        }
                        if (_DictList.ContainsKey("EmailId"))
                        {
                            return new ValidationResult(false, BasicUserAdditionSlots.EmailId,
                     $"Please enter email");
                        }
                        if (_DictList.ContainsKey("PhoneNumber"))
                        {
                            return new ValidationResult(false, BasicUserAdditionSlots.PhoneNumber,
                     $"Please enter phone number");
                        }

                    }
                    
                    
                }
                if (!String.IsNullOrEmpty(userInfoObject.EmailId))
                {
                    bool IsValidEmail = CommonFunctions.IsValidEmail(userInfoObject.EmailId);
                    if (IsValidEmail==false)
                    {
                        return new ValidationResult(false, BasicUserAdditionSlots.EmailId,
                            $"Please enter email {userInfoObject.EmailId} in proper format");
                    }
                }

                if (!string.IsNullOrEmpty(userInfoObject.PhoneNumber))
                {
                    bool isValidPhoneNumber = CommonFunctions.IsPhoneNumber(userInfoObject.PhoneNumber);
                    if (isValidPhoneNumber==false)
                    {
                        return new ValidationResult(false, BasicUserAdditionSlots.PhoneNumber,
                            $"Please enter phone number {userInfoObject.PhoneNumber} in proper format");
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ValidationResult.VALID_RESULT;
        }
    }
}
