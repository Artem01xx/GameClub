using System;
using System.Collections.Generic;

namespace C__Learning
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            ComputerClub computerClub = new ComputerClub(5, random);
            computerClub.Work();
        }    
    }
  
    class ComputerClub
    {
        private int _computerClubMoney;
        private Queue<Client> clientsQuary = new Queue<Client>();
        private List<Computer> _computers = new List<Computer>();
        public ComputerClub(int computersCount, Random random)
        {
            
            _computerClubMoney = 0;

            for (int i = 0; i< computersCount ; i++)
            {
                _computers.Add(new Computer(random));
            }

            CreateNewClients(random.Next(5, 11), random);
        }

        public void CreateNewClients(int count, Random random)
        {
            for(int i = 0; i < count; i++)
            {
                clientsQuary.Enqueue(new Client(random));
            }
        }
        public void Work()
        {
            while (clientsQuary.Count>0)
            {
                Client client = clientsQuary.Dequeue();
                Console.WriteLine($"Starting to work! Money in a game club is: {_computerClubMoney} $ ");
                Console.WriteLine($"Here is our client, he desire to buy {client.DesiredMinutes} minutes");
                ShowAllComputerStates();

                Console.WriteLine("\n You offer him to buy computer under number: ");
                string input = Console.ReadLine();
                if(int.TryParse(input, out int computerNumber))
                {
                    computerNumber -= 1;
                    if(computerNumber >= 0 && computerNumber < _computers.Count) 
                    {
                        if (_computers[computerNumber].IsTaken)
                        {
                            Console.WriteLine("You gave him busy place so he left");
                        }

                        else
                        {
                            if (client.CheckPayment(_computers[computerNumber]))
                            {
                                Console.WriteLine("Client pay for the place then went to play games under number: " + (computerNumber+1));
                                _computerClubMoney += client.Pay();
                                _computers[computerNumber].BecomeTaken();
                            }
                            else
                            {
                                Console.WriteLine("Client does'not have enougth money so he left");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("You dont know what computer you need to give so client left");
                    }
                }
                else
                {
                    CreateNewClients(1, new Random());
                    Console.WriteLine("Incorect input, try again");
                }

                Console.WriteLine("To go on another client press any button");
                Console.ReadKey();
                Console.Clear();
            }
            Console.WriteLine("Queue is over. You can relax now");
        }

        private void ShowAllComputerStates()
        {
            Console.WriteLine("List of all computers");
            for (int i = 0; i< _computers.Count; i++)
            {
                Console.Write(i+1 + "-");
                _computers[i].ShowState();
            }
        }
      
    }


    class Computer
    {
        public int NumberOfComputer { get; private set; }
        public int QuantityOfComputers {get; private set;}
      
        public int PricePerMinute { get; private set; }
        public bool IsTaken { get; private set; }

        public Computer(Random random)
        {
          
            PricePerMinute = random.Next(5, 15);
            IsTaken = false;
        }

        

        public bool BecomeTaken()
        { return IsTaken = true;}
       

        public bool BecomeEmpty()
        { return IsTaken = false;}

        public void ShowState()
        {
            if (IsTaken)
                Console.WriteLine("Computer is busy, try other");
            else
                Console.WriteLine("Empty Computer, price for minute is: " + PricePerMinute + "$");
        }


    }

    class Client
    {
        private int _moneyToPay;
        private int _money;
        public int DesiredMinutes { get; private set;}
        
        public Client(Random random)
        {
            _money = random.Next(40, 60);
            DesiredMinutes = random.Next(1, 15);
        }

        public int Pay()
        {
            _money -= _moneyToPay;
            return _moneyToPay;
        }

        public bool CheckPayment(Computer computer)
        {
            _moneyToPay = DesiredMinutes * computer.PricePerMinute;
            if (_money >= _moneyToPay)
              return true;
            
            else
                _moneyToPay = 0;
                return false;
        }

    }

 }
  
