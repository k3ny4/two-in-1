using System;

class MobilePhone
{
    public string Model { get; set; }
    public string Processor { get; set; }
    public int RAM { get; set; }
    public int Storage { get; set; }
    public string Camera { get; set; }

    public MobilePhone Clone()
    {
        return (MobilePhone)this.MemberwiseClone();
    }

    public void DisplaySpecs()
    {
        Console.WriteLine($"Model: {Model}");
        Console.WriteLine($"Processor: {Processor}");
        Console.WriteLine($"RAM: {RAM} GB");
        Console.WriteLine($"Storage: {Storage} GB");
        Console.WriteLine($"Camera: {Camera}");
        Console.WriteLine("--------------------");
    }
}

interface IPhoneBuilder
{
    IPhoneBuilder SetModel(string model);
    IPhoneBuilder SetProcessor(string processor);
    IPhoneBuilder SetRAM(int ram);
    IPhoneBuilder SetStorage(int storage);
    IPhoneBuilder SetCamera(string camera);
    MobilePhone Build();
}

class PhoneBuilder : IPhoneBuilder
{
    private MobilePhone _phone;

    public PhoneBuilder()
    {
        Reset();
    }

    public void Reset()
    {
        _phone = new MobilePhone();
    }

    public IPhoneBuilder SetModel(string model)
    {
        _phone.Model = model;
        return this;
    }

    public IPhoneBuilder SetProcessor(string processor)
    {
        _phone.Processor = processor;
        return this;
    }

    public IPhoneBuilder SetRAM(int ram)
    {
        _phone.RAM = ram;
        return this;
    }

    public IPhoneBuilder SetStorage(int storage)
    {
        _phone.Storage = storage;
        return this;
    }

    public IPhoneBuilder SetCamera(string camera)
    {
        _phone.Camera = camera;
        return this;
    }

    public MobilePhone Build()
    {
        var result = _phone;
        Reset();
        return result;
    }
}

class Director
{
    private readonly IPhoneBuilder _builder;

    public Director(IPhoneBuilder builder)
    {
        _builder = builder;
    }

    public MobilePhone ConstructPhone(string model)
    {
        return _builder
            .SetModel(model)
            .SetProcessor("Snapdragon 888")
            .SetRAM(12)
            .SetStorage(256)
            .SetCamera("108 MP")
            .Build();
    }
}

class Program
{
    static void Main(string[] args)
    {
        var builder = new PhoneBuilder();
        var director = new Director(builder);

        Console.WriteLine("=== Mobile Phone Builder ===");
        Console.WriteLine("1. Build a pre-configured phone");
        Console.WriteLine("2. Build a custom phone");
        Console.Write("Choose an option (1/2): ");
        string choice = Console.ReadLine();

        MobilePhone phone = null;

        if (choice == "1")
        {
            Console.Write("Enter phone model: ");
            string model = Console.ReadLine();
            phone = director.ConstructPhone(model);
        }
        else if (choice == "2")
        {
            Console.Write("Enter model: ");
            string model = Console.ReadLine();

            Console.Write("Enter processor: ");
            string processor = Console.ReadLine();

            Console.Write("Enter RAM (GB): ");
            int ram = int.Parse(Console.ReadLine());

            Console.Write("Enter storage (GB): ");
            int storage = int.Parse(Console.ReadLine());

            Console.Write("Enter camera: ");
            string camera = Console.ReadLine();

            phone = builder
                .SetModel(model)
                .SetProcessor(processor)
                .SetRAM(ram)
                .SetStorage(storage)
                .SetCamera(camera)
                .Build();
        }

        Console.WriteLine("\nPhone built successfully:");
        phone.DisplaySpecs();

        Console.WriteLine("Cloning the phone...");
        MobilePhone clonedPhone = phone.Clone();

        Console.WriteLine("\nCloned phone specs:");
        clonedPhone.DisplaySpecs();
    }
}