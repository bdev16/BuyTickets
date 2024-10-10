using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using BuyTickets.models;
using Flunt.Validations;
using Flunt.Notifications;

namespace BuyTickets
{
    public class Verifier : Notifiable<Notification>
    {
        public NotificationResult Verify(Enterprise enterprise)
        {
            AddNotifications(enterprise.Notifications);

            if(!IsValid)
            {
                return new NotificationResult(
                    false,
                    "Alguns erros foram gerados",
                    Notifications
                );
            }

            return new NotificationResult(
                true,
                "Mensagem",
                null
            );
        }
    }
}