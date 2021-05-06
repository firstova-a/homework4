using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hw4
{
	class Program
	{
		static Queue<int> Queue = new Queue<int>();
		static bool flag = true;
		static AtistContext context = new AtistContext();

		static void Main(string[] args)
		{
			context.Database.EnsureDeleted();
			context.Database.EnsureCreated();
			Console.WriteLine("Веедите одну из команд: 1 - первый запрос" + Environment.NewLine 
				+ "2 - второй запрос" + Environment.NewLine
				+ "3 - остановить программу" + Environment.NewLine);
			Task process = Processing();
			context.Dispose();
		}

		static async Task Processing()
		{
			Task queryTask = Query();
			while (flag)
			{
				string caseSwitch = Console.ReadLine();
				switch (caseSwitch)
				{
					case "1":
						Queue.Enqueue(1);
						break;
					case "2":
						Queue.Enqueue(2);
						break;
					case "3":
						flag = false;
						break;
					default:
						Console.WriteLine("Understand command");
						break;
				}
			}
			await queryTask;
		}

		static async Task Query()
		{
			while(flag)
			{
				int query = 0;
				try
				{
					await Task.Delay(1000);
					query = Queue.Dequeue();
				}
				catch
				{
					//Console.WriteLine(Environment.NewLine + "Error with queue/Queue is empty");
				}
				if (query == 1)
				{
					await Task.Delay(2500);
					Console.WriteLine(Environment.NewLine +"Answer: " + query + Environment.NewLine);
					IQueryable<ArtOrder> orders = from Orders in context.Orders
												  select Orders;
					List<ArtOrder> orderList = orders.ToList();
					foreach (ArtOrder order in orderList)
					{
						Console.WriteLine("Id: " + order.Id.ToString() + Environment.NewLine
							+ order.Description + Environment.NewLine);
					}
				}
				if (query == 2)
				{
					await Task.Delay(3500);
					Console.WriteLine(Environment.NewLine + "Answer: " + query + Environment.NewLine);
					IQueryable<Art> arts = from Arts in context.Arts
										   select Arts;
					List<Art> artList = arts.ToList();
					foreach (Art art in artList)
					{
						if (art.Types.Contains(Art.eType.sketch))
						{
							Console.WriteLine("Id: " + art.Id.ToString() + Environment.NewLine
								+ art.Description + Environment.NewLine);
						}
					}
				}
			}
		}

		/*static int GetQuery()
		{
			int number = -1;
			if(Queue.Count != 0) number = Queue[0];
			if(Queue.Count == 1)
			{
				Queue.RemoveAt(0);
			}
			if(Queue.Count > 1)
			{
				for (int i = 0; i <= Queue.Count - 1; i++)
				{
					Queue[i] = Queue[i + 1];
					if (Queue[i] == Queue.Count - 2) Queue.Remove(Queue[i]);
				}
			}
			return number;
		}*/

	}
}
