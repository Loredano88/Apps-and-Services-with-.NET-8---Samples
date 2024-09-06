using System.Diagnostics;
OutputThreadInfo();
Stopwatch timer = Stopwatch.StartNew();

/*
SectionTitle("Running methods synchronously on the thread.");
MethodA();
MethodB();
MethodC();
WriteLine($"{timer.ElapsedMilliseconds:#,##0}ms elapsed");
*/

/*
timer.Restart();
SectionTitle("Running methods asynchronously on multiple threads");
Task taskA = new(MethodA);
taskA.Start();
Task taskB = Task.Factory.StartNew(MethodB);
Task taskC = Task.Run(MethodC);
Task[] tasks = { taskA, taskB, taskC };
Task.WaitAll(tasks);
WriteLine($"{timer.ElapsedMilliseconds:#,##0}ms elapsed");
*/

/*
SectionTitle("Pass the result of one task as an input into another");
Task<string> taskServiceThenSProc = Task.Factory
    .StartNew(CallWebService)
    .ContinueWith(previousTask => CallStoredProcedure(previousTask.Result));
WriteLine($"Result: {taskServiceThenSProc.Result}");
WriteLine($"{timer.ElapsedMilliseconds:#,##0}ms elapsed.");
*/

SectionTitle("Nested and child tasks");
Task outerTask = Task.Factory.StartNew(OuterMethod);
outerTask.Wait();
WriteLine("Console app is stopping.");
WriteLine($"{timer.ElapsedMilliseconds:#,##0}ms elapsed.");
