using Entities.DTO;
using Entities.DTO.PaymentMethodsDTO;
using Entities.Exceptions;
using Entities.Models;
using Repositories.Contracts;
using Repositories.EFCore;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class PaymentManager : IPaymentService
    {
        private readonly IRepositoryManager _manager;
        public PaymentManager(IRepositoryManager manager)
        {
            _manager = manager;
        }

        public PaymentMethods CreatePaymentMethod(PaymentMethods paymentMethod)
        {
            _manager.PaymentService.CreatePaymentMethod(paymentMethod);
            _manager.Save();
            return paymentMethod;
        }

        public void DeletePaymentMethod(int payment_id, PaymentMethods paymentMethod)
        {
            var entity = _manager.PaymentService.GetOnePaymentMethodById(payment_id, false);
            if (entity == null)
                throw new PaymentNotFound(payment_id);
            _manager.PaymentService.DeletePaymentMethod(payment_id, paymentMethod);
            _manager.Save();
           
        }

        public IQueryable<PaymentMethods> GetAllPaymentMethods(bool trackChanges)
        {
            return _manager.PaymentService.GetAllPaymentMethods(trackChanges);
        }

        public PaymentMethods GetOnePaymentMethodById(int payment_id, bool trackChanges)
        {
            return _manager.PaymentService.GetOnePaymentMethodById(payment_id, trackChanges)
                ?? throw new PaymentNotFound(payment_id);
        }

        public void UpdatePaymentMethod(int payment_id, PaymentMethodDtoForUpdate paymentDto, bool trackChanges)
        {
            var entity = _manager.PaymentService.GetOnePaymentMethodById(payment_id, true);
            if (entity == null)
                throw new PaymentNotFound(payment_id);
            entity.method_type = paymentDto.MethodType;
            entity.cardNumber = paymentDto.CardNumber;
            entity.cardHolder = paymentDto.CardHolder;
            entity.ExpiryDate = paymentDto.ExpiryDate;
            entity.CVV = paymentDto.CVV;
            entity.BillingAddress = paymentDto.BillingAddress;
            _manager.PaymentService.UpdatePaymentMethod(payment_id, entity, true);
            _manager.Save();

        }
    }
}
