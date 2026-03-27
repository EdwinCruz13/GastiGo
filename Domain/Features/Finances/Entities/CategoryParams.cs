using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Features.Finances.Entities
{
    public class CategoryParams:AuditableEntity
    {
        public Guid ParamId => Id;
        public Guid CategoryId { get; private set; }
        public Category Category { get; private set; } = default!;
        public Boolean ApplySalary { get; private set; }
        public Boolean ApplyPercentage { get; private set; }
        public Boolean ApplyAmount { get; private set; }
        public Double Value { get; private set; }

        private CategoryParams() { }
        public CategoryParams(Guid categoryId, Boolean applySalary, Boolean applyPercentage, Boolean applyAmount, Double value)
        {
            CategoryId = categoryId;
            ApplySalary = applySalary;
            ApplyPercentage = applyPercentage;
            ApplyAmount = applyAmount;
            Value = value;
        }

    }
}
