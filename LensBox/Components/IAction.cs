using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LensBox.Interface;


namespace LensBox.Components
{
    //Check GPT. I already searching for this topic. keywords: "free", IAction 
    //Or leave only IAction<T> and for "free" actions use the Map object
    public interface IAction<T> : IDisplayable
    {
        public void Execute(T obj);
    }

    public interface IAction : IDisplayable
    {
        public void Execute();
    }
}
