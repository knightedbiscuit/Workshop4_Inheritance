using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workshop4_Inheritance
{
    class BankTest3
    {
        static void Main(string[] args)
        {
            Customer y = new Customer("Tan Ah Kow", "20, Seaside Road", "XXX20", new DateTime(1989, 10, 11));
            Customer z = new Customer("Kim Lee Keng", "2, Rich View", "XXX9F", new DateTime(1993, 4, 25));
            SavingsAccount a = new SavingsAccount("001-001-001", y, 2000);
            a.Deposit(100);
            Console.WriteLine(a.Show());
            a.Withdraw(200);
            Console.WriteLine(a.Show());
            a.CalculateInterest();
            a.CreditInterest();
            Console.WriteLine(a.Show());
        }
    }

    class Customer
    {
        //attributes
        string name;
        string address;
        string nric;
        DateTime dob = new DateTime();
        int age;

        //prroperty
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }
        public string Address
        {
            get
            {
                return address;
            }
            set
            {
                address = value;
            }
        }

        public string NRIC
        {
            get
            {
                return nric;
            }
        }

        public int Age
        {
            get
            {
                return age;
            }
        }

        //constructor
        public Customer(string name, string address, string nric, DateTime dobIn)
        {
            this.name = name;
            this.address = address;
            this.nric = nric;
            dob = dobIn;
            age = DateTime.Now.Year - dob.Year;
        }

        public Customer() : this("NoName", "NoAddress", "NoNRIC", new DateTime(1900, 1, 1))
        { }

        public string Showcustomer()
        {
            return (String.Format(name + "\t\t" + address + "\t\t" + nric + "\t\t" + "{0}", age));
        }

    }

    class Account
    {
        //attributes
        protected string accountNum;
        protected double currentAmt;
        Customer data;

        //properties
        public string AccountNum
        {
            get {
                return accountNum;
            }
        }

        public double CurrentAmt
        {
            get {
                return currentAmt;
            }
        }
        

        //constructor
        public Account(string accountNum, Customer data, double initialAmt)
        {
            this.accountNum = accountNum;
            this.data = data;
            currentAmt = initialAmt;
        }

        public Account()
            : this("xxx-xxx-xxx", new Customer(), 0)
        { }

        //methods
        public void Deposit(double amount)
        {
            currentAmt += amount;
        }

        public void Withdraw(double amount)
        {
            if (currentAmt-amount>=0)
            {
                currentAmt -= amount;
            }
            else {
                Console.WriteLine("Withdrawal failed. Please ensure that there is sufficient balance in your account");
            }        
        }

        public void TransferTo(double amount, Account Another)
        {
            Withdraw(amount);
            Another.Deposit(amount);
        }

        public string Show()
        {
            return (String.Format(accountNum + "\t\t" + data.Showcustomer() + "\t\t" + "{0}", currentAmt));
        }
    }

    class SavingsAccount :Account
    {
        static double savingsInterestRate;
        double interest;

        //Constructor
        public SavingsAccount(string accountNum, Customer data, double initialAmt) :base(accountNum, data, initialAmt)
        {
            savingsInterestRate = 0.01;
        }

        public SavingsAccount() : base()
        { }

        //Property. Bank changes interest rates.
        public double InterestRate
        {
            get
            {
                return savingsInterestRate;
            }
            set {
                savingsInterestRate = value;
            }
        }

        public double Interest
        {
            get {
                CalculateInterest();
                return interest;
            }
        }

        // Methods
        public void CalculateInterest() //calculate annual interest
        {
            if (base.currentAmt > 0)
            {
                interest = base.currentAmt * savingsInterestRate;
            }
            else
            {
                interest = 0;
            }
        }

        public void CreditInterest()
        {
            base.Deposit(interest);
        }

    }

    class CurrentAccount :Account
    {
        static double currentInterestRate;
        double interest;

        //Constructor
        public CurrentAccount(string accountNum, Customer data, double initialAmt) :base(accountNum, data, initialAmt)
        {
            currentInterestRate = 0.025;
        }

        public CurrentAccount() : base()
        { }

        //Property. Bank changes interest rates.
        public double InterestRate
        {
            get
            {
                return currentInterestRate;
            }
            set
            {
                currentInterestRate = value;
            }
        }

        public double Interest
        {
            get
            {
                CalculateInterest();
                return interest;
            }
        }

        // Methods
        public void CalculateInterest() //calculate annual interest
        {
            if (base.currentAmt > 0)
            {
                interest = base.currentAmt * currentInterestRate;
            }
            else
            {
                interest = 0;
            }
        }

        public void CreditInterest()
        {
            base.Deposit(interest);
        }
    }

    class OverdraftAccount :Account
    {
        double interestRateNeg = 0.025;
        double interestRatePos = 0.06;
        static double currInterestRate;
        double interest;

        //Constructor
        public OverdraftAccount(string accountNum, Customer data, double initialAmt) :base(accountNum, data, initialAmt)
        {
            setCurrInterestRate();
        }

        public OverdraftAccount() : base()
        { }

        //Property. Bank changes interest rates.
        public double InterestRateNeg
        {
            get
            {
                return interestRateNeg;
            }
            set {
                interestRateNeg = value;
            }
        }

        public double InterestRatePos
        {
            get
            {
                return interestRatePos;
            }
            set
            {
                interestRatePos = value;
            }
        }

        public double Interest
        {
            get
            {
                CalculateInterest();
                return interest;
            }
        }

        // Methods
        public void CalculateInterest() //calculate annual interest
        {
            setCurrInterestRate();
            interest = base.currentAmt * currInterestRate;
        }

        public void CreditInterest()
        {
            base.Deposit(interest);
        }

        private void setCurrInterestRate()
        {
            if (base.currentAmt < 0)
            {
                currInterestRate = interestRateNeg;
            }
            else {
                currInterestRate = interestRatePos;
            }
        }
    }
}
