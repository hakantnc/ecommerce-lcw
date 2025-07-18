using Entities.Exceptions;
using Entities.Models;
using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public class PaymentRepository : RepositoryBase<PaymentMethods>, IPaymentRepository
    {
        public PaymentRepository(AppDbContext context) : base(context)
        {
        }

        public void CreatePaymentMethod(PaymentMethods paymentMethod) => Create(paymentMethod);

        public void DeletePaymentMethod(int payment_id, PaymentMethods paymentMethod)
        {
            Delete(paymentMethod);
        }

        public IQueryable<PaymentMethods> GetAllPaymentMethods(bool trackChanges) => FindAll(trackChanges);

        public PaymentMethods GetOnePaymentMethodById(int payment_id, bool trackChanges) => FindByCondition(p => p.Id == payment_id, trackChanges)
            .SingleOrDefault()!;

        public void UpdatePaymentMethod(int payment_id, PaymentMethods paymentMethod, bool trackChanges)
        {
            var entity = GetOnePaymentMethodById(payment_id, trackChanges);
            if (entity == null)
                throw new PaymentNotFound(payment_id);
            entity.method_type = paymentMethod.method_type;
            entity.cardNumber = paymentMethod.cardNumber;
            entity.cardHolder = paymentMethod.cardHolder;
            entity.CVV = paymentMethod.CVV;
            entity.ExpiryDate = paymentMethod.ExpiryDate;
            entity.BillingAddress = paymentMethod.BillingAddress;
            entity.customer_id = paymentMethod.customer_id;
            Update(entity);
        }
    }
}
