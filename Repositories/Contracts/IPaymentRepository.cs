using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IPaymentRepository : IRepositoryBase<PaymentMethods>
    {
        IQueryable<PaymentMethods> GetAllPaymentMethods(bool trackChanges);
        PaymentMethods GetOnePaymentMethodById(int  payment_id, bool trackChanges);
        void CreatePaymentMethod(PaymentMethods paymentMethod);
        void UpdatePaymentMethod(int payment_id, PaymentMethods paymentMethod, bool trackChanges);
        void DeletePaymentMethod(int payment_id, PaymentMethods paymentMethod);

    }
}
