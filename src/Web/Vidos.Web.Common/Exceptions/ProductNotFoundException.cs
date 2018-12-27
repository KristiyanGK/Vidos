using System;

namespace Vidos.Web.Common.Exceptions
{
    public class ProductNotFoundException : Exception
    {
        public ProductNotFoundException() :
            base(ExceptionMessages.ProductNotFoundExceptionMessage)
        {
            
        }
    }
}
