using Microsoft.Extensions.DependencyInjection;
using System;

namespace DotnetCoreDIWithParameter
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddScoped<IAction, Action>();
            serviceCollection.AddScoped<IDog>(svc => new Dog(svc.GetService<IAction>()));

            var serviceProvider = serviceCollection.BuildServiceProvider();
            var dog = serviceProvider.GetService<IDog>();

            dog.CallShakeHands();
        }

        public interface IAction
        {
            void ShakeHands();
        }

        public class Action : IAction
        {
            public void ShakeHands()
            {
                Console.WriteLine("Shaking hands!");
            }
        }

        public interface IDog
        {
            void CallShakeHands();
        }

        public class Dog : IDog
        {
            IAction _action;
            public Dog(IAction action)
            {
                _action = action;
            }
            public void CallShakeHands()
            {
                Console.WriteLine("Dog: ");
                _action.ShakeHands();
            }
        }
    }
}
