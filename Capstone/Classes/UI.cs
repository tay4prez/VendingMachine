﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Classes;

namespace Capstone.Classes
{
	public class UI
	{
		private VendingMachine vendingMachine;

		public UI(VendingMachine vendingMachine)
		{
			this.vendingMachine = vendingMachine;
		}

		// Method Main Screen
		public void MainScreen()
		{
			while (true)
			{
				//Console.Clear();
				Console.WriteLine("Main Menu:");
				Console.WriteLine("1. Display Vending Machine Items");
				Console.WriteLine("2. Purchase");
				//Console.WriteLine("3. Quit");

				Console.Write("Please make a selection: ");
				string input = Console.ReadLine();

				if (input == "1")
				{
					Console.WriteLine();
					DisplayInventory();
				}
				else if (input == "2")
				{
					Console.WriteLine();
					PurchaseSubmenu();
				}
				//else if (input == "3")
				//{
				//	Console.WriteLine("Quitting...");
				//	return;
				//}
				else
				{
					Console.WriteLine("Please enter a valid selection!");
				}
			}
		}

		// Method DisplayInventory
		public void DisplayInventory()
		{
			Console.Clear();
			foreach (KeyValuePair<string, List<Item>> kvp in vendingMachine.Stock)
			{
				if (kvp.Value.Count == 0)
				{
					Console.WriteLine($"{kvp.Key} is out of stock");
				}
				else
				{ 
				Console.WriteLine($"{kvp.Key} {kvp.Value[0].Name} {kvp.Value[0].Cost:C} {kvp.Value.Count}");
				}
			}
			Console.WriteLine();
		}

		// Method Purchase Screen
		public void PurchaseSubmenu()
		{
			while (true)
			{
				Console.WriteLine($"Current Balance: {vendingMachine.Balance:C}");
				Console.WriteLine();
				Console.WriteLine("1. Feed Money");
				Console.WriteLine("2. Select Product");
				Console.WriteLine("3. Finish Transaction");

				Console.Write("Please make a selection: ");
				string input = Console.ReadLine();

				while (true)
				{
					if (input == "1")
					{
						Console.WriteLine();
						Console.Write("Enter dollar amount to feed into the vending machine: ");


						try
						{
							int dollarsEntered = int.Parse(Console.ReadLine());
							if (dollarsEntered > 0)
							{
								Console.WriteLine();
								vendingMachine.FeedMoney(dollarsEntered);
							}
						}
						catch (Exception)
						{
							Console.WriteLine();
							Console.WriteLine($"The dollar amount you have entered is not valid.");
							
						}

						break;

					}
					else if (input == "2")
					{
						Console.WriteLine();
						DisplayInventory();
						Console.Write("Enter the item position to purchase: ");
						string possiblePurchase = Console.ReadLine().ToUpper();
						Console.WriteLine();
						vendingMachine.DispenseItem(possiblePurchase);
						break;
					}
					else if (input == "3")
					{
						Console.WriteLine();
						Console.Write("Here is your change: ");
						int[] change = vendingMachine.GiveChange();
						Console.WriteLine($"{change[0]} quarters, {change[1]} dimes, {change[2]} nickles");
						Console.WriteLine();

						foreach (Item purchasedItem in vendingMachine.PurchasedItems)
						{
							Console.WriteLine($"{purchasedItem.MakeSound()}");
						}
						Console.ReadLine();
						MainScreen();

					}
					else
					{
						Console.WriteLine("Please enter a valid selection!");
						break;

					}
					
					
				}
				//Call Main Menu
				//MainScreen();
			}
		}

		// Method Out of Stock
		// To display a message to user that the selected item is out of stock

		// Method Invalid Selection
		// To display a message to user that they have entered an invalid item

		// Method Finish transaction
		// Need to get the List of purchased items and foreach through
		// Print out each item name and the noise
	}
}
