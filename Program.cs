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
            serviceCollection.AddScoped<IMachine, Machine>();
            serviceCollection.AddScoped<IDog>(svc => new Dog(svc.GetService<IAction>(),
                                                             svc.GetService<IMachine>()));

            var serviceProvider = serviceCollection.BuildServiceProvider();
            var dog = serviceProvider.GetService<IDog>();

            dog.DogDance();
            dog.DogGlow();
        }

        public interface IAction
        {
            void Dance();
        }

        public class Action : IAction
        {
            public void Dance()
            {
                Console.WriteLine("Dancing!");
            }
        }

        public interface IMachine
        {
            void Glow();
        }

        public class Machine : IMachine
        {
            public void Glow()
            {
                Console.WriteLine("Glowing!");
            }
        }

        public interface IDog
        {
            void DogDance();
            void DogGlow();
        }

        public class Dog : IDog
        {
            IAction _action;
            IMachine _machine;
            public Dog(IAction action, IMachine machine)
            {
                _action = action;
                _machine = machine;
            }
            public void DogDance()
            {
                Console.WriteLine("Dog is");
                _action.Dance();
            }

            public void DogGlow()
            {
                Console.WriteLine("Dog is");
                _machine.Glow();
            }
        }
    }
}
