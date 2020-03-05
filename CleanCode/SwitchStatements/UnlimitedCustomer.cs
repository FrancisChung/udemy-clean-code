using System;

namespace CleanCode.SwitchStatements
{
    public class UnlimitedCustomer : Customer
    {
        public override MonthlyStatement GeneratesStatement(MonthlyUsage monthlyUsage)
        {
            var statement = new MonthlyStatement();

            statement.TotalCost = 54.90f;
            return statement;
        }
    }
}