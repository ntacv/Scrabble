//C# Example of calling the Interrupt() method on the Main thread

using System;
using System.Threading;

class ThreadEx
{
	Thread thread;

	//Constructor of our class
	public ThreadEx(String name, Thread main)
	{
		//Creating a new thread, based on the object of ThreadEx class
		//Which is referred by 'this' reference
		//We are also specifying the entry-point method that will be called to begin the execution of our thread.
		thread = new Thread(this.Run);

		//Setting the name of a thread by using the Name property of Thread
		thread.Name = name;

		//Calling the Start() method, which calls the entry-point method defined with an argument
		//And passing it an argument i.e. reference to the Main Thread
		thread.Start(main);
	}


	//Entry Point of the new thread.
	public void Run(Object main)
	{

		//Getting the reference to the currently executing thread.
		Thread th = Thread.CurrentThread;

		Console.WriteLine(th.Name + " has started its execution");

		//Casting the value of main(which was internally an Thread), from Object to Thread
		Thread th2 = (Thread)main;

		Console.WriteLine(th2.Name + " is being interrupted");

		//Calling the Interrupt() method on the Main thread
		//this will throw ThreadInterruptedException in it
		th2.Interrupt();

		Console.WriteLine(th.Name + " has finished its execution ");

	}


}