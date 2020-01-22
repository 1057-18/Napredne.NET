using System;
using System.ComponentModel;

namespace DataAccessLayer.Models
{
    public enum PaymentType
    {
        [Description("Wage Via Paycheck")]
        WageViaPaycheck,
        [Description("Wage Via Direct Deposit")]
        WageViaDirectDeposit,
        [Description("Overtime Bonus")]
        OvertimeBonus,
        [Description("Paid Holiday")]
        PaidHoliday,
        [Description("Mobility")]
        Mobility,
        [Description("Medical Insurence")]
        MedicalInsurence,
        [Description("Food And Beverage")]
        FoodAndBeverage,
        [Description("Pension Plan")]
        PensionPlan,
        [Description("Paid Time Off")]
        PaidTimeOff,
        [Description("Professional Development")]
        ProfessionalDevelopment,
        [Description("'Work From Home' Compensation")]
        WorkFromHomeCompensation
    }
}
