
using System;

namespace CleanCode.SwitchStatements
{
    public abstract class Customer
    {

        public abstract MonthlyStatement GeneratesStatement(MonthlyUsage monthlyUsage);
    }

}
