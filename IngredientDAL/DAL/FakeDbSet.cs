using IngredientDAL.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
namespace IngredientDAL.DAL
{
    public class FakeDbSet<T> : IDbSet<T> where T : class
    {
        readonly ObservableCollection<T> _data;
        readonly IQueryable _query;

        #region Interfaced Methods
        public FakeDbSet()
        {
            _data = new ObservableCollection<T>();
            _query = _data.AsQueryable();
        }

        public virtual T Find(params object[] keyValues)
        {
            throw new NotImplementedException("Derive from FakeDbSet<T> and override Find");
        }

        public T Add(T item)
        {
            _data.Add(item);
            return item;
        }

        public T Remove(T item)
        {
            _data.Remove(item);
            return item;
        }

        public T Attach(T item)
        {
            _data.Add(item);
            return item;
        }

        public T Detach(T item)
        {
            _data.Remove(item);
            return item;
        }

        public T Create()
        {
            return Activator.CreateInstance<T>();
        }

        public TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, T
        {
            return Activator.CreateInstance<TDerivedEntity>();
        }

        public ObservableCollection<T> Local
        {
            get { return _data; }
        }

        Type IQueryable.ElementType
        {
            get { return _query.ElementType; }
        }

        System.Linq.Expressions.Expression IQueryable.Expression
        {
            get { return _query.Expression; }
        }

        IQueryProvider IQueryable.Provider
        {
            get { return _query.Provider; }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return _data.GetEnumerator();
        }
        #endregion
    }

    public class FakeIngredientSet : FakeDbSet<Ingredient>
    {
        public override Ingredient Find(params object[] keyValues)
        {
            return this.SingleOrDefault(i => i.IngredientId == (int)keyValues.Single());
        }
    }

    public class FakeProductSet : FakeDbSet<Product>
    {
        public override Product Find(params object[] keyValues)
        {
            return this.SingleOrDefault(i => i.ProductId == (int)keyValues.Single());
        }
    }

    public class FakeReceiptItemSet : FakeDbSet<ReceiptItem>
    {
        public override ReceiptItem Find(params object[] keyValues)
        {
            return this.SingleOrDefault(i => i.ReceiptItemId == (int)keyValues.Single());
        }
    }

    public class FakeRecipeItemSet : FakeDbSet<RecipeItem>
    {
        public override RecipeItem Find(params object[] keyValues)
        {
            return this.SingleOrDefault(i => i.RecipeItemId == (int)keyValues.Single());
        }
    }

    public class FakeRecipeSet : FakeDbSet<Recipe>
    {
        public override Recipe Find(params object[] keyValues)
        {
            return this.SingleOrDefault(i => i.RecipeId == (int)keyValues.Single());
        }
    }

    public class FakeStepSet : FakeDbSet<Step>
    {
        public override Step Find(params object[] keyValues)
        {
            return this.SingleOrDefault(i => i.StepId == (int)keyValues.Single());
        }
    }

    public class FakeRefrigeratorSet : FakeDbSet<Refrigerator>
    {
        public override Refrigerator Find(params object[] keyValues)
        {
            return this.SingleOrDefault(i => i.RefrigeratorId == (int)keyValues.Single());
        }
    }

    public class FakeRefrigeratedProductSet : FakeDbSet<RefrigeratedProduct>
    {
        public override RefrigeratedProduct Find(params object[] keyValues)
        {
            return this.SingleOrDefault(i => i.RefrigeratedProductId == (int)keyValues.Single());
        }
    }
}
