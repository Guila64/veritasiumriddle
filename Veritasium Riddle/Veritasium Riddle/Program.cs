using Veritasium_Riddle.Classes;

var interactions = 100;
var numberOfPrisionersAndBoxes = 10000;

List<Task<bool>> tasks = new List<Task<bool>>();

var results = await Task.WhenAll(Enumerable.Range(0, interactions).Select(o => new Riddle(numberOfPrisionersAndBoxes).Run()));
var succeed = results.Count(o => o);
var failed = results.Count() - succeed;

Console.WriteLine($"Succeed: {succeed}\nFailed: {failed}");
