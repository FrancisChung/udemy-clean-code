using System;

namespace CleanCode.SwitchStatements
{
    public class PayAsYouGoCustomer : Customer
    {


        public override MonthlyStatement GeneratesStatement(MonthlyUsage monthlyUsage)
        {
            var statement = new MonthlyStatement();

            statement.CallCost = 0.12f * monthlyUsage.CallMinutes;
            statement.SmsCost = 0.12f * monthlyUsage.SmsCount;
            statement.TotalCost = statement.CallCost + statement.SmsCost;

            return statement;
        }
    }
}