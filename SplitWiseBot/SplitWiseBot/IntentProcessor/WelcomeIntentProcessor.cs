using System;
using System.Collections.Generic;
using System.Text;
using Amazon.Lambda.Core;
using Amazon.Lambda.LexEvents;

namespace SplitWiseBot.IntentProcessor
{
    public class WelcomeIntentProcessor : AbstractIntentProcessor
    {
        public override LexResponse Process(LexEvent lexEvent, ILambdaContext context)
        {
            //var slots = lexEvent.CurrentIntent.Slots;

            var sessionAttributes = lexEvent.SessionAttributes ?? new Dictionary<string, string>();
            //string confirmationStaus = lexEvent.CurrentIntent.ConfirmationStatus;



            return Close(
                       sessionAttributes,
                       "Fulfilled",
                       new LexResponse.LexMessage
                       {
                           ContentType = MESSAGE_CONTENT_TYPE,
                           Content = String.Format("Welcome to SplitWise Bot")
                       }
                   );
        }
    }
}
