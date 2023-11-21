using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LensBox.Interface;

namespace LensBox.Actions
{
    public interface IAction<T> : IDisplayable
    {
        public void Execute(T obj);
    }

    public interface IAction : IDisplayable
    {
        public void Execute();
    }
}
