using Entities.DTO;
using Entities.DTO.PaymentMethodsDTO;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IPaymentService
    {
        IQueryable<PaymentMethods> GetAllPaymentMethods(bool trackChanges);
        PaymentMethods GetOnePaymentMethodById(int payment_id, bool trackChanges);
        PaymentMethods CreatePaymentMethod(PaymentMethods paymentMethod);

        void UpdatePaymentMethod(int payment_id, PaymentMethodDtoForUpdate paymentDto, bool trackChanges);
        void DeletePaymentMethod(int payment_id, PaymentMethods paymentMethod);

    }
}
