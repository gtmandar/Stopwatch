namespace Stopwatch
{
    using System;

    internal class Program
    {
        static void Main(string[] args)
        {
            IContextBase stopwatch = new Context(new ConcreteState.InitialScreenState());

            stopwatch.PressSecondButton();

            stopwatch.PressFirstButton(); 
            stopwatch.PressSecondButton(); 
            stopwatch.PressSecondButton(); 
            stopwatch.PressFirstButton(); 
            stopwatch.PressFirstButton(); 
            stopwatch.PressSecondButton(); 
            stopwatch.PressFirstButton(); 
            stopwatch.PressSecondButton(); 
        }
    }

    internal class Context : IContextBase
    {
        private IState currentState;

        public Context(IState initialState)
        {
            currentState = initialState;
        }

        public void PressFirstButton()
        {
            currentState = currentState.PressFirstButton();
            Console.WriteLine($"Current State: {currentState.GetType().Name}, Button1: {currentState.TextOnFirstButton}, Button2: {currentState.TextOnSecondButton}");
        }

        public void PressSecondButton()
        {
            currentState = currentState.PressSecondButton();
            Console.WriteLine($"Current State: {currentState.GetType().Name}, Button1: {currentState.TextOnFirstButton}, Button2: {currentState.TextOnSecondButton}");
        }
    }

    internal interface IContextBase
    {
        void PressFirstButton();
        void PressSecondButton();
    }

    internal interface IState
    {
        string TextOnFirstButton { get; }
        string TextOnSecondButton { get; }
        IState PressFirstButton();
        IState PressSecondButton();
    }

    namespace ConcreteState
    {
        internal class InitialScreenState : IState
        {
            public string TextOnFirstButton => "Start";
            public string TextOnSecondButton => "Lap";

            public IState PressFirstButton()
            {
                Console.WriteLine("Stopwatch Started");
                return new RunningState();
            }

            public IState PressSecondButton()
            {
                Console.WriteLine("Nothing Happens");
                return this;
            }
        }

        internal class RunningState : IState
        {
            public string TextOnFirstButton => "Stop";
            public string TextOnSecondButton => "Lap";

            public IState PressFirstButton()
            {
                Console.WriteLine("Stopwatch Stopped");
                return new DisplayingResultState();
            }

            public IState PressSecondButton()
            {
                Console.WriteLine("Lap");
                return this;
            }
        }

        internal class DisplayingResultState : IState
        {
            public string TextOnFirstButton => "Start";
            public string TextOnSecondButton => "Reset";

            public IState PressFirstButton()
            {
                Console.WriteLine("Stopwatch Restarted");
                return new RunningState();
            }

            public IState PressSecondButton()
            {
                Console.WriteLine("Stopwatch Reset");
                return new InitialScreenState();
            }
        }
    }
}

